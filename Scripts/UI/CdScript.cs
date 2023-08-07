using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CdScript : MonoBehaviour
{
    [SerializeField] float rotationForce;
    private float newRotation;
    private float rotationMultiplier = 1;
    private PlayerMovement player;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    void Update()
    {
        newRotation = rotationForce * rotationMultiplier;
        speedCheck();
        //changeSpeed();
        transform.Rotate(Vector3.forward, newRotation);
    }

    private void speedCheck() {
        if (player.getMoveSpeed() > 20)
        {
            rotationMultiplier = player.getMoveSpeed() / 8;
        }
        else {
            rotationMultiplier = 1;
        }
    }
}
