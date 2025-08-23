using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    void Update()
    {
        GameController.Instance.playerTime += Time.deltaTime * PlayerController.Instance.rc.timeScale;
        GameController.Instance.worldTime += Time.deltaTime;
    }
}
