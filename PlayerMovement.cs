using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    //Base structure
    private Rigidbody rb;
    private Camera cam;

    //Movement 
    private float horizontalMovement,verticalMovement;
    [SerializeField] float horizontalSpeed = 100, verticalSpeed = 1000;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float gravityValue = 150f;
    private bool isJumping = false;
    private bool canDash = true;

    //Auxiliary variables
    private Vector3 cameraForward, cameraRight;
    private Vector3 moveDirection;

    [SerializeField] float sensitivity = 2f;
    private Vector2 rotation = Vector2.zero;

    private float lastDash;
    [SerializeField] float dashCooldown = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }

    void FixedUpdate()
    {

        move();
        

    }

    void LateUpdate()
    {
        moveCamera();
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    private void move() {
        directionCheck();



        if (rb.velocity.magnitude > maxSpeed) {  //Checks the speed to prevent extraordinary speeds
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        // Project camera's forward and right vectors onto the horizontal plane
        cameraForward = cam.transform.forward;
        cameraRight = cam.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        moveDirection = cameraForward * verticalMovement + cameraRight * horizontalMovement;

        //horizontal movement
        if (!isJumping){//ground state
            rb.AddForce(moveDirection * horizontalSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
        if (isJumping){ //airborne state
            rb.AddForce(moveDirection * horizontalSpeed * 10f * Time.deltaTime, ForceMode.Impulse);
            gravity();
        }

        //jump
        if (Input.GetKey(KeyCode.Space) && !isJumping){
            rb.AddForce(Vector3.up * verticalSpeed * Time.deltaTime, ForceMode.Impulse);
            isJumping = true;
        }

        //dash
        if (Input.GetKey(KeyCode.LeftShift) && canDash) {
            rb.AddForce(cameraForward * horizontalSpeed * 10000f * Time.deltaTime, ForceMode.Impulse);
            lastDash = Time.time;
            canDash = false;
        }
        if (Time.time > lastDash + dashCooldown) {
            canDash = true;
        }
    }

    private void directionCheck() {
        verticalMovement = Input.GetAxisRaw("Vertical");
        horizontalMovement = Input.GetAxisRaw("Horizontal");
    }

    private void moveCamera() {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Adjust the rotation
        rotation.x += mouseX * sensitivity;
        rotation.y -= mouseY * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -90f, 90f); // Limit vertical rotation to avoid flip

        // Apply rotation to the camera
        transform.localRotation = Quaternion.Euler(0f, rotation.x, rotation.y);
    }

    private void gravity() {
        
        rb.AddForce(Vector3.down * horizontalSpeed * Time.deltaTime * gravityValue, ForceMode.Acceleration);
    }



    private void OnCollisionStay(Collision collision)
    {
        isJumping = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        isJumping = true; //to prevent an additional jump if the player falls
    }
}
