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
    private float horizontalSpeed = 100, verticalSpeed;
    private float maxSpeed = 10f;

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
        
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        rb.AddForce(Vector3.forward * Time.deltaTime * horizontalMovement * horizontalSpeed , ForceMode.VelocityChange);
        rb.AddForce(Vector3.left * Time.deltaTime * verticalMovement * horizontalSpeed, ForceMode.VelocityChange);
    }

    private void directionCheck() {
        verticalMovement = Input.GetAxisRaw("Vertical");
        horizontalMovement = Input.GetAxisRaw("Horizontal");
    }
}
