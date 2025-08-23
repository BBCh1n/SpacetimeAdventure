using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetector : MonoBehaviour
{
    private HorizontalTrap ht;

    void Start()
    {
        ht = GetComponentInParent<HorizontalTrap>();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            ht.isBlocked = true;
        }
    }
}
