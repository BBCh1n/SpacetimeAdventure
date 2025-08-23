using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAreaTrap : TrapBase
{
    public BoxCollider2D moveArea;
    private ChainGenerator cg;

    private List<Vector2> pathPoints = new List<Vector2>();
    private int targetIndex = 1;

    private float waitTimer = 0;
    public float waitDuration = 0.5f;

    protected override void Start()
    {
        base.Start();
        GeneratePath();
        rb.MovePosition(pathPoints[0]);
        cg = GetComponent<ChainGenerator>();
        cg.GenerateChains(pathPoints);

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isWaiting)
        {
            Wait();
        }
    }

    void GeneratePath()
    {
        pathPoints.Clear();

        Bounds moveBound = moveArea.bounds;
        float sizeThreshold = 0.5f;

        Vector2 startPos = rb.position;

        if (moveBound.size.x > sizeThreshold && moveBound.size.y <= sizeThreshold)
        {
            pathPoints.Add(new Vector2(moveBound.min.x, startPos.y));
            pathPoints.Add(new Vector2(moveBound.max.x, startPos.y));
        }
        else if (moveBound.size.y > sizeThreshold && moveBound.size.x <= sizeThreshold)
        {
            pathPoints.Add(new Vector2(startPos.x, moveBound.min.y));
            pathPoints.Add(new Vector2(startPos.x, moveBound.max.y));
        }
        else if (moveBound.size.x > sizeThreshold && moveBound.size.y > sizeThreshold)
        {
            pathPoints.Add(new Vector2(moveBound.min.x, moveBound.min.y));
            pathPoints.Add(new Vector2(moveBound.max.x, moveBound.min.y));
            pathPoints.Add(new Vector2(moveBound.max.x, moveBound.max.y));
            pathPoints.Add(new Vector2(moveBound.min.x, moveBound.max.y));
        }
    }

    protected override void Move()
    {
        Vector2 currentPos = rb.position;
        Vector2 targetPos = pathPoints[targetIndex];
        
        float threshold = 0.01f;
        if (Vector2.Distance(currentPos, targetPos) < threshold)
        {
            StartWait();
            SetNextTarget();
            rb.MovePosition(targetPos);
            return;
        }

        Vector2 nextPos = Vector2.MoveTowards(currentPos, targetPos, moveSpeed * Time.fixedDeltaTime);
        Vector2 deltaPos = nextPos - currentPos;

        rb.MovePosition(nextPos);
        rc.SetObjectVel(deltaPos / Time.fixedDeltaTime);
    }

    void SetNextTarget()
    {
        targetIndex++;
        if (targetIndex >= pathPoints.Count)
        {
            targetIndex = 0;
        }
    }

    protected override void StartWait()
    {
        base.StartWait();
        waitTimer = waitDuration;
    }

    void Wait()
    {
        waitTimer -= Time.fixedDeltaTime * rc.timeScale;
        if (waitTimer <= 0)
        {
            waitTimer = 0;
            isWaiting = false;
        }
    }
}
