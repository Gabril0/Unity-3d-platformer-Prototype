using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumper : MonoBehaviour
{
    [SerializeField] float jumpForce;

    private MeshRenderer render;

    void Start()
    {
        render = GetComponent<MeshRenderer>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            render.enabled = false;
            collision.rigidbody.velocity = new Vector3(collision.rigidbody.velocity.x, jumpForce * Time.deltaTime, collision.rigidbody.velocity.z);
            Destroy(gameObject);
        }
    }
}
