
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] Transform orientation;
    [SerializeField] float groundPoundSpeed;
    [SerializeField] float airAcceleration;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    //[SerializeField] float crouchSpeed;
    //[SerializeField] float jumpCooldown;

    //base parameters needed
    private float horizontalInput, verticalInput;
    private Rigidbody rb;
    private CapsuleCollider playerCollider;

    Vector3 direction;



    //ground check parameters
    [SerializeField] float groundDrag;
    [SerializeField] LayerMask groundLayer;
    private float playerHeight;

    //Slope Check
    private float maxSlopeAngle = 50;
    private RaycastHit slopeHit;

    //Bunny Hop
    private float timeTouchedGround;
    private float originalMoveSpeed;
    private float timerToJump = 0f;
    [SerializeField] float bunnyHopBuffer = 1f;
    [SerializeField] float bunnyHopSpeedMultiplier = 0.1f;

    //Crouch
    private float crouchYScale, startYScale;
    private float timeStartedCrouching;
    private bool crouchSpeedExtender = true; //to help with bunny hops extension
    private float speedExtenderBuffer = 1.5f;
    private bool crouchExitBuffer = false;

    //Dashing
    private float originalDashSpeed;
    private float originalDashDuration;
    private float dashHeight;

    //WallRun
    [Header("WallRun")]
    [SerializeField] LayerMask whatIsWall;
    [SerializeField] float wallRunForce;
    [SerializeField] float wallJumpUpForce;
    [SerializeField] float wallJumpSideForce;
    [SerializeField] float wallCheckDistance;
    [SerializeField] float minJumpHeight;
    [SerializeField] RaycastHit leftWallhit;
    [SerializeField] RaycastHit rightWallhit;
    private float exitWallTime = 0.2f;
    private float exitWallTimer;
    private bool exitingWall;
    private bool wallLeft;
    private bool wallRight;

    //Jumping
    private float originalJumpSpeed;

    //UI
    private Image jumpBar;
    private Image dashBar;

    //booleans
    private bool isGrounded;
    //private bool isJumpReady;
    private bool isCrounching;
    private bool justTouchedGround = false;
    private bool isDashing;
    private bool canDash = true;
    private bool isInInfinite = true;

    //Lock
    private bool groundPoundLock = false;


    void Start()
    {
        //images
        jumpBar = GameObject.Find("BunnyHopLight").GetComponent<Image>();
        dashBar = GameObject.Find("DashLight").GetComponent<Image>();

        //Physics and references
        playerCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        playerHeight = playerCollider.height;
        startYScale = transform.localScale.y;
        originalMoveSpeed = moveSpeed;
        originalDashSpeed = dashSpeed;
        originalDashDuration = dashDuration;

        crouchYScale = playerHeight / 4f;

    }

    void Update()
    {
        //Debug.Log(timerToJump < bunnyHopBuffer);
        //Debug.Log(timerToJump + " " + bunnyHopBuffer);
        groundCheck();
        checkForWall();
        //ledgeGrab();
        move();
        runUI();
    }

    private void inputAssignor() { //make inputs checks and assign them to control variables
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        bunnyHopInput();
        crouchInput();
        dashingInput();
        wallRunningInput();

        
    }
    private void dashingInput() {
        if (Input.GetKeyDown(KeyCode.LeftShift) || isDashing && canDash)
        {
            if (isCrounching && isGrounded)
            {
                superJump();
            }
            if (!canDash)
            {
                return;
            }
            else
            {
                dash();
            }

        }
    }
    private void wallRunningInput() {
        if (exitWallTimer > 0)
        {
            exitWallTimer -= Time.deltaTime;
        }
        if (exitWallTimer <= 0)
        {
            exitingWall = false;
        }
        if ((wallLeft || wallRight) && verticalInput > 0 && !isGrounded && !exitingWall)
        {


            if (!exitingWall)
            {
                wallRun();
            }
            if (Input.GetKey(KeyCode.Space))
            {
                wallJump();
            }
        }
    }
    private void crouchInput() {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouch();
            if (!isCrounching)
            {
                timeStartedCrouching = Time.time;
                crouchSpeedExtender = true;
                crouchExitBuffer = false;
            }


            moveSpeed = moveSpeed * 1.5f;

        }
        if (Time.time > timeStartedCrouching + speedExtenderBuffer)
        {
            ;
            crouchSpeedExtender = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && isCrounching)
        {
            crouchExitBuffer = true;
            if (isCrouchExitSafe())
            {
                moveSpeed = moveSpeed / 1.5f;
                transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
                isCrounching = false;
                crouchExitBuffer = false;
            }
        }

        if (crouchExitBuffer && isCrouchExitSafe() && isCrounching)
        { // to help player get up with buffer instead of crouching again
            moveSpeed = moveSpeed / 1.5f;
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            isCrounching = false;
            crouchExitBuffer = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !isGrounded)
        {
            if (!groundPoundLock)
            {
                groundPound();
            }
        }
    }
    private void bunnyHopInput() {
        if (isGrounded)
        {
            timerToJump += Time.deltaTime;
        }
        if (!isGrounded) {
            timerToJump = 0;
        }

        if ((Input.GetKeyDown(KeyCode.Space) && isGrounded))
        {  //&& isJumpReady) {

            if ((timerToJump < bunnyHopBuffer))
            {
                if ((moveSpeed < originalMoveSpeed * 2.5f) && !isInInfinite)
                {
                    moveSpeed += bunnyHopSpeedMultiplier;
                }
                if (isInInfinite)
                {
                    moveSpeed += bunnyHopSpeedMultiplier;
                }
            }

            jump();
            //Invoke(() => { isJumpReady = true; }, jumpCooldown);
        }
        else if (isGrounded && (timerToJump > bunnyHopBuffer) && !crouchSpeedExtender)
        {
            moveSpeed = originalMoveSpeed;
        }
    }

    private void move() {
        inputAssignor();
        

        direction = orientation.forward * verticalInput + orientation.right * horizontalInput; //gets the direction by combining both inputs with the rotation

        if (isGrounded) { 
            rb.AddForce(direction.normalized * moveSpeed, ForceMode.Force);
        }
        if (!isGrounded) {
            rb.AddForce(direction.normalized * moveSpeed * airAcceleration, ForceMode.Force);
        }
        //if (onSlope()) {
        //    rb.AddForce(getSlopeMoveDirection() * moveSpeed, ForceMode.Force);
        //    if (rb.velocity.y > 0) {
        //        rb.AddForce(Vector3.down , ForceMode.Force);
        //    }
        //}
        
        rb.useGravity = !onSlope();
        speedLimiter();
    }

    private void groundCheck() {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);

        if (isGrounded)
        {
            if (!justTouchedGround){
                timeTouchedGround = Time.time;
                justTouchedGround = true;
                
            }
            rb.drag = groundDrag;
            canDash = true;
            groundPoundLock = false;
            
        }
        else {
            rb.drag = 0;
        }
    }

    private void speedLimiter(){
        Vector3 flatSpeed = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //if (onSlope()){
        //    if (rb.velocity.magnitude > moveSpeed){
       //         rb.velocity = rb.velocity.normalized * moveSpeed;
        //    }
        //}
        //else
        //{
            if (flatSpeed.magnitude > moveSpeed)
            {
                
                Vector3 limitedSpeed = flatSpeed.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedSpeed.x, rb.velocity.y, limitedSpeed.z);
            }
        //}
    }
    private void jump() {

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //resets the y velocity to prevent force conflicts

        rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
            justTouchedGround = false;
        
    }

    private void crouch() {
            if (isCrouchExitSafe())
            {
                transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
                rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
                rb.AddForce(direction * 1000, ForceMode.Force);
                isCrounching = true;
            }    
    }
    private bool isCrouchExitSafe() { //to prevent the player from getting stuck
        RaycastHit hit;
        float crouchHeight = playerHeight / 2f;

        return !Physics.Raycast(transform.position, Vector3.up, out hit, crouchHeight, groundLayer);
    }

    private bool onSlope() {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f)) {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 getSlopeMoveDirection() {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    private void groundPound() {
        rb.AddForce(Vector3.down * groundPoundSpeed, ForceMode.Impulse);
        groundPoundLock = true;
    }

    private void dash() {
        if (isCrounching) {
            isDashing = false;
            return;
        }
        if (!isDashing) {
            dashSpeed = originalDashSpeed;
            dashDuration = originalDashDuration;
        }
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        isDashing = true;
        Vector3 dashOrientation = orientation.forward * dashSpeed;
        rb.AddForce(dashOrientation, ForceMode.Impulse);
        Invoke(nameof(resetDash), dashDuration);
    }
    private void resetDash() {
        if (dashSpeed > 0)
        {
            dashSpeed -= 10f * Time.deltaTime;
            dashDuration = 0;
        }
        else
        {
            dashSpeed = originalDashSpeed;
            dashDuration = originalDashDuration;
            rb.useGravity = true;
            isDashing = false;
            canDash = false;
        }
        //dashSpeed /= 3;
    }

    private void checkForWall() {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall) ;
    }

    private void wallRun() {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude) {
            wallForward = - wallForward;
        }

        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        if (!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
        {
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
        }
    }

    private void wallJump() {
        if (!Input.GetKey(KeyCode.W)) {
            exitingWall = true;
            exitWallTimer = exitWallTime;

            Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
            Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(forceToApply, ForceMode.Impulse);
        }
    }

    private void superJump() {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //resets the y velocity to prevent force conflicts

        rb.velocity = new Vector3(rb.velocity.x, jumpSpeed * 2, rb.velocity.z);
        justTouchedGround = false;
    }

    private void runUI() {
        jumpBarUI();
        dashLightUI();
    }
    private void jumpBarUI() {
        jumpBar.fillAmount = -timerToJump + bunnyHopBuffer/bunnyHopBuffer;
    }

    private void dashLightUI() {
        if (canDash)
        {
            dashBar.enabled = true;
        }
        if(!canDash || isDashing) dashBar.enabled = false;
    }

    public float getMoveSpeed() {
        return moveSpeed;
    }


    //private void OnDrawGizmos()
    //{
    //    Vector3 raycastDirection = transform.TransformDirection(Vector3.up); // Cast ray in the up direction
    //    RaycastHit hit;
    //    float crouchHeight = playerHeight / 2f;

    //    if (Physics.Raycast(transform.position, raycastDirection, out hit, crouchHeight, groundLayer))
    //    {
    //        // If the ray hits something, draw a red ray from the player's position to the hit point
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawRay(transform.position, raycastDirection * hit.distance);
    //    }
    //    else
    //    {
    //        // If the ray doesn't hit anything, draw a green ray from the player's position in the up direction with length playerHeight
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawRay(transform.position, raycastDirection * playerHeight);
    //    }
    //}


    //private void ledgeGrab()
    //{
    //    Vector3 forwardDirection = orientation.forward;
    //    Vector3 upwardBody = transform.position + forwardDirection * playerHeight * 0.5f;
    //    Vector3 lowerBody = transform.position + forwardDirection * playerHeight * 0.25f;

    //    bool hitUpper = Physics.Raycast(upwardBody, forwardDirection, out RaycastHit upperHit, 1f, groundLayer);
    //    bool hitLower = Physics.Raycast(lowerBody, forwardDirection, out RaycastHit lowerHit, 1f, groundLayer);

    //    if (hitUpper && !hitLower && !isGrounded )
    //    {
    //        // Start ledge grab
    //        Debug.Log("Ahoy!!");

    //        rb.AddForce(direction + Vector3.up * 80,ForceMode.Force);
    //    }
    //}

}
