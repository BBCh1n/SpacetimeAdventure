using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InertiaButton : ToggleButtonBase
{
    protected override void InitActive()
    {
        isActive = GameController.Instance.hasInertia;
    }

    protected override void UpdateActive()
    {
        GameController.Instance.hasInertia = isActive;
    }
}
