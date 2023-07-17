using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] GameObject destination;
    [SerializeField] float boostSpeed;

    [SerializeField] bool destinationTeleportRight, destinationTeleportLeft, destinationTeleportForward, destinationTeleportBackward;
    private Vector3 direction;

    // Update is called once per frame
    void Update()
    {

        if (destinationTeleportRight) direction = Vector3.right;
        if (destinationTeleportLeft) direction = Vector3.left;
        if (destinationTeleportForward) direction = Vector3.forward;
        if (destinationTeleportBackward) direction = Vector3.back;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") ) {

            collision.transform.position = destination.transform.position + direction;
            destination.SetActive(false);
            collision.rigidbody.AddForce(direction * boostSpeed, ForceMode.Impulse);
            destination.SetActive(true);
        }
    }
}
