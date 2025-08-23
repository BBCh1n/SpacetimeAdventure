using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private RelativityController rc;

    public float flySpeed = 5;
    private Vector2 flyDirection;

    private bool inDestroy = false;
    private float lifeTimer = 0;
    public float maxLifeTime = 10;

    private PlayerHealth ph;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rc = GetComponent<RelativityController>();
        ph = PlayerHealth.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer += Time.deltaTime * rc.timeScale;
        if (lifeTimer > maxLifeTime)
        {
            TriggerDestroy();
        }
    }

    void FixedUpdate()
    {
        if (!inDestroy)
        {
            Vector2 currentVel = flyDirection * flySpeed;
            rb.velocity = currentVel;
            rc.SetObjectVel(currentVel);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!inDestroy)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                ph.GetHitted();
                TriggerDestroy();
            }
            else if (collision.gameObject.CompareTag("Ground"))
            {
                TriggerDestroy();
            }
        }
    }

    public void SetFlyDirection(DirectionType direction, bool isFliped)
    {
        switch (direction)
        {
            case DirectionType.Up:
                flyDirection = Vector2.up;
                break;
            case DirectionType.Down:
                flyDirection = Vector2.down;
                break;
            case DirectionType.Left:
                flyDirection = Vector2.left;
                break;
            case DirectionType.Right:
                flyDirection = Vector2.right;
                break;
        }

        if ((direction == DirectionType.Left || direction == DirectionType.Right) && isFliped)
        {
            flyDirection *= -1;
        }
    }

    void TriggerDestroy()
    {
        if (!inDestroy)
        {
            inDestroy = true;
            anim.SetTrigger("Destroy");
            rb.velocity = Vector2.zero;
            rc.SetObjectVel(Vector2.zero);
        }
    }

    public void Broken()
    {
        Destroy(gameObject);
    }
}
