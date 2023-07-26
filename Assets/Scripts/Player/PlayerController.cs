using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using static PolarityExtention;
using static MathExtention;

public class PlayerController : GravityObject, PlayerInput.IPlayerActions
{
    [Header("Parameters")]
    [SerializeField] Polarity polarity;
    [SerializeField, Tooltip("shows if the Player is Grounded")] float walkSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float jumpLength;
    [SerializeField] float maxFallingSpeed;
    [SerializeField] float coyoteTime;
    [SerializeField] float jumpBuffer;
    [SerializeField] float gravityChangeBuffer;
    [SerializeField, Range(1, 10), Tooltip("multiply gravity when dropping")] float dropMultiplier;
    [SerializeField, Range(0f, 1f), Tooltip("How much can player turn when in air")] float inAirMovementCap;
    [SerializeField] float respawnTime;
    [SerializeField, Tooltip("Speed at wich player rotates towards Forcefield when entering it")] float rotateToForcefieldSpeed;
    [SerializeField] float rotateToGroundSpeed;                             //Speed at wich player rotates towards Ground after exiting forcefield
    [SerializeField] float dashDuration;
    [SerializeField] float dashSpeed;
    [SerializeField] float velocityLossInForcefield;                    //How much jddVelocity player looses per frame when inside forcefield
    [SerializeField] float forcefieldVelocityDrag;
    [SerializeField] float groundFriciton;
    [SerializeField] float groundCheckradius;
    [SerializeField] Color dashUnavailable;
    [SerializeField] Color dashAvailable;

    [HideInInspector] public bool isSleeping;                               //If Player should calculate physics and act on currentStage
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public SavePoint lastSavePoint;

    PlayerInput controls;
    ForceField currentForcefield;
    Vector2 gravityVelocity;                        //Velocity of jump/dash/drop in one 
    Vector2 dashVelocity;
    Vector2 forcefieldVelocity;
    Vector2 forcefieldExitVelocity;
    Vector2 leftStickDir;                       //Direction of the left gamepadstick or WASD input of keyboard
    Vector2 dashDirection;
    Vector2 forcefieldExitDirection;            //Direction of velocity when player is exiting a forcefield
    Vector2 forcefieldEnterDirection;           //Same as above but when entering a forcefield
    //Vector2 velocitySaveWhenSleeping;           //Save current Velocity when rotatinig camera to apply it back on after camera finished rotating

    float turnability = 1;
    float walkVelocityX;                        //horizontal Movement from input (Gamepad, Keyboard)
    float timeSinceStartedJumping;
    float timeSinceStartedDashing;
    float forcefieldExitMagnitude;              //Strength of Velocity when exiting a forcefield
    float dropMagnitudeSaver;                   //Strength of Velocity when entering a forcefield
    float dashMagnitudeSaver;
    float rotationGoal;                         //When rotating over Time determines goal to rotate to
    float rotationStart;                        //When rotating over Time determines start to rotate from
    float coyoteTimer;
    float jumpBufferTimer;
    float gravityChangeBufferTimer;
    float visualsScale = 1.13f;

    bool canDash = true;
    bool isRotating;
    bool isDying;
    public bool endedGame;

    

    [Header("References")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] Transform headCheckTransform;
    [SerializeField] Transform leftSideCheckTransform;
    [SerializeField] Transform rightSideCheckTransform;
    [SerializeField] Transform visualsTransform;
    [SerializeField] Sprite bloodSplash;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Animator animator;
    [SerializeField] Image dashUi;
    [SerializeField] GravityHandler gravityHandler;
    [SerializeField] Collider2D pCollider;
    [SerializeField] LayerMask groundLayer;                     //Layer Player can stand on (ground and gravityObject)

    public GameObject Indicator;
    public int deathCount;
    public bool death = false;


    private void Start()
    {
        if (controls == null)
        {
            controls = new PlayerInput();
            controls.Enable();
            controls.Player.SetCallbacks(this);
        }

        gravityDirection = Vector2.down;
        gravityAngle = Vector2.SignedAngle(Vector2.down, gravityDirection);

        GameEvents.gravityChangedEvent.AddListener(EndedgravityChange);
        GameEvents.prepareGravityChangeEvent.AddListener(PrepareGravityChange);

        dashUi.color = dashAvailable;
        inAirMovementCap = -inAirMovementCap + 1;   //invert value between 0-1 for better usability
        walkVelocityX = 0;
        coyoteTimer = coyoteTime + 1;
        jumpBufferTimer = jumpBuffer + 1;

        rb.centerOfMass = new Vector2(0, -pCollider.offset.y * 2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, .3f, .3f);
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

    private void Update()
    {
        CheckIfPlayerEnteredNewState();
    }

    private void FixedUpdate()
    {
        if (isSleeping)
            return;

        ActOnCurrentStage();
        Vector2 walkVelocity = Rotate90Deg(gravityDirection, true) * (walkVelocityX * walkSpeed);
        rb.velocity = walkVelocity + forcefieldVelocity + gravityVelocity + dashVelocity + forcefieldExitVelocity;
        //Debug.Log($"walk: {walkVelocity}, forcefield: {forcefieldVelocity}, gravity: {gravityVelocity}, forcefieldExit: {forcefieldExitVelocity}");
    }

    void CheckIfPlayerEnteredNewState()
    {
        isGrounded = IsGrounded();

        if (!isGrounded && currentState <= State.walk)                   //player started falling
        {
            currentState = State.drop;
            turnability = inAirMovementCap;

            CrossFade("Drop");

            StartCoroutine(CoyoteTimer());
            StartCoroutine(GravityChangeTimer());
        }
        else if (isGrounded && currentState == State.drop)               //player landed after falling
        {
            currentState = State.idle;
            timeSinceStartedDropping = 0;
            walkVelocityX = leftStickDir.x;
            canDash = true;
            dashUi.color = dashAvailable;
            turnability = 1;
            gravityVelocity = Vector2.zero;

            CrossFade("Drop Impact");

            if (jumpBufferTimer <= jumpBuffer)
                StartJumping();

            LandedOnPlattform();
            StartCoroutine(DropImpactTimer());
            ControllerRumbleManager.StartTimedRumble(.2f, .1f, .2f);

        }

        if (currentState == State.idle && walkVelocityX != 0)            //player started walking
        {
            currentState = State.walk;
            if(!isSleeping)
                CrossFade("Walk");
        }
        else if (currentState == State.walk && walkVelocityX == 0)       //player stopped walking
        {
            currentState = State.idle;
            if (!isSleeping && !isDying)
                CrossFade("Idle");
        }

        if (BumpedSide())
        {
            forcefieldExitMagnitude = 0;
            forcefieldExitVelocity = Vector2.zero;
            timeSinceStartedDashing = dashDuration + 1;
        }
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
    }

    //Bools
    //---------------------------------------------------------
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

    bool IsInbetween(float a, float b, float t)
    {
        if (b < a)
        {
            float tmp = a;
            a = b;
            b = tmp;
        }

        return t >= a && t <= b;
    }

    //Various Methods
    //---------------------------------------------------------
    void LandedOnPlattform()
    {
        if (gravityChangeBufferTimer < gravityChangeBuffer)
            return;

        //Get point where player and ground collided
        Vector2 pos = transform.position;
        Collider2D groundCollider = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckradius, groundLayer);
        Vector2 collsionPoint = groundCollider.ClosestPoint(pos);

        Vector2 newGravityDirection = collsionPoint - pos;
        newGravityDirection = SnapVectorToOrthogonal(newGravityDirection);

        float gravityAngle = Vector2.SignedAngle(Vector2.down, newGravityDirection);

        gravityHandler.StartGravityChange(gravityAngle, newGravityDirection);
    }

    void PrepareGravityChange()
    {
        isSleeping = true;
        StopCoroutinesSafely();
        rb.velocity = Vector2.zero;
    }

    void EndedgravityChange()
    {
        KillAllVelocities();
        isSleeping = false;
        StartCoroutine(RotateOverTimeLinear(rotateToForcefieldSpeed, gravityDirection));
        dashVelocity = Vector2.zero;
    }

    public void RotateOverTimePreparation(Vector2 rotationReference)
    {
        rb.rotation = Modulo(rb.rotation, 360);
        rotationStart = rb.rotation;
        rotationStart += Vector2.SignedAngle(rotationReference, Vector2.down);
        if (Mathf.Abs(rotationStart) > 180)
            rotationStart -= 360;

        rotationGoal = 0;
    }

    void CrossFade(string name)
    {
        name = (polarity == Polarity.positiv ? "Positive " : "Negative ") + name;
        animator.CrossFade(name, 0);
    }

    void KillAllVelocities()
    {
        walkVelocityX = 0;
        dashVelocity = Vector2.zero;
        gravityVelocity = Vector2.zero;
        forcefieldVelocity = Vector2.zero;
        timeSinceStartedJumping = 0;
        timeSinceStartedDashing = 0;
        forcefieldExitMagnitude = 0;
        dashMagnitudeSaver = 0;
        dropMagnitudeSaver = 0;
    }


    //Coroutines
    //---------------------------------------------------------

    void StopCoroutinesSafely()
    {
        if (isDying)
            return;


        StopAllCoroutines();
        jumpBufferTimer = jumpBuffer + 1;
        coyoteTimer = coyoteTime + 1;
        isRotating = false;
        //ControllerRumbleManager.StopRumble();

        if(forcefieldExitMagnitude > 0)
        {
            StartCoroutine(KillForcefieldExitVelocity());
        }

        if (isSleeping)
            StartCoroutine(DropImpactTimer());
    }

    IEnumerator DropImpactTimer()
    {
        yield return new WaitForSeconds(.25f);
        if(!isDying)
            CrossFade("Idle");
    }

    IEnumerator LookToForcefieldDirection()
    {
        yield return new WaitForEndOfFrame();

        bool clockwiseRotation = currentForcefield.GetClockwiseRotation();

        visualsTransform.localScale = clockwiseRotation ? new Vector3(-1, 1, 1) * visualsScale : new Vector3(1, 1, 1) * visualsScale;
    }

    IEnumerator CoyoteTimer()
    {
        coyoteTimer = 0;
        while (coyoteTimer < coyoteTime)
        {
            coyoteTimer += Time.deltaTime;
            yield return 0;
        }
        coyoteTimer = coyoteTime + 1;
    }

    IEnumerator JumpBufferTimer()
    {
        jumpBufferTimer = 0;
        while (jumpBufferTimer < jumpBuffer)
        {
            jumpBufferTimer += Time.deltaTime;
            yield return 0;
        }
        jumpBufferTimer = jumpBuffer + 1;
    }

    IEnumerator GravityChangeTimer()
    {
        gravityChangeBufferTimer = 0;
        while(gravityChangeBufferTimer < gravityChangeBuffer)
        {
            gravityChangeBufferTimer += Time.deltaTime;
            yield return 0;
        }
        gravityChangeBufferTimer = gravityChangeBuffer + 1;
    }

    IEnumerator RotateOverTimeLinear(float rotationAcceleration, Vector2 rotationReference)
    {
        RotateOverTimePreparation(rotationReference);
        if (rotationStart == rotationGoal)
            yield break;

        int rotationDir = rotationStart < rotationGoal ? 1 : -1;
        rotationAcceleration *= rotationDir;
        float rotationSpeed = rotationAcceleration;

        isRotating = true;
        float rotationAdded = 0;

        while (IsInbetween(rotationStart, rotationGoal, rotationStart + rotationAdded) && !isSleeping)
        {
            if (currentForcefield != null)
                rotationGoal = Vector2.SignedAngle(rotationReference, forcefieldVelocity);


            rotationSpeed += rotationAcceleration * (Time.fixedDeltaTime / 2);
            rotationAdded += rotationSpeed;
            rb.rotation += rotationSpeed;
            rotationSpeed += rotationAcceleration * (Time.fixedDeltaTime / 2);

            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        isRotating = false;
        rb.rotation = Vector2.SignedAngle(Vector2.down, rotationReference) + rotationGoal;
    }

    IEnumerator KillForcefieldExitVelocity()
    {
        while (forcefieldExitMagnitude > 0)
        {

            //Loose Velocity Faster if entered another forcfield
            float velocityLossMultiplier;

            if (isSleeping)
                velocityLossMultiplier = 0;
            else if (currentForcefield != null)
                velocityLossMultiplier = velocityLossInForcefield * 2;
            else if (isGrounded)
                velocityLossMultiplier = groundFriciton + 1;
            else
                velocityLossMultiplier = forcefieldVelocityDrag;

            //Debug.Log(velocityLossMultiplier * Time.deltaTime + ", " + forcefieldExitMagnitude);

            forcefieldExitMagnitude -= Time.deltaTime * velocityLossMultiplier;
            forcefieldExitMagnitude = Mathf.Max(0, forcefieldExitMagnitude);
            forcefieldExitVelocity = forcefieldExitDirection * forcefieldExitMagnitude;

            yield return 0;
        }
        forcefieldExitVelocity = Vector2.zero;
    }

    IEnumerator Respawn()
    {
        isDying = true;
        deathCount++;
        yield return new WaitForSeconds(respawnTime);
   
        if(lastSavePoint == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            yield break;
        }
        gravityHandler.StopAllCoroutines();
        KillAllVelocities();

        transform.position = lastSavePoint.transform.position;
        gravityDirection = lastSavePoint.savedGravityDir;
        gravityAngle = lastSavePoint.savedGravityAngle;
        rb.rotation = gravityAngle;

        turnability = 1;

        currentState = State.idle;
        canDash = true;
        isSleeping = false;
        isDying = false;

        GameEvents.Respawn.Invoke();
        Camera.main.transform.eulerAngles = new Vector3(0,0, gravityAngle);
    }

    //Handling States
    //---------------------------------------------------------
    void Drop()
    {
        float t = timeSinceStartedDropping + jumpLength;
        float currentDropMultiplier = forcefieldExitMagnitude > 0 ? 1 : dropMultiplier;
        float dropVelocity = CalculateVelocityGraph(t, jumpHeight, jumpLength) * currentDropMultiplier;
        dropVelocity = Mathf.Min(dropVelocity, maxFallingSpeed);

        gravityVelocity = dropVelocity / jumpLength * gravityDirection;

        timeSinceStartedDropping += Time.fixedDeltaTime;
    }

    void Jump()
    {
        float jumpVelocity = CalculateVelocityGraph(timeSinceStartedJumping, jumpHeight, jumpLength + .4f);
        gravityVelocity = jumpVelocity / jumpLength * -gravityDirection;
        timeSinceStartedJumping += Time.fixedDeltaTime;

        if (timeSinceStartedJumping > jumpLength || BumpedHead())
        {
            currentState = State.drop;
            gravityVelocity = Vector2.zero;
            CrossFade("Drop");
        }
    }

    void Dash()
    {
        float currentDashSpeed = CalculateVelocityGraph(timeSinceStartedDashing - .1f, dashSpeed, dashDuration);
        dashVelocity = currentDashSpeed * dashDirection;
        timeSinceStartedDashing += Time.fixedDeltaTime;

        if(timeSinceStartedDashing > dashDuration)
        {
            currentState = State.drop;
            dashVelocity = Vector2.zero;
            turnability = inAirMovementCap;
            CrossFade("Drop");
        }
    }

    void ForceFieldInteraction()
    {
        forcefieldVelocity = currentForcefield.CalculatePlayerVelocity(forcefieldVelocity, polarity, transform.position, forcefieldExitMagnitude);

        if (!isRotating)
            rb.rotation = Vector2.SignedAngle(Vector2.down, forcefieldVelocity);


        if(dropMagnitudeSaver > 0 || dashMagnitudeSaver > 0)
        {
            dropMagnitudeSaver -= Time.fixedDeltaTime * velocityLossInForcefield;
            dropMagnitudeSaver = Mathf.Max(0, dropMagnitudeSaver);
            gravityVelocity = forcefieldEnterDirection * dropMagnitudeSaver;

            dashMagnitudeSaver -= Time.fixedDeltaTime * dashSpeed * 5;
            dashMagnitudeSaver = Mathf.Max(0, dashMagnitudeSaver);
            dashVelocity = dashDirection * dashMagnitudeSaver;
        }
    }

    public void Die()
    {
        isSleeping = true;
        rb.velocity = Vector2.zero;
        StopCoroutinesSafely();
        CrossFade("Death");
        ControllerRumbleManager.StartTimedRumble(.5f, .7f, .25f);

        StartCoroutine(Respawn());
    }

    //Handling Input System
    //---------------------------------------------------------
    public void OnMovement(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        leftStickDir = context.ReadValue<Vector2>();
        float tmpWalkVelocity = leftStickDir.x * turnability;

        if (tmpWalkVelocity != 0)
        {
            walkVelocityX += leftStickDir.x * turnability;
            walkVelocityX = Mathf.Clamp(walkVelocityX, -1, 1);

            if(!isSleeping)
                visualsTransform.localScale = walkVelocityX > 0 ? new Vector3(1, 1, 1) * visualsScale : new Vector3(-1, 1, 1) * visualsScale;
        }
        else
        {
            walkVelocityX = isGrounded ? 0 : walkVelocityX * (-inAirMovementCap +1);
        }
    }

    public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        if (endedGame)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (isGrounded || coyoteTimer <= coyoteTime)
        {
            StartJumping();
        }
        else
        {
            StartCoroutine(JumpBufferTimer());
        }
    }

    void StartJumping()
    {
        currentState = State.jump;
        timeSinceStartedJumping = 0;
        turnability = inAirMovementCap;
        CrossFade("Jump");
        ControllerRumbleManager.StartTimedRumble(0, .2f, .1f);
    }

    public void OnDash(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.started || !canDash || currentForcefield != null)
            return;

        if (leftStickDir.magnitude == 0)
        {
            if (rb.velocity.magnitude > 0)
                dashDirection = rb.velocity.normalized;
            else
                return;
        }
        else
        {
            dashDirection = leftStickDir;
        }

        currentState = State.dash;
        canDash = false;
        dashUi.color = dashUnavailable;
        timeSinceStartedDashing = 0;
        timeSinceStartedDropping = 0;
        turnability = 0;
        forcefieldExitMagnitude = 0;
        forcefieldExitVelocity = Vector2.zero;
        forcefieldExitMagnitude = 0;
        walkVelocityX = 0;
        gravityVelocity = Vector2.zero;
        CrossFade("Jump");

        //Rotate dashdirection according to Gravity
        dashDirection = Quaternion.Euler(0f, 0f, gravityAngle) * dashDirection;
    }

    public void OnSwitchPolarity(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.started)
        {
            polarity = polarity == Polarity.positiv ? Polarity.negativ : Polarity.positiv;

            string animationName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            animationName = animationName.Substring(9);
            CrossFade(animationName);
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
                
                Indicator.SetActive(true);
               
                Vector2 dropVelocity = gravityVelocity + new Vector2(walkVelocityX * walkSpeed, 0);
                dropMagnitudeSaver = dropVelocity.magnitude;
                forcefieldEnterDirection = dropVelocity.normalized;

                dashMagnitudeSaver = dashVelocity.magnitude;
                walkVelocityX = 0;
                CrossFade("Forcefield");

                isRotating = true;
                ForceFieldInteraction();
                float rotationSpeed = currentState == State.dash ? rotateToForcefieldSpeed * 2 : rotateToForcefieldSpeed;
                StartCoroutine(RotateOverTimeLinear(rotationSpeed, forcefieldVelocity));
                StartCoroutine(LookToForcefieldDirection());
                ControllerRumbleManager.StartRumble(.05f, .2f);
                break;
            case "GameWon":
                SceneManager.LoadScene("GameWon");
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.gameObject.layer == 8)
            Die();
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
            canDash = true;
            dashUi.color = dashAvailable;

            dashVelocity = Vector2.zero;
            forcefieldVelocity = Vector2.zero;
            timeSinceStartedDropping = 0;
            CrossFade("Drop");
            ControllerRumbleManager.StopRumble();

            rotationGoal = Vector2.SignedAngle(Vector2.down, gravityDirection);

            Indicator.SetActive(false);

            StopCoroutinesSafely();
            StartCoroutine(KillForcefieldExitVelocity());
            StartCoroutine(RotateOverTimeLinear(rotateToGroundSpeed, gravityDirection));
        } 
    }

 
}