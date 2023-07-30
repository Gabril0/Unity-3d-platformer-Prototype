using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : Collectable
{
    [SerializeField] float floatForce;
    [SerializeField] float duration;

    private Transform playerRotation;
    private bool isActive = false;

    void Start()
    {
        playerRotation = GameObject.Find("Orientation").GetComponent<Transform>();
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if (isActive)
        {
            transform.position = playerRotation.position;
            transform.rotation = playerRotation.rotation;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true;
            rotationAmount = 0;
            transform.rotation = playerRotation.rotation;
            playerRb.velocity = new Vector3(playerRb.velocity.x, floatForce * Time.deltaTime, playerRb.velocity.z);
            Invoke("destroyObject", duration);
        }
    }

    private void destroyObject() { Destroy(gameObject); }
}
