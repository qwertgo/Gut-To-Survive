using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using static PolarityHandler;

public class PlayerController : GravityObject, PlayerInput.IPlayerActions
{
    [Header("Parameters")]
    [SerializeField] Polarity polarity;
    [SerializeField] float walkSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float jumpLength;
    [SerializeField] float maxFallingSpeed;
    [SerializeField, Range(1, 10)] float dropMultiplier;
    [SerializeField, Range(0f, 1f)] float inAirMovementCap;                 //How much can player turn when in air
    [SerializeField] float rotateToForcefieldSpeed;                              //Speed at wich player rotates towards Forcefield when entering it
    [SerializeField] float rotateToGroundSpeed;                             //Speed at wich player rotates towards Ground after exiting forcefield
    [SerializeField] float dashDuration;
    [SerializeField] float dashSpeed;
    [SerializeField] float dropVelocityLossInForcefield;                          //How much jddVelocity player looses per frame when inside forcefield
    [SerializeField] float groundFriciton;
    [SerializeField] float groundCheckradius;
    [SerializeField] Color negativeColor = new Color(50, 50, 255);
    [SerializeField] Color positiveColor = new Color(171, 72, 97);

    [HideInInspector] public bool isSleeping;                               //If Player should calculate physics and act on currentStage

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
    float rotationGoal = 0;                   //Needed when exiting or entering forcefield
    float rotationLerpT =0;
    float rotationStart;

    bool isGrounded;
    bool canDash = true;
    bool isRotating;

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
        //Get point where player and ground collided
        Vector2 pos = transform.position;
        Collider2D groundCollider = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckradius, groundLayer);
        Vector2 collsionPoint = groundCollider.ClosestPoint(pos);

        gravityDirection = collsionPoint - pos;

        float newPlayerRotation = Vector2.SignedAngle(Vector2.down, gravityDirection);
        rotationGoal = newPlayerRotation;
        StopCoroutine(RotateOverTime(rotateToGroundSpeed));
        isRotating = false;

        velocitySaveWhenSleeping = rb.velocity;
        gravityHandler.StartGravityChange(this, newPlayerRotation, gravityChangedEvent, rb);
    }

    void EndedgravityChange()
    {
        
        isSleeping = false;
        StartCoroutine(RotateOverTime(rotateToGroundSpeed));
        rb.velocity = velocitySaveWhenSleeping;
    }

    IEnumerator RotateOverTime(float rotationSpeed)
    {
        rotationStart = rb.rotation;
        if (rotationStart == rotationGoal)
            yield break;

        int rotationDir = rotationStart < rotationGoal ? 1: -1;
        rotationSpeed *= rotationDir;
        isRotating = true;
        
        while(IsInbetween(rotationStart, rotationGoal, rb.rotation))
        {
            rb.rotation += Time.fixedDeltaTime * rotationSpeed;

            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        isRotating = false;
        rb.rotation = rotationGoal;
    }

    bool IsInbetween(float a, float b, float t)
    {
        if(b < a)
        {
            float tmp = a;
            a = b;
            b = tmp;
        }

        //Debug.Log($"a: {a}, b: {b}, t: {t}");
        //Debug.Log((t >= a && t <= b) + ",  insgesamt");
        ////Debug.Log((t >= a) + ", erster Teil");
        ////Debug.Log((t <= b) + ", zweiter Teil");
        //Debug.Log("-------------------------------------");

        return t >= a && t <= b;
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

        if (isRotating)
        {
            float tmpRotGoal = Vector2.SignedAngle(Vector2.down, forcefieldVelocity);
            float t = Mathf.Abs(rotationGoal - tmpRotGoal);
            if (t >= 350)
            {
                rotationStart = tmpRotGoal < 0 ? -180 : 180;
                rotationGoal = tmpRotGoal;
                Debug.Log("changed RotationStart");
                Debug.Log($"start: {rotationStart}, end: {rotationGoal}, rbRotation: {rb.rotation} ");
            }

            rotationGoal = tmpRotGoal;
        }
        else
            rb.rotation = Vector2.SignedAngle(Vector2.down, forcefieldVelocity);


        if(forcefieldEnterMagnitude > 0)
        {
            forcefieldEnterMagnitude -= Time.fixedDeltaTime * dropVelocityLossInForcefield;
            forcefieldEnterMagnitude = Mathf.Max(0, forcefieldEnterMagnitude);
            jddVelocity = forcefieldEnterDirection * forcefieldEnterMagnitude;
        }
    }

    void CalculateForcefieldExitVelocity()
    {
        if (forcefieldExitMagnitude > 0)
        {
            //Loose Velocity Faster if entered another forcfield
            float velocityLossMultiplier;

            if (currentForcefield != null)
                velocityLossMultiplier = dropVelocityLossInForcefield;
            else if (isGrounded)
                velocityLossMultiplier = groundFriciton + 1;
            else
                velocityLossMultiplier = 20;

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
        if (context.started && canDash && currentForcefield == null)
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

            //Rotate dashdirection according to Gravity
            dashDirection = Quaternion.Euler(0f, 0f, gravityAngle) * dashDirection;
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

                isRotating = true;
                ForceFieldInteraction();
                StartCoroutine(RotateOverTime(rotateToForcefieldSpeed));
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

            rotationGoal = Vector2.SignedAngle(Vector2.down, gravityDirection);

            StartCoroutine(RotateOverTime(rotateToGroundSpeed));
        } 
    }
}
