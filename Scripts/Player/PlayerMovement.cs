using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] Transform orientation;
    [SerializeField] float maxSpeed;
    //[SerializeField] float jumpCooldown;

    //base parameters needed
    private float horizontalInput, verticalInput;
    private Rigidbody rb;

    Vector3 direction;


    //ground check parameters
    [SerializeField] float groundDrag;
    [SerializeField] LayerMask groundLayer;
    private float playerHeight;

    //booleans
    private bool isGrounded;
    private bool isJumpReady;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        playerHeight = rb.transform.localScale.y*2;
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
        if (rb.velocity.magnitude > maxSpeed) {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
    private void jump() {

            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //resets the y velocity to prevent force conflicts

            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
        
    }


}
