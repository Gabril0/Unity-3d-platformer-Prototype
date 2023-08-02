using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private float startTime;
    private float endTime;
    private bool showEndTime;

    void Start()
    {
        startTime = Time.time;
        showEndTime = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            endTime = Time.time;
            showEndTime = true;
        }
    }

    void OnGUI()
    {
        // Calculate the time difference between start and end
        float timeDifference = showEndTime ? endTime - startTime : Time.time - startTime;

        // Convert the time difference to a formatted string
        string timeString = "Time: " + timeDifference.ToString("F2") + " seconds";

        // Set up GUI style for the label
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;

        // Display the time on the screen at position (10, 10)
        GUI.Label(new Rect(10, 10, 300, 50), timeString, style);
    }
}
