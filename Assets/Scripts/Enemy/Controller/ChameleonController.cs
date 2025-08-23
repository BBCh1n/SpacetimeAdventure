using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonController : EnemyControllerBase
{
    public BoxCollider2D tongueBC;

    // Start is called before the first frame update
    void Start()
    {
        tongueBC.enabled = false;
    }

    public override bool CanStartMove()
    {
        return currentState == EnemyState.Idle || currentState == EnemyState.Move;
    }

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
            case EnemyState.Attack:
                anim.SetTrigger("Attack");
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

    public void AttackByTongue()
    {
        tongueBC.enabled = true;
    }

    public void OnAttackAnimEnd()
    {
        tongueBC.enabled = false;
        SetState(EnemyState.Idle);
    }
}
