using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    private Transform cameraPosition;
    private void Start()
    {
        cameraPosition = GameObject.Find("CameraPosition").GetComponent<Transform>();
    }
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
