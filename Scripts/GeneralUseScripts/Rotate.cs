using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float rotationForce = 0.5f;
    [SerializeField] bool rotateX = false, rotateY = false, rotateZ = true;

    // Update is called once per frame
    void Update()
    {
        if(rotateZ) transform.Rotate(new Vector3(0,0, rotationForce));
        if(rotateX) transform.Rotate(new Vector3(rotationForce, 0, 0));
        if(rotateY) transform.Rotate(new Vector3(0, rotationForce, 0));
    }
}
