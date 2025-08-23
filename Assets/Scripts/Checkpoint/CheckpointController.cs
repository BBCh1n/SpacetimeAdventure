using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private Animator anim;

    private Vector3 respawnOffset = new Vector3(0, -1, 0);
    public bool respawnRight = false;
    private bool isActive = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive && collision.gameObject.CompareTag("Player"))
        {
            isActive = true;
            anim.SetTrigger("Active");
            PlayerHealth.Instance.SetRespawn(transform.position + respawnOffset, respawnRight);
            AudioController.Instance.PlaySFX(AudioController.Instance.checkpointSFX);
        }
    }
}
