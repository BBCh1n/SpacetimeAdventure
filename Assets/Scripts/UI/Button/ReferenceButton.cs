using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferenceButton : MonoBehaviour
{
    private Text buttonText;

    void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        UpdateText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnClick();
        }
    }

    public void OnClick()
    {
        ReferenceController.Instance.ChangeTarget();
        UpdateText();
        AudioController.Instance.PlayUIChange();
    }

    public void UpdateText()
    {
        buttonText.text = $"Reference (R)\n{GameController.Instance.referenceTarget}";
    }
}
