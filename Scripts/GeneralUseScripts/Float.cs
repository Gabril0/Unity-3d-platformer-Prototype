using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    [SerializeField] float movementSpeed = 2;
    [SerializeField] float movementRange = 2;
    private Vector3 originalPosition;
    private void Start()
    {
        originalPosition = transform.position;
    }
    void Update()
    {
        float newY = originalPosition.y + Mathf.Sin(Time.time * movementSpeed) * movementRange;

        Vector3 newPosition = new Vector3(originalPosition.x, newY, originalPosition.z);
        transform.position = newPosition;
    }
}
