using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerTriggerDetector ptd;
    private EnemyControllerBase ec;
    private RelativityController rc;

    public float attackCooldown = 1;
    private float attackTimer = 0;
    private bool canAttack = true;

    void Start()
    {
        ptd = GetComponent<PlayerTriggerDetector>();
        ec = GetComponentInParent<EnemyControllerBase>();
        rc = ec.rc;
    }

    void Update()
    {
        if (!canAttack)
        {
            attackTimer += Time.deltaTime * rc.timeScale;

            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
            }
        }
        else if (ptd.playerDetected && ec.CanStartAttack())
        {
            Attack();
        }
    }

    void Attack()
    {
        canAttack = false;
        attackTimer = 0;
        ec.SetState(EnemyState.Attack);
        AudioController.Instance.PlaySFX(AudioController.Instance.enemyAttackSFX);
    }
}
