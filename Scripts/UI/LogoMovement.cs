using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoMovement : MonoBehaviour
{
    private Image sprite;
    private Vector3 originalPosition;
    [SerializeField] float movementRange = 0.5f; // Adjust this value to control the movement range
    [SerializeField] float movementSpeed = 1.0f; // Adjust this value to control the movement speed
    private PlayerMovement player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        sprite = GetComponent<Image>();
        originalPosition = sprite.rectTransform.localPosition;
    }

    void Update()
    {
        if (player.getIsAlive())
        {

            float newY = originalPosition.y + Mathf.Sin(Time.time * movementSpeed) * movementRange;

            Vector3 newPosition = new Vector3(originalPosition.x, newY, originalPosition.z);
            sprite.rectTransform.localPosition = newPosition;
        }
    }
}
