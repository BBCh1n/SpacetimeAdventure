using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFaceDetector : MonoBehaviour
{
    private EnemyMoveBase em;

    void Start()
    {
        em = GetComponentInParent<EnemyMoveBase>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            em.isBlocked = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            em.isBlocked = false;
        }
    }
}
