using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceDetector : MonoBehaviour
{
    private PlayerController pc;
    private int wallCount = 0;

    void Start()
    {
        pc = PlayerController.Instance;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            wallCount++;
            pc.isBlocked = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            wallCount = Mathf.Max(0, wallCount - 1);
            if (wallCount == 0)
            {
                pc.isBlocked = false;
            }
        }
    }
}
