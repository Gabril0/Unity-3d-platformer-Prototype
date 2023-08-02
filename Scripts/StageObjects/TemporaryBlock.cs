using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryBlock : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] float respawnRate;
    private Collider objectCollider;
    private MeshRenderer model;
    private bool isDisabled = false;

    private void Start()
    {
        objectCollider = GetComponent<Collider>();
        model = GetComponentInChildren<MeshRenderer>();

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
        //model.SetActive(false);
        model.enabled = false;


        Invoke("EnableAfterDelay", respawnRate);
    }

    private void EnableAfterDelay()
    {

        objectCollider.enabled = true;
        //model.SetActive(true);
        model.enabled = true;
        isDisabled = false;
    }
}
