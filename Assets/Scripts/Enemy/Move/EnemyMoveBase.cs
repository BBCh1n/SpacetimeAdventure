using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMoveBase : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected EnemyControllerBase ec;
    protected RelativityController rc;

    public float moveSpeed = 3;

    public bool groundDetected = true;
    public bool isBlocked = false;
    protected bool isStopLocked = false;

    public bool isWaiting = false;

    public bool flipAfterWait = false;
    public bool movingRight = false;

    public float waitDuration = 0.5f;
    protected float waitTimer = 0;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ec = GetComponent<EnemyControllerBase>();
        rc = GetComponent<RelativityController>();

        movingRight = ec.isFliped;
    }

    protected virtual void Update()
    {
        if (isWaiting)
        {
            Wait();
        }
        else if (flipAfterWait && ec.IsNotHitted())
        {
            flipAfterWait = false;
            movingRight = !movingRight;
            ec.Flip();
        }
    }

    void FixedUpdate()
    {
        if (!isWaiting && ec.CanStartMove())
        {
            Move();
        }
    }

    protected abstract void Move();

    protected void StartMove()
    {
        ec.SetState(EnemyState.Move);
        isStopLocked = false;
    }

    protected void StartWait()
    {
        isWaiting = true;
        waitTimer = waitDuration;
        ec.SetState(EnemyState.Idle);
        rb.velocity = Vector2.zero;
        rc.SetObjectVel(Vector2.zero);
    }

    protected void Wait()
    {
        waitTimer -= Time.deltaTime * rc.timeScale;

        if (waitTimer <= 0)
        {
            waitTimer = 0;
            isWaiting = false;
        }
    }
}
