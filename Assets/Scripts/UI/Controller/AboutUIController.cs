using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutUIController : MonoBehaviour
{
    public static AboutUIController Instance;

    public GameObject aboutPanel;
    public bool isActive = false;

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
        aboutPanel.SetActive(false);
    }

    void Update()
    {
        if (isActive)
        {
            if (Input.GetButtonDown("Next") || Input.GetButtonDown("Back"))
            {
                CloseAbout();
            }
        }
    }

    public void OpenAbout()
    {
        isActive = true;
        aboutPanel.SetActive(true);
        AudioController.Instance.PlayUIOpen(true);
    }

    public void CloseAbout()
    {
        isActive = false;
        aboutPanel.SetActive(false);
        AudioController.Instance.PlayUIOpen(false);
    }
}
