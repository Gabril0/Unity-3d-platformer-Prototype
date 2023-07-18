using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumper : MonoBehaviour
{
    [SerializeField] float jumpForce;

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
            render.enabled = false;
            playerRb.velocity = new Vector3(playerRb.velocity.x, jumpForce * Time.deltaTime, playerRb.velocity.z);
            Destroy(gameObject);
        }
    }
}
