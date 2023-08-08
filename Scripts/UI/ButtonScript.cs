using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    private PlayerMovement player;
    private Image img;
    private Button btn;
    private TextMeshProUGUI child; 
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        img = GetComponent<Image>();
        btn = GetComponent<Button>();
        child = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        if (player.getIsAlive())
        {
            img.enabled = false;
            btn.enabled = false;
            child.enabled = false;
        }
        else {
            img.enabled = true;
            btn.enabled = true;
            child.enabled = true;
        }
    }
}
