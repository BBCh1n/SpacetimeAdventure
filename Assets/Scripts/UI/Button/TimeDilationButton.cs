using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDilationButton : ToggleButtonBase
{
    protected override void InitActive()
    {
        isActive = GameController.Instance.hasTimeDilation;
    }

    protected override void UpdateActive()
    {
        GameController.Instance.hasTimeDilation = isActive;
    }
}
