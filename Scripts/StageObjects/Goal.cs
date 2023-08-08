using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private float time = 0;
    private bool beaten = false;
    private PlayerMovement player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (!beaten && player.getIsAlive())
        {
            // Update the start time only if the goal hasn't been beaten yet
            time += Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            beaten = true;
        }
    }
    public string getTimer()
    {

        float elapsedTime =  time;

        int minutes = (int)((elapsedTime % 3600) / 60);
        int seconds = (int)(elapsedTime % 60);
        int milliseconds = (int)((elapsedTime % 1) * 1000);

        string formattedTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        return formattedTime;
    }
    public bool isBeaten() {
        return beaten;
    }
}
