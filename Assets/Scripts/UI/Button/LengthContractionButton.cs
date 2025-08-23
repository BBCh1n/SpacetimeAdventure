using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LengthContractionButton : ToggleButtonBase
{
    protected override void InitActive()
    {
        isActive = GameController.Instance.hasLengthContraction;
    }

    protected override void UpdateActive()
    {
        GameController.Instance.hasLengthContraction = isActive;
    }
}
