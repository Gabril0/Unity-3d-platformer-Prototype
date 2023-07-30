using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] protected float rotationAmount;


    protected Rigidbody playerRb;

    void Start()
    {

        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationAmount);
    }
}
