using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalTrap : TrapBase
{
    public BlockDetector bd;
    public bool isBlocked = false;

    public float leftRange = 3;
    public float rightRange = 10;
    private Vector2 startPos;
    public Vector2 startDir = Vector2.right;
    private Vector2 moveDir;
    private Vector2 deltaPos = Vector2.zero;

    protected override void Start()
    {
        base.Start();
        startPos = rb.position;
        startDir = startDir.normalized;
        moveDir = startDir;
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        HitPlayer();

        if (isBlocked && !isWaiting)
        {
            StartWait();
            moveDir *= -1;
            RotateBlockDetector();
            isBlocked = false;
        }
    }

    void HitPlayer()
    {
        float detectDistance = rb.GetComponent<Collider2D>().bounds.extents.x + 0.2f;
        Vector2 boxSize = rb.GetComponent<Collider2D>().bounds.size;

        RaycastHit2D hitPlayer = Physics2D.BoxCast(rb.position, boxSize, 0f, moveDir, detectDistance, LayerMask.GetMask("Player"));
        if (hitPlayer.collider != null)
        {
            float wallCheckDistance = 0.4f;
            RaycastHit2D hitWall = Physics2D.Raycast(hitPlayer.collider.bounds.center, moveDir, wallCheckDistance, LayerMask.GetMask("Ground"));

            if (hitWall.collider != null)
            {
                PlayerHealth ph = hitPlayer.collider.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    ph.GetHitted();
                }
            }
        }
    }

    bool IsOutOfRangeX(Vector2 currentPos)
    {
        float minX = startPos.x - leftRange;
        float maxX = startPos.x + rightRange;
        return currentPos.x < minX || currentPos.x > maxX;
    }

    void ResetTrap()
    {
        rb.MovePosition(startPos);
        moveDir = startDir;
        RotateBlockDetector();
        bd.transform.localRotation = Quaternion.identity;
    }

    void RotateBlockDetector()
    {
        if (moveDir.x > 0)
        {
            bd.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveDir.x < 0)
        {
            bd.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    protected override void Move()
    {
        if (IsOutOfRangeX(rb.position))
        {
            ResetTrap();
            StartWait();
            return;
        }

        Vector2 currentPos = rb.position;
        Vector2 nextPos = currentPos + moveDir * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(nextPos);
        deltaPos = nextPos - currentPos;
        rc.SetObjectVel(deltaPos / Time.fixedDeltaTime);
    }

    protected override void StartWait()
    {
        base.StartWait();
        TriggerHitAnimation(deltaPos);
    }

    void TriggerHitAnimation(Vector2 deltaPos)
    {
        float threshold = 0.01f;

        if (Mathf.Abs(deltaPos.x) > Mathf.Abs(deltaPos.y))
        {
            if (deltaPos.x > threshold)
                anim.SetTrigger("RightHit");
            else if (deltaPos.x < -threshold)
                anim.SetTrigger("LeftHit");
        }
        else
        {
            if (deltaPos.y > threshold)
                anim.SetTrigger("TopHit");
            else if (deltaPos.y < -threshold)
                anim.SetTrigger("BottomHit");
        }
    }

    public void OnHitAnimEnd()
    {
        isWaiting = false;
    }
}
