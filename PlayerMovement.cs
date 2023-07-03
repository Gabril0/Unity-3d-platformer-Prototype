using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    //Base structure
    private Rigidbody rb;

    //Movement 
    private float horizontalMovement,verticalMovement;
    [SerializeField] float horizontalSpeed = 100, verticalSpeed = 1000;
    [SerializeField] float maxSpeed = 10f;
    private bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        move();

    }

    private void move() {
        directionCheck(); 
        
        if (rb.velocity.magnitude > maxSpeed) //Checks the speed to prevent extraordinary speeds
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        //horizontal movement
        rb.AddForce(Vector3.forward * Time.deltaTime * horizontalMovement * horizontalSpeed , ForceMode.VelocityChange);
        rb.AddForce(Vector3.left * Time.deltaTime * verticalMovement * horizontalSpeed, ForceMode.VelocityChange);

        //jump
        if (Input.GetKey(KeyCode.Space) && !isJumping){
            rb.AddForce(Vector3.up * Time.deltaTime * verticalSpeed, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void directionCheck() {
        verticalMovement = Input.GetAxisRaw("Vertical");
        horizontalMovement = Input.GetAxisRaw("Horizontal");
    }

    private void OnCollisionStay(Collision collision)
    {
        isJumping = false;
    }
}
