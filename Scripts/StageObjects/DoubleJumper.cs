using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumper : Collectable
{
    [SerializeField] float jumpForce;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            playerRb.velocity = new Vector3(playerRb.velocity.x, jumpForce * Time.deltaTime, playerRb.velocity.z);
            Destroy(gameObject);
        }
    }
}
