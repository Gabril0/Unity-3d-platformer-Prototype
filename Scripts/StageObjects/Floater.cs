using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : Collectable
{
    [SerializeField] float floatForce;
    [SerializeField] float duration;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = other.transform.position;
            playerRb.velocity = new Vector3(playerRb.velocity.x, floatForce * Time.deltaTime, playerRb.velocity.z);
            Invoke("destroyObject", duration);
        }
    }

    private void destroyObject() { Destroy(gameObject); }
}
