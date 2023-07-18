using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField] float floatForce;
    [SerializeField] float duration;
    private MeshRenderer render;
    private Rigidbody playerRb;

    void Start()
    {
        render = GetComponent<MeshRenderer>();
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = other.transform.position;
            render.enabled = false;
            playerRb.velocity = new Vector3(playerRb.velocity.x, floatForce * Time.deltaTime, playerRb.velocity.z);
            Invoke("destroyObject", duration);
        }
    }

    private void destroyObject() { Destroy(gameObject); }
}
