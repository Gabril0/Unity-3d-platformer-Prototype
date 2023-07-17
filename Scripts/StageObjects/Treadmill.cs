using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool forward;
    [SerializeField] bool reverse;
    private Vector3 direction;

    private Rigidbody playerRb;

    void Start()
    {
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }
    private void Update()
    {

        if (forward && !reverse)
        {
            direction = Vector3.forward;
        }
        else if (reverse && !forward)
        {
            direction = Vector3.back;
        }
        else
        {
            direction = Vector3.zero;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        
        if (collision.collider.CompareTag("Player")) {
            playerRb.AddForce(direction * speed, ForceMode.Force);
        }
    }
}
