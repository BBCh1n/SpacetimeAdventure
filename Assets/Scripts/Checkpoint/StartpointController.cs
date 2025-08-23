using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartpointController : MonoBehaviour
{
    public static StartpointController Instance;

    private Animator anim;

    private Vector3 respawnOffset = new Vector3(0.5f, -0.5f, 0);
    public bool respawnRight = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("Active");
    }

    public Vector3 GetStartPos()
    {
        return transform.position + respawnOffset;
    }
}
