using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBirdController : EnemyControllerBase
{
    protected override void TriggerAnim(EnemyState newState)
    {
        switch (newState)
        {
            case EnemyState.Hit:
                anim.SetTrigger("Hit");
                break;
        }
    }

    public override void OnHitAnimEnd()
    {
        if (!eh.IsDead())
        {
            SetState(EnemyState.Idle);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
