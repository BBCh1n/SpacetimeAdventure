using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFireController : MonoBehaviour
{
    private Animator anim;
    private StepDetector sd;
    public Collider2D damageArea;

    private bool isActive = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        sd = GetComponentInChildren<StepDetector>();
        damageArea.enabled = false;
    }

    void Update()
    {
        if (!isActive && sd.stepDetected)
        {
            isActive = true;
            anim.SetTrigger("Hit");
            AudioController.Instance.PlaySFX(AudioController.Instance.trapActiveSFX);
        }
    }

    public void OnHitAnimEnd()
    {
        anim.SetTrigger("On");
        damageArea.enabled = true;
    }

    public void OnFireAnimEnd()
    {
        isActive = false;
        anim.SetTrigger("Off");
        damageArea.enabled = false;
    }
}
