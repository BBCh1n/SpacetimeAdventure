using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    public bool isActive = true;

    public AudioSource playerBGM;
    public AudioSource playerUI;
    public AudioSource playerSFX;

    public AudioClip startBGM;
    public AudioClip menuBGM;
    public AudioClip levelBGM;

    public AudioClip changeUI;
    public AudioClip nextUI;
    public AudioClip backUI;
    public AudioClip openUI;
    public AudioClip closeUI;
    public AudioClip activeUI;
    public AudioClip inactiveUI;

    public AudioClip playerAppearSFX;
    public AudioClip playerJumpSFX;
    public AudioClip playerStepSFX;
    public AudioClip playerHitSFX;
    public AudioClip playerDisappearSFX;
    public AudioClip enemyAttackSFX;
    public AudioClip trapActiveSFX;
    public AudioClip checkpointSFX;
    public AudioClip endpointSFX;

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

    public void ToggleAudio()
    {
        isActive = !isActive;
        AudioListener.volume = isActive ? 1 : 0;
    }

    public void PlayBGM(int levelIndex)
    {
        AudioClip newClip;
        if (levelIndex == -1)
        {
            newClip = startBGM;
        }
        else if (levelIndex == 0)
        {
            newClip = menuBGM;
        }
        else
        {
            newClip = levelBGM;
        }

        if (playerBGM.clip != newClip)
        {
            playerBGM.clip = newClip;
            playerBGM.Play();
        }
    }

    public void PlayUI(AudioClip clip)
    {
        playerUI.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip)
    {
        playerSFX.PlayOneShot(clip);
    }

    public void PlayUIChange()
    {
        PlayUI(changeUI);
    }

    public void PlayUINext(bool isNext)
    {
        AudioClip clip = isNext ? nextUI : backUI;
        PlayUI(clip);
    }

    public void PlayUIOpen(bool isOpen)
    {
        AudioClip clip = isOpen ? openUI : closeUI;
        PlayUI(clip);
    }

    public void PlayUIToggle(bool isActive)
    {
        AudioClip clip = isActive ? activeUI : inactiveUI;
        PlayUI(clip);
    }
}
