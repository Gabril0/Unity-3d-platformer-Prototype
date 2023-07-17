using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField] float floatForce;
    [SerializeField] float duration;
    private MeshRenderer render;

    void Start()
    {
        render = GetComponent<MeshRenderer>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player")) {
            transform.position = collision.transform.position;
            render.enabled = false;
            collision.rigidbody.velocity = new Vector3(collision.rigidbody.velocity.x, floatForce * Time.deltaTime, collision.rigidbody.velocity.z);
            Invoke("destroyObject", duration);
        }
    }
    private void destroyObject() { Destroy(gameObject); }
}
