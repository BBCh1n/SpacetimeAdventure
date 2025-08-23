using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndpointController : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Active");
            AudioController.Instance.PlaySFX(AudioController.Instance.endpointSFX);
        }
    }

    public void OnPressedAnimEnd()
    {
        SceneController.Instance.LoadNextLevel();
    }
}
