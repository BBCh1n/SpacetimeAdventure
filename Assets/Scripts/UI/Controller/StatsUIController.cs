using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUIController : MonoBehaviour
{
    public static StatsUIController Instance;

    public GameObject statsPanel;
    public Text statsText;

    private bool isActive = false;
    private float updateInterval = 1;
    private float updateTimer = 0;

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
        statsPanel.SetActive(false);
    }

    void Update()
    {
        if (!CutsceneUIController.Instance.isActive)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                ToggleStats();
            }
        }

        updateTimer += Time.deltaTime;
        if (updateTimer >= updateInterval)
        {
            updateTimer = 0;
            UpadteStatsText();
        }
    }

    void UpadteStatsText()
    {
        statsText.text = "Your Time = " + FormatTime(GameController.Instance.playerTime) +
                    "\nWorld Time = " + FormatTime(GameController.Instance.worldTime) +
                    "\nHits = " + GameController.Instance.hitNum +
                    "\nDeaths = " + GameController.Instance.deathNum;
    }

    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ToggleStats()
    {
        isActive = !isActive;
        UpadteStatsText();
        statsPanel.SetActive(isActive);
        AudioController.Instance.PlayUIOpen(isActive);
    }

    public void ResetStats()
    {
        GameController.Instance.ResetStats();
        UpadteStatsText();
        AudioController.Instance.PlayUIChange();
    }
}
