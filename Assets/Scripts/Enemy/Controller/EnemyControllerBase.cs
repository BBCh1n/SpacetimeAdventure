using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyControllerBase : MonoBehaviour
{
    public Animator anim;
    private Rigidbody2D rb;
    public RelativityController rc;
    protected EnemyHealth eh;

    public EnemyState currentState = EnemyState.Idle;
    public bool isFliped = false;
    public float flipOffset = 0;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rc = GetComponent<RelativityController>();
        eh = GetComponentInChildren<EnemyHealth>();

        if (transform.localRotation.y == 0)
        {
            isFliped = false;
        }
        else
        {
            isFliped = true;
        }
    }

    public virtual bool IsNotHitted()
    {
        return currentState != EnemyState.Hit;
    }

    public virtual bool CanStartMove()
    {
        return currentState == EnemyState.Idle || currentState == EnemyState.Move || currentState == EnemyState.Attack;
    }

    public virtual bool CanStartAttack()
    {
        return currentState == EnemyState.Idle || currentState == EnemyState.Move;
    }

    public void SetState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            TriggerAnim(newState);
        }
    }

    protected abstract void TriggerAnim(EnemyState newState);

    public void Flip()
    {
        isFliped = !isFliped;

        if (!isFliped)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0) ;
            rb.position -= new Vector2(flipOffset, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            rb.position += new Vector2(flipOffset, 0);
        }
    }

    public abstract void OnHitAnimEnd();
}
