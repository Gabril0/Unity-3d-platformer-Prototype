using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryBlock : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] float respawnRate;
    private Collider objectCollider;
    private Renderer objectRenderer;
    private bool isDisabled = false;

    private void Start()
    {
        objectCollider = GetComponent<Collider>();
        objectRenderer = GetComponent<Renderer>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && !isDisabled)
        {
            Invoke("DisableAfterDelay", duration);
            isDisabled = true;
        }
    }

    private void DisableAfterDelay()
    {

        objectCollider.enabled = false;
        objectRenderer.enabled = false;


        Invoke("EnableAfterDelay", respawnRate);
    }

    private void EnableAfterDelay()
    {

        objectCollider.enabled = true;
        objectRenderer.enabled = true;
        isDisabled = false;
    }
}
