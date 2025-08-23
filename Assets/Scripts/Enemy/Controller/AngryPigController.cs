using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPigController : EnemyControllerBase
{
    private EnemyMoveBase em;

    private bool wasHitted = false;
    private float hitRate = 3;

    // Start is called before the first frame update
    void Start()
    {
        em = GetComponent<EnemyMoveBase>();
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
            case EnemyState.Hit:
                anim.SetTrigger("Hit");
                break;
        }
    }

    public override void OnHitAnimEnd()
    {
        if (!wasHitted)
        {
            wasHitted = true;
            anim.SetBool("wasHitted", true);
            em.waitDuration = 0;
            em.moveSpeed *= hitRate;
        }

        if (!eh.IsDead())
        {
            SetState(EnemyState.Move);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
