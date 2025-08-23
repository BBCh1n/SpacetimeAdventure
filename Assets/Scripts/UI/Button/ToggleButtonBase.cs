using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ToggleButtonBase : MonoBehaviour
{
    protected Image buttonImage;
    protected bool isActive;

    public Sprite activeSprite;
    public Sprite inactiveSprite;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        InitActive();
        UpdateButton();
    }

    public void OnClick()
    {
        isActive = !isActive;
        UpdateActive();
        UpdateButton();
        AudioController.Instance.PlayUIToggle(isActive);
    }

    protected virtual void UpdateButton()
    {
        buttonImage.sprite = isActive ? activeSprite : inactiveSprite;
    }

    protected abstract void InitActive();

    protected abstract void UpdateActive();
}
