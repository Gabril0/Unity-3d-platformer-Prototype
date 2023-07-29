using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumper : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] float rotationAmount;


    private Rigidbody playerRb;

    void Start()
    {

        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationAmount);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            playerRb.velocity = new Vector3(playerRb.velocity.x, jumpForce * Time.deltaTime, playerRb.velocity.z);
            Destroy(gameObject);
        }
    }
}
