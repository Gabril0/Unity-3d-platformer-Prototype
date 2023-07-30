using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBoxes : MonoBehaviour
{
    [SerializeField] bool invertOrder;
    [SerializeField] float cooldown;

    private float lastChange = 0;
    private BoxCollider boxCollider;
    private GameObject onBox, offBox;

    private bool signal = true;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        onBox = transform.Find("On").gameObject;
        offBox = transform.Find("Off").gameObject;
    }

    void FixedUpdate()
    {

        if (Time.time < lastChange + cooldown)
        {
            if (!invertOrder)
            {
                boxCollider.enabled = signal;
                onBox.SetActive(signal);
                offBox.SetActive(!signal);
            }
            else
            {
                boxCollider.enabled = !signal;
                onBox.SetActive(!signal);
                offBox.SetActive(signal);
            }
            
        }
        else {
            lastChange = Time.time;
            signal = !signal;
        }
    }
}
