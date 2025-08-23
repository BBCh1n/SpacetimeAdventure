using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepDetector : MonoBehaviour
{
    public bool stepDetected = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerFeet"))
        {
            stepDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerFeet"))
        {
            stepDetected = false;
        }
    }
}
