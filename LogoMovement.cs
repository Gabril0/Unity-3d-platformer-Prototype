using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoMovement : MonoBehaviour
{
    private Image sprite;
    private Vector3 originalPosition;
    public float movementRange = 0.5f; // Adjust this value to control the movement range
    public float movementSpeed = 1.0f; // Adjust this value to control the movement speed

    void Start()
    {
        sprite = GetComponent<Image>();
        originalPosition = sprite.rectTransform.localPosition;
    }

    void Update()
    {
        // Calculate the new position based on a sine wave
        float newY = originalPosition.y + Mathf.Sin(Time.time * movementSpeed) * movementRange;

        // Apply the new position to the sprite's local position
        Vector3 newPosition = new Vector3(originalPosition.x, newY, originalPosition.z);
        sprite.rectTransform.localPosition = newPosition;
    }
}
