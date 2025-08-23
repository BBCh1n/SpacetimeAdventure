using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public int currentLevelIndex = -1;
    public static int maxLevelIndex = 3;

    public static float lightSpeed = 10;

    public TargetType cameraTarget = TargetType.Ground;
    public TargetType referenceTarget = TargetType.Ground;
    public bool hasInertia = true;
    public bool hasLengthContraction = false;
    public bool hasTimeDilation = false;
    
    public float playerTime = 0;
    public float worldTime = 0;
    public int hitNum = 0;
    public int deathNum = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void UpdateLevelSettings()
    {
        cameraTarget = TargetType.Ground;
        referenceTarget = TargetType.Ground;
        hasInertia = true;
        hasLengthContraction = currentLevelIndex >= 2;
        hasTimeDilation = currentLevelIndex >= 3;
    }

    public void SetPause(bool isPaused)
    {
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void ResetStats()
    {
        playerTime = 0;
        worldTime = 0;
        hitNum = 0;
        deathNum = 0;
    }
}
