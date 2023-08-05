using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float senseX = 10f, senseY = 10f;
    private Transform orientation;
    private float mouseX, mouseY; //to get mouse input
    private float rotationY, rotationX;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        orientation = GameObject.Find("Orientation").GetComponent<Transform>();
    }

    void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senseX;
        mouseY = -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senseY;

        rotationY += mouseX;
        rotationX += mouseY;

        //clamping to prevent weird behaviour
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        orientation.rotation = Quaternion.Euler(0, rotationY, 0);


    }
}
