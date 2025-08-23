using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private EnemyControllerBase ec;

    // Start is called before the first frame update
    void Start()
    {
        ec = GetComponentInParent<EnemyControllerBase>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (ec == null || ec.IsNotHitted())
            {
                PlayerHealth.Instance.GetHitted();
            }
        }
    }
}
