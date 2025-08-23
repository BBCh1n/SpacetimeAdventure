using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private EnemyControllerBase ec;

    public int maxHP = 1;
    private int currentHP;

    void Start()
    {
        ec = GetComponentInParent<EnemyControllerBase>();
        currentHP = maxHP;
    }

    public bool GetHitted()
    {
        if (ec.IsNotHitted())
        {
            currentHP--;
            ec.SetState(EnemyState.Hit);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsDead()
    {
        return currentHP <= 0;
    }
}
