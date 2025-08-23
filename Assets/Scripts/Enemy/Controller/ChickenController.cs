using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : EnemyControllerBase
{
    protected override void TriggerAnim(EnemyState newState)
    {
        switch (newState)
        {
            case EnemyState.Idle:
                anim.SetBool("Move", false);
                break;
            case EnemyState.Move:
                anim.SetBool("Move", true);
                break;
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
