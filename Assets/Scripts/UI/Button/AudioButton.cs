using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : ToggleButtonBase
{
    protected override void UpdateButton()
    {
        buttonImage.color = isActive ? Color.white : Color.gray;
    }

    protected override void InitActive()
    {
        isActive = AudioController.Instance.isActive;
    }

    protected override void UpdateActive()
    {
        AudioController.Instance.ToggleAudio();
    }
}
