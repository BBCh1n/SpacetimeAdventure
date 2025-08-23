using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUIController : MonoBehaviour
{
    void Update()
    {
        if (!AboutUIController.Instance.isActive)
        {
            if (Input.GetButtonDown("Next"))
            {
                LoadMenu();
            }
            else if (Input.GetButtonDown("Back"))
            {
                CloseGame();
            }
        }
    }

    public void LoadMenu()
    {
        SceneController.Instance.LoadMenu();
        AudioController.Instance.PlayUINext(true);
    }

    public void CloseGame()
    {
        GameController.Instance.CloseGame();
        AudioController.Instance.PlayUINext(false);
    }
}
