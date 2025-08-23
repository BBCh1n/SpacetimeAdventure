using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerDetector : MonoBehaviour
{
    public bool playerDetected = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }
}
