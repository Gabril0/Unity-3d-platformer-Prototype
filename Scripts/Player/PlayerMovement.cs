using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] Transform orientation;
    [SerializeField] float maxSpeed;
    //[SerializeField] float crouchSpeed;
    //[SerializeField] float jumpCooldown;

    //base parameters needed
    private float horizontalInput, verticalInput;
    private Rigidbody rb;

    Vector3 direction;

    private float crouchYScale, startYScale;
    //ground check parameters
    [SerializeField] float groundDrag;
    [SerializeField] LayerMask groundLayer;
    private float playerHeight;

    //Slope Check
    private float maxSlopeAngle = 50;
    private RaycastHit slopeHit;

    //booleans
    private bool isGrounded;
    private bool isJumpReady;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        playerHeight = rb.transform.localScale.y*2;
        startYScale = transform.localScale.y;
    }

    void Update()
    {
        groundCheck();
        move();
    }

    private void inputAssignor() { //make inputs checks and assign them to control variables
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space) && isGrounded) {  //&& isJumpReady) {
            //isJumpReady = false;
            jump();
            //Invoke(() => { isJumpReady = true; }, jumpCooldown);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            crouch();
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void move() {
        inputAssignor();
        speedLimiter();

        direction = orientation.forward * verticalInput + orientation.right * horizontalInput; //gets the direction by combining both inputs with the rotation

        if (isGrounded) { 
            rb.AddForce(direction.normalized * moveSpeed, ForceMode.Force);
        }
        if (!isGrounded) {
            rb.AddForce(direction.normalized * moveSpeed * 0.4f, ForceMode.Force);
        }
        if (onSlope()) {
            rb.AddForce(getSlopeMoveDirection() * moveSpeed, ForceMode.Force);
            if (rb.velocity.y > 0) {
                rb.AddForce(Vector3.down, ForceMode.Force);
            }
        }
        
        rb.useGravity = !onSlope();
    }

    private void groundCheck() {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);

        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else {
            rb.drag = 0;
        }
    }

    private void speedLimiter() {
        if (onSlope())
        {
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }
        else {
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
        
    }
    private void jump() {

            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //resets the y velocity to prevent force conflicts

            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
        
    }

    private void crouch() {
        transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
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
}
