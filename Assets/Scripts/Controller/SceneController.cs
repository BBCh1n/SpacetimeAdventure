using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

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

    public void LoadStart()
    {
        GameController.Instance.currentLevelIndex = -1;
        AudioController.Instance.PlayBGM(-1);
        SceneManager.LoadScene("Start");
    }

    public void LoadMenu()
    {
        GameController.Instance.currentLevelIndex = 0;
        AudioController.Instance.PlayBGM(0);
        SceneManager.LoadScene("Menu");
    }

    public void LoadLevel(int levelIndex)
    {
        GameController.Instance.currentLevelIndex = levelIndex;
        GameController.Instance.UpdateLevelSettings();
        AudioController.Instance.PlayBGM(levelIndex);
        StartCoroutine(LoadSceneWithCutscene(levelIndex));
    }

    private IEnumerator LoadSceneWithCutscene(int levelIndex)
    {
        string levelName = "Level" + levelIndex;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName);
        asyncLoad.allowSceneActivation = false;

        CutsceneUIController.Instance.StartCutscene(levelIndex);

        while (CutsceneUIController.Instance.isActive)
            yield return null;

        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone)
            yield return null;

        CutsceneUIController.Instance.SetBackgroundActive(false);
        AudioController.Instance.PlaySFX(AudioController.Instance.playerAppearSFX);
    }

    public void LoadNextLevel()
    {
        int nextLevelIndex = GameController.Instance.currentLevelIndex + 1;

        if (nextLevelIndex <= GameController.maxLevelIndex)
        {
            LoadLevel(nextLevelIndex);
        }
        else
        {
            LoadMenu();
        }
    }

    public void RestartLevel()
    {
        string currentName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentName);
    }
}
