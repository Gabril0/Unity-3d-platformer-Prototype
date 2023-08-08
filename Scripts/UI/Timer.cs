using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Goal goal;
    private TextMeshProUGUI timer;
    void Start()
    {
        goal = GameObject.Find("Goal").GetComponent<Goal>();
        timer = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        timer.text = goal.getTimer();
    }
}
