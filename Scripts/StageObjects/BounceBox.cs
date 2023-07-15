using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BounceBox : MonoBehaviour
{
    [SerializeField] float bounceForce;

    private Rigidbody playerRb;
    void Start()
    {
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player")) { 
            Vector3 bounceDirection = collision.contacts[0].normal;
        playerRb.AddForce(-bounceDirection * bounceForce + playerRb.velocity, ForceMode.Impulse);
    }
}
}
