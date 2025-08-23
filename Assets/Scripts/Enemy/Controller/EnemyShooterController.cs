using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterController : EnemyControllerBase
{
    private bool canShoot = false;
    public GameObject bulletPrefab;
    public Transform bulletParent;
    public DirectionType shootDirection;
    public Vector3 bulletOffset = Vector3.zero;

    void Start()
    {
        canShoot = bulletPrefab != null && bulletParent != null;
    }

    protected override void TriggerAnim(EnemyState newState)
    {
        switch (newState)
        {
            case EnemyState.Attack:
                anim.SetTrigger("Attack");
                break;
            case EnemyState.Hit:
                anim.SetTrigger("Hit");
                break;
        }
    }

    public void GenerateBullet()
    {
        if (canShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletParent);

            Vector3 offsetPos = bulletOffset;

            if ((shootDirection == DirectionType.Left || shootDirection == DirectionType.Right) && isFliped)
            {
                offsetPos.x *= -1;
            }

            Vector3 bulletPos = transform.position + offsetPos;
            bullet.transform.position = bulletPos;

            BulletController bc = bullet.GetComponent<BulletController>();
            bc.SetFlyDirection(shootDirection, isFliped);
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

    public void OnAttackAnimEnd()
    {
        SetState(EnemyState.Idle);
    }
}
