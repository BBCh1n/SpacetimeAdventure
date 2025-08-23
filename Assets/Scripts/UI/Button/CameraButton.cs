using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraButton : MonoBehaviour
{
    private Text buttonText;

    void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        UpdateText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnClick();
        }
    }

    public void OnClick()
    {
        CameraController.Instance.ChangeTarget();
        UpdateText();
        AudioController.Instance.PlayUIChange();
    }

    public void UpdateText()
    {
        buttonText.text = $"Camera (C)\n{GameController.Instance.cameraTarget}";
    }
}
