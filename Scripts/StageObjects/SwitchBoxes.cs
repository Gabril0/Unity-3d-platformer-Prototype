using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBoxes : MonoBehaviour
{
    [SerializeField] bool invertOrder;
    [SerializeField] float cooldown;

    private float lastChange = 0;
    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;

    private bool signal = true;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void FixedUpdate()
    {

        if (Time.time < lastChange + cooldown)
        {
            if (!invertOrder)
            {
                boxCollider.enabled = signal;
                meshRenderer.enabled = signal;
            }
            else
            {
                boxCollider.enabled = !signal;
                meshRenderer.enabled = !signal;
            }
            
        }
        else {
            lastChange = Time.time;
            signal = !signal;
        }
    }
}
