using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceController : MonoBehaviour
{
    public static ReferenceController Instance;

    private Transform referenceTarget;

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

    void Start()
    {
        UpdateTarget();
    }

    public void UpdateTarget()
    {
        referenceTarget = TargetController.Instance.GetTarget(GameController.Instance.referenceTarget);
    }

    public void ChangeTarget()
    {
        GameController.Instance.referenceTarget = (TargetType)(((int)GameController.Instance.referenceTarget + 1) % ((int)TargetType.Count));
        UpdateTarget();
    }

    public Vector2 GetReferenceVel()
    {
        RelativityController referenceRC = referenceTarget.GetComponent<RelativityController>();
        return referenceRC.GetObjectVel();
    }
}
