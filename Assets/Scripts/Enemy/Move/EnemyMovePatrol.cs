using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovePatrol : EnemyMoveBase
{
    public float rangeY = 0;
    public float speedY = 1;
    private bool canMoveY = false;

    private float minY;
    private float maxY;
    private bool movingUp = false;

    protected override void Start()
    {
        base.Start();
        
        canMoveY = rangeY != 0;
        minY = rb.position.y;
        maxY = minY + rangeY;
    }

    protected override void Move()
    {
        if (!groundDetected || isBlocked)
        {
            if (!isStopLocked)
            {
                StartWait();
                flipAfterWait = true;
                movingUp = true;
                isStopLocked = true;
            }
            return;
        }

        StartMove();

        Vector2 currentPos = rb.position;
        float directionX = movingRight ? 1 : -1;

        float nextX = currentPos.x + directionX * moveSpeed * Time.fixedDeltaTime;
        float nextY = currentPos.y;

        if (canMoveY)
        {
            if (movingUp)
            {
                nextY += speedY * Time.fixedDeltaTime;
                if (nextY >= maxY)
                {
                    nextY = maxY;
                    movingUp = false;
                }
            }
            else
            {
                nextY -= speedY * Time.fixedDeltaTime;
                if (nextY <= minY)
                {
                    nextY = minY;
                    movingUp = true;
                }
            }
        }

        Vector2 nextPos = new Vector2(nextX, nextY);

        Vector2 deltaPos = nextPos - currentPos;
        rc.SetObjectVel(deltaPos / Time.fixedDeltaTime);

        rb.MovePosition(nextPos);
    }
}
