using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeetDetector : MonoBehaviour
{
    private PlayerController pc;
    private int groundCount = 0;

    void Start()
    {
        pc = PlayerController.Instance;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundCount++;
            pc.isGrounded = true;
        }

        if (collision.CompareTag("EnemyHead"))
        {
            EnemyHealth eh = collision.GetComponent<EnemyHealth>();
            eh.GetHitted();
            pc.BounceUp();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundCount = Mathf.Max(0, groundCount - 1);
            if (groundCount == 0)
            {
                pc.isGrounded = false;
            }
        }
    }
}
