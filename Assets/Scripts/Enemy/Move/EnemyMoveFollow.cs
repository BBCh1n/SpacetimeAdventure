using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFollow : EnemyMoveBase
{
    private PlayerTriggerDetector ptd;
    private Transform player;
    private Rigidbody2D playerRB;

    private float prevDirectionX;

    protected override void Start()
    {
        base.Start();
        ptd = GetComponentInChildren<PlayerTriggerDetector>();
        player = PlayerController.Instance.transform;
        playerRB = player.GetComponent<Rigidbody2D>();
        prevDirectionX = player.position.x - rb.position.x;
    }
    
    protected override void Update()
    {
        base.Update();
        
        if (ptd.playerDetected && ec.IsNotHitted())
        {
            float directionX = player.position.x - transform.position.x;

            if (Mathf.Sign(directionX) != Mathf.Sign(prevDirectionX))
            {
                StartWait();
            }

            if ((directionX < 0 && movingRight) || (directionX > 0 && !movingRight))
            {
                if (!flipAfterWait)
                {
                    flipAfterWait = true;
                }
            }

            prevDirectionX = directionX;
        }
    }
    
    protected override void Move()
    {
        if (!ptd.playerDetected)
        {
            StartWait();
            return;
        }

        if (!groundDetected || isBlocked)
        {
            if (!isStopLocked)
            {
                StartWait();
                isStopLocked = true;
            }
            return;
        }

        Vector2 currentPos = rb.position;
        Vector2 targetPos = playerRB.position;
        targetPos.y = currentPos.y;

        float directionX = targetPos.x - currentPos.x;
        Vector2 moveDirection = new Vector2(Mathf.Sign(directionX), 0);
        Vector2 nextPos = currentPos + moveDirection * moveSpeed * Time.fixedDeltaTime;

        float threshold = 0.01f;
        if (Vector2.Distance(nextPos, targetPos) <= threshold)
        {
            StartWait();
            rb.MovePosition(targetPos);
            return;
        }

        StartMove();
        Vector2 deltaPos = nextPos - currentPos;
        rc.SetObjectVel(deltaPos / Time.fixedDeltaTime);
        rb.MovePosition(nextPos);
    }
}
