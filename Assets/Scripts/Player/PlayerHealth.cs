using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerController pc;

    public static int maxHP = 3;
    public int currentHP;
    public Vector3 respawnPos;
    public bool respawnRight = true;

    public float hitDuration = 1;
    private bool isHitted = false;
    private bool isDead = false;

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
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();

        currentHP = maxHP;
        StartpointController sc = StartpointController.Instance;
        SetRespawn(sc.GetStartPos(), sc.respawnRight);
        TiggerRespawn();
    }

    public void SetRespawn(Vector3 respawnPos, bool respawnRight)
    {
        this.respawnPos = respawnPos;
        this.respawnRight = respawnRight;
    }

    void TiggerRespawn()
    {
        rb.velocity = Vector2.zero;
        rb.position = respawnPos;
        transform.localRotation = Quaternion.Euler(0, respawnRight ? 0 : 180, 0);
        pc.moveRight = respawnRight;
    }

    public void GetHitted()
    {
        if (!isHitted && !isDead)
        {
            currentHP--;
            GameController.Instance.hitNum++;
            pc.canMove = false;

            if (currentHP <= 0)
            {
                isDead = true;
                GameController.Instance.deathNum++;
                anim.SetTrigger("Disappear");
                AudioController.Instance.PlaySFX(AudioController.Instance.playerDisappearSFX);
            }
            else
            {
                isHitted = true;
                TiggerRespawn();
                anim.SetTrigger("Hit");
                CameraController.Instance.TriggerShake(true);
                AudioController.Instance.PlaySFX(AudioController.Instance.playerHitSFX);
            }
        }
    }

    public void OnHitAnimEnd()
    {
        isHitted = false;
        pc.canMove = true;
        CameraController.Instance.TriggerShake(false);
    }

    public void OnDisappearAnimEnd()
    {
        SceneController.Instance.RestartLevel();
    }
}
