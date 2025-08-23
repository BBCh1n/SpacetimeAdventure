using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIController : MonoBehaviour
{
    void Update()
    {
        if (!AboutUIController.Instance.isActive && !CutsceneUIController.Instance.isActive)
        {
            if (Input.GetButtonDown("Next"))
            {
                LoadLevel(1);
            }
            else if (Input.GetButtonDown("Back"))
            {
                LoadStart();
            }
        }
    }

    public void LoadLevel(int levelIndex)
    {
        SceneController.Instance.LoadLevel(levelIndex);
        AudioController.Instance.PlayUINext(true);
    }

    public void LoadStart()
    {
        SceneController.Instance.LoadStart();
        AudioController.Instance.PlayUINext(false);
    }
}
