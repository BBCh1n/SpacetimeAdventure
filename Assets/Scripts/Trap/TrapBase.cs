using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrapBase : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected RelativityController rc;

    public float moveSpeed = 3;

    protected bool isWaiting = false;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rc = GetComponent<RelativityController>();
    }

    protected virtual void FixedUpdate()
    {
        if (!isWaiting)
        {
            Move();
        }
    }

    protected abstract void Move();

    protected virtual void StartWait()
    {
        isWaiting = true;
        rb.velocity = Vector2.zero;
        rc.SetObjectVel(Vector2.zero);
    }
}
