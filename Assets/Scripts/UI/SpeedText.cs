using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedText : MonoBehaviour
{
    private Text speedText;
    private RelativityController playerRC;

    private float currentSpeed;
    private float previousSpeed = -1;

    void Start()
    {
        speedText = GetComponent<Text>();
        playerRC = PlayerController.Instance.GetComponent<RelativityController>();
    }

    void LateUpdate()
    {
        currentSpeed = playerRC.relativeSpeed;

        if (currentSpeed != previousSpeed)
        {
            speedText.text = "Player Speed: " + currentSpeed.ToString("F2") + " m/s" +
                "\nLight Speed: " + GameController.lightSpeed.ToString("F2") + " m/s";
            previousSpeed = currentSpeed;
        }
    }
}
