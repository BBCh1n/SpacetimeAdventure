using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    private Camera mainCamera;
    private Transform cameraTarget;

    private Vector3 targetOffset;

    private Vector3 shakeOffset = Vector3.zero;

    public bool isShaking = false;
    private float shakeDuration;
    private float shakeMagnitude = 0.2f;

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
        mainCamera = Camera.main;
        mainCamera.orthographicSize = 12;
        UpdateTarget();
        shakeDuration = PlayerHealth.Instance.hitDuration;
    }

    void Update()
    {
        if (isShaking)
        {
            Shake();
        }
    }

    void LateUpdate()
    {
        transform.position = cameraTarget.position + targetOffset + shakeOffset;
    }

    public void UpdateTarget()
    {
        cameraTarget = TargetController.Instance.GetTarget(GameController.Instance.cameraTarget);
        switch (GameController.Instance.cameraTarget)
        {
            case TargetType.Ground:
            case TargetType.Player:
                targetOffset = new Vector3(0, 5, -10);
                break;

            case TargetType.Cloud:
                targetOffset = new Vector3(-15.5f, -1.5f, -10);
                break;
        }
    }

    public void ChangeTarget()
    {
        GameController.Instance.cameraTarget = (TargetType)(((int)GameController.Instance.cameraTarget + 1) % ((int)TargetType.Count));
        UpdateTarget();
    }

    public void TriggerShake(bool isShaking)
    {
        this.isShaking = isShaking;
        if (!isShaking)
        {
            shakeOffset = Vector3.zero;
        }
    }

    void Shake()
    {
        float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
        float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;
        shakeOffset = new Vector3(offsetX, offsetY, 0);
    }
}
