using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlUIController : MonoBehaviour
{
    public static ControlUIController Instance;

    public GameObject controlPanel;
    private bool isActive = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        controlPanel.SetActive(false);
    }

    void Update()
    {
        if (!CutsceneUIController.Instance.isActive)
        {
            if (Input.GetButtonDown("Back"))
            {
                ToggleControl();
            }
        }
    }

    public void ToggleControl()
    {
        isActive = !isActive;
        controlPanel.SetActive(isActive);
        GameController.Instance.SetPause(isActive);
        AudioController.Instance.PlayUIOpen(isActive);
    }

    public void RestartLevel()
    {
        GameController.Instance.SetPause(false);
        SceneController.Instance.RestartLevel();
        AudioController.Instance.PlayUINext(true);
    }

    public void LoadMenu()
    {
        GameController.Instance.SetPause(false);
        SceneController.Instance.LoadMenu();
        AudioController.Instance.PlayUINext(false);
    }
}
