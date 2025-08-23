using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public static TargetController Instance;

    public Transform groundTarget;
    public Transform playerTarget;
    public Transform cloudTarget;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Transform GetTarget(TargetType target)
    {
        switch (target)
        {
            case TargetType.Ground:
                return groundTarget;
            case TargetType.Player:
                return playerTarget;
            case TargetType.Cloud:
                return cloudTarget;
            default:
                return null;
        }
    }
}
