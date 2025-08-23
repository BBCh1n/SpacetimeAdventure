using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpeedCalculator
{
    public static Vector2 GetTargetVel()
    {
        return ReferenceController.Instance.GetReferenceVel();
    }

    public static Vector2 GetRelativeVel(Vector2 objectVel)
    {
        return objectVel - GetTargetVel();
    }

    public static Vector2 CalculateObjectVel(Vector2 prevPos, Vector2 currentPos, float deltaTime)
    {
        Vector2 deltaPos = currentPos - prevPos;
        return deltaPos / deltaTime;
    }
}
