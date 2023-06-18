using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using static PolarityHandler;

public class PlayerController : GravityObject, PlayerInput.IPlayerActions
{
    [Header("Parameters")]
    [HideInInspector] public bool isSleeping;                               //If Player should calculate physics and act on currentStage
    [SerializeField] Polarity polarity;
    [SerializeField] float walkSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float jumpLength;
    [SerializeField] float maxFallingSpeed;
    [SerializeField, Range(1, 10)] float dropMultiplier;
    [SerializeField, Range(0f, 1f)] float inAirMovementCap;                 //How much can player turn when in air
    [SerializeField] float rotateToGroundSpeed;                             //Speed at wich player rotates towards Ground after exiting forcefield
    [SerializeField] float dashDuration;
    [SerializeField] float dashSpeed;
    [SerializeField] float forcefieldVelocityLoss;                          //How much jddVelocity player looses per frame when inside forcefield
    [SerializeField] float groundCheckradius;
    [SerializeField] Color negativeColor = new Color(50, 50, 255);
    [SerializeField] Color positiveColor = new Color(171, 72, 97);

    PlayerInput controls;
    ForceField currentForcefield;
    Vector2 jddVelocity;                        //Velocity of jump/dash/drop in one 
    Vector2 forcefieldVelocity;
    Vector2 leftStickDir;                       //Direction of the left gamepadstick or WASD input of keyboard
    Vector2 dashDirection;
    Vector2 forcefieldExitDirection;            //Direction of velocity when player is exiting a forcefield
    Vector2 forcefieldEnterDirection;           //Same as above but when entering a forcefield
    Vector2 velocitySaveWhenSleeping;           //Save current Velocity when rotatinig camera to aplly it back on after camera finished rotating


    float turnability = 1;                      
    float walkVelocityX;                        //horizontal Movement from input (Gamepad, Keyboard)
    float timeSinceStartedJumping;
    float timeSinceStartedDashing;
    float forcefieldExitMagnitude;              //Strength of Velocity when exiting a forcefield
    float forcefieldEnterMagnitude;             //Strength of Velocity when entering a forcefield
    float lerpToRotation = 0;                   //Needed when exiting or entering forcefield
    float rotationLerpT =0;

    bool isGrounded;
    bool canDash = true;
    bool isLerpingRotation;

    [Header("References")]
    [SerializeField] Rigidbody2D rb;                            
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] Transform headCheckTransform;
    [SerializeField] Transform leftSideCheckTransform;
    [SerializeField] Transform rightSideCheckTransform;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GravityHandler gravityHandler;
    [SerializeField] LayerMask groundLayer;                     //Layer Player can stand on (ground and gravityObject)
    [SerializeField] Sprite bloodSplash;
    [SerializeField] Collider2D pCollider;


    private void Start()
    {
        if (controls == null)
        {
            controls = new PlayerInput();
            controls.Enable();
            controls.Player.SetCallbacks(this);
        }

        if (gravityChangedEvent == null)
            gravityChangedEvent = new UnityEngine.Events.UnityEvent();

        gravityDirection = Vector2.down;

        gravityChangedEvent.AddListener(EndedgravityChange);

        spriteRenderer.color = polarity == Polarity.negativ ? negativeColor : positiveColor;
        inAirMovementCap = -inAirMovementCap + 1;
        walkVelocityX = 0;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,.3f,.3f);
        Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckradius);
        Gizmos.DrawWireSphere(headCheckTransform.position, groundCheckradius);

        Gizmos.color = new Color(.3f, 1, .3f);
        Gizmos.DrawWireSphere(leftSideCheckTransform.position, .1f);
        Gizmos.DrawWireSphere(rightSideCheckTransform.position, .1f);
    }

    private void OnDestroy()
    {
        controls.Disable();
    }


    private void FixedUpdate()
    {
        CheckIfPlayerEnteredNewState();

        if (isSleeping)
            return;

        ActOnCurrentStage();
        Vector2 walkVelocity = Rotate90Deg(gravityDirection, true) * (walkVelocityX * walkSpeed);
        rb.velocity = walkVelocity + forcefieldVelocity + jddVelocity;
    }

    void CheckIfPlayerEnteredNewState()
    {
        isGrounded = IsGrounded();

        if (!isGrounded && currentState <= State.walk)                   //player started falling
        {
            currentState = State.drop;
            turnability = inAirMovementCap;
        }
        else if (isGrounded && currentState == State.drop)               //player landed after falling
        {
            currentState = State.idle;
            timeSinceStartedDropping = 0;
            walkVelocityX = leftStickDir.x;
            canDash = true;
            turnability = 1;
            jddVelocity = Vector2.zero;

            PrepareGravityChange();
        }

        if (currentState == State.idle && walkVelocityX != 0)            //player started walking      
            currentState = State.walk;
        else if (currentState == State.walk && walkVelocityX == 0)       //player stopped walking
            currentState = State.idle;
    }

    void ActOnCurrentStage()
    {
        

        switch (currentState)                                            //react depending on playerState
        {
            case State.jump:
                Jump();
                break;
            case State.drop:
                Drop();
                break;
            case State.dash:
                Dash();
                break;
            case State.forcefield:
                ForceFieldInteraction();
                break;
        }

        CalculateForcefieldExitVelocity();

        if (BumpedSide())
        {
            forcefieldExitMagnitude = 0;
            forcefieldVelocity = Vector2.zero;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckradius, groundLayer);
    }

    bool BumpedHead()
    {
        return Physics2D.OverlapCircle(headCheckTransform.position, groundCheckradius, groundLayer);
    }

    bool BumpedSide()
    {
        bool left = Physics2D.OverlapCircle(leftSideCheckTransform.position, .1f, groundLayer);
        bool right = Physics2D.OverlapCircle(rightSideCheckTransform.position, .1f, groundLayer);
        return left || right;
    }

    void PrepareGravityChange()
    {
        Vector2 pos = transform.position;
        Collider2D groundCollider = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckradius, groundLayer);
        Vector2 collsionPoint = groundCollider.ClosestPoint(pos);

        gravityDirection = collsionPoint - pos;
        float playerRotation = Vector2.SignedAngle(Vector2.down, gravityDirection);
        rotationLerpT = 1;
        rb.rotation = Vector2.SignedAngle(Vector2.down, gravityDirection);
        lerpToRotation = rb.rotation;

        velocitySaveWhenSleeping = rb.velocity;
        gravityHandler.StartGravityChange(this, playerRotation, gravityChangedEvent, rb);
    }

    void EndedgravityChange()
    {
        isSleeping = false;
        rb.velocity = velocitySaveWhenSleeping;
    }

    IEnumerator RotateOverTime(float rotationSpeed, bool linearRotation)
    {
        float startRotation = rb.rotation;
        if (startRotation == lerpToRotation)
            yield break;

        isLerpingRotation = true;
        rotationLerpT = 0;

        while(rotationLerpT < 1)
        {
            rotationLerpT += Time.fixedDeltaTime * rotationSpeed;

            float lerpA = linearRotation ? startRotation : rb.rotation;
            float currentRotation = Mathf.LerpAngle(lerpA, lerpToRotation, rotationLerpT);
            rb.rotation = currentRotation;

            yield return new WaitForFixedUpdate();
        }

        isLerpingRotation = false;
        rb.rotation = lerpToRotation;
    }

    //Handling States
    //---------------------------------------------------------
    void Drop()
    {
        float t = timeSinceStartedDropping + jumpLength;
        float currentDropMultiplier = forcefieldExitMagnitude > 0 ? 1 : dropMultiplier;
        float dropVelocity = CalculateVelocityGraph(t, jumpHeight, jumpLength) * currentDropMultiplier;
        dropVelocity = Mathf.Min(dropVelocity, maxFallingSpeed);

        jddVelocity = dropVelocity / jumpLength  * gravityDirection;

        timeSinceStartedDropping += Time.fixedDeltaTime;
    }

    void Jump()
    {
        float jumpVelocity = CalculateVelocityGraph(timeSinceStartedJumping, jumpHeight, jumpLength);
        jddVelocity = jumpVelocity / jumpLength * -gravityDirection;
        timeSinceStartedJumping += Time.fixedDeltaTime;

        if (timeSinceStartedJumping > jumpLength || BumpedHead())
        {
            currentState = State.drop;
            jddVelocity = Vector2.zero;
        }
    }

    void Dash()
    {
        float dashVelocity = CalculateVelocityGraph(timeSinceStartedDashing, dashSpeed, dashDuration);
        jddVelocity = dashVelocity * dashDirection;
        timeSinceStartedDashing += Time.fixedDeltaTime;

        if(timeSinceStartedDashing > dashDuration)
        {
            currentState = State.drop;
            jddVelocity = Vector2.zero;
            turnability = inAirMovementCap;
        }
    }

    void ForceFieldInteraction()
    {
        forcefieldVelocity = currentForcefield.CalculatePlayerVelocity(forcefieldVelocity, polarity, transform.position);

        if (isLerpingRotation)
            lerpToRotation = Vector2.SignedAngle(Vector2.down, forcefieldVelocity);
        else
            rb.rotation = Vector2.SignedAngle(Vector2.down, forcefieldVelocity);


        if(forcefieldEnterMagnitude > 0)
        {
            forcefieldEnterMagnitude -= Time.fixedDeltaTime * forcefieldVelocityLoss;
            forcefieldEnterMagnitude = Mathf.Max(0, forcefieldEnterMagnitude);
            jddVelocity = forcefieldEnterDirection * forcefieldEnterMagnitude;
        }
    }

    void CalculateForcefieldExitVelocity()
    {
        if (forcefieldExitMagnitude > 0)
        {
            //Loose Velocity Faster if entered another forcfield
            float velocityLossMultiplier = currentForcefield == null ? 20 : forcefieldVelocityLoss;

            forcefieldExitMagnitude -= Time.fixedDeltaTime * velocityLossMultiplier;
            forcefieldExitMagnitude = Mathf.Max(0, forcefieldExitMagnitude);
            forcefieldVelocity = forcefieldExitDirection * forcefieldExitMagnitude;
        }
    }

    public void Die()
    {
        isSleeping = true;
        rb.velocity = Vector2.zero;
        enabled = false;
        spriteRenderer.sprite = bloodSplash;
        spriteRenderer.color = Color.white;
        pCollider.enabled = false;

        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    

    //Handling Input System
    //---------------------------------------------------------
    public void OnMovement(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        leftStickDir = context.ReadValue<Vector2>();

        if(leftStickDir.x != 0)
        {
            walkVelocityX += leftStickDir.x * turnability;
            walkVelocityX = Mathf.Clamp(walkVelocityX, -1, 1);
        }
        else
        {
            walkVelocityX = isGrounded ? 0 : walkVelocityX * (-inAirMovementCap +1);
        }
    }

    public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            currentState = State.jump;
            timeSinceStartedJumping = 0;
            turnability = inAirMovementCap;
        }
    }

    public void OnDash(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.started && canDash )
        {
            currentState = State.dash;
            canDash = false;
            timeSinceStartedDashing = 0;
            turnability = 0;
            forcefieldExitMagnitude = 0;
            forcefieldVelocity = Vector2.zero;

            if (leftStickDir.magnitude == 0)                                    //if there is input from left stick dash
                dashDirection = transform.right;
            else
                dashDirection = leftStickDir;                                   //else dash right

        }
    }

    public void OnSwitchPolarity(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.started)
        {
            polarity = polarity == Polarity.positiv ? Polarity.negativ : Polarity.positiv;
            spriteRenderer.color = polarity == Polarity.negativ ? negativeColor : positiveColor;
        }
    }

    //Collider Stuff
    //---------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "ForceField":
                currentForcefield = collision.gameObject.GetComponent<ForceField>();
                currentState = State.forcefield;
                turnability = 0;
                canDash = true;

                Vector2 velocity = jddVelocity + new Vector2(walkVelocityX * walkSpeed, 0);
                forcefieldEnterMagnitude = velocity.magnitude;
                forcefieldEnterDirection = velocity.normalized;

                walkVelocityX = 0;

                lerpToRotation = Vector2.SignedAngle(Vector2.down, currentForcefield.position - transform.position);
                StartCoroutine(RotateOverTime(.4f, false));
                break;
            case "Spikes":
                Die();
                break;
            case "GameWon":
                SceneManager.LoadScene("GameWon");
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.tag.Equals("ForceField")){
            forcefieldExitMagnitude = forcefieldVelocity.magnitude;
            forcefieldExitDirection = forcefieldVelocity.normalized;

            currentForcefield = null;
            currentState = State.drop;
            turnability = inAirMovementCap;
            rotationLerpT = 0;

            lerpToRotation = Vector2.SignedAngle(Vector2.down, gravityDirection);

            StartCoroutine(RotateOverTime(rotateToGroundSpeed, true));
        }

        
    }
}
