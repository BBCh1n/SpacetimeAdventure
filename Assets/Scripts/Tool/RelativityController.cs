using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativityController : MonoBehaviour
{
    private Animator anim;
    private bool hasAnim;

    public bool isStatic;

    private Vector2 objectVel;
    private Vector2 relativeVel;
    public float relativeSpeed;
    private Vector3 originalScale;

    private float c;
    private float cSquared;
    private float epsilon = 1;
    public float timeScale = 1;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        hasAnim = anim != null;

        c = GameController.lightSpeed;
        cSquared = c * c;
    }

    void LateUpdate()
    {
        SetRelative();
        LengthContraction();
        TimeDilation();
    }

    public void SetObjectVel(Vector2 velocity)
    {
        objectVel = velocity;
    }

    public Vector2 GetObjectVel()
    {
        if (isStatic)
        {
            return Vector2.zero;
        }
        else
        {
            return objectVel;
        }
    }

    void SetRelative()
    {
        relativeVel = SpeedCalculator.GetRelativeVel(GetObjectVel());

        float speedXSquared = relativeVel.x * relativeVel.x;
        float speedYSquared = relativeVel.y * relativeVel.y;

        float maxSpeed = c * 0.9f;
        if (speedXSquared + speedYSquared > maxSpeed * maxSpeed)
        {
            float scale = Mathf.Sqrt((maxSpeed * maxSpeed) / (speedXSquared + speedYSquared));
            relativeVel *= scale;
        }

        relativeSpeed = relativeVel.magnitude;
    }

    void LengthContraction()
    {
        if (GameController.Instance.hasLengthContraction && relativeSpeed > epsilon)
        {
            float factorX = Mathf.Sqrt(1 - (relativeVel.x * relativeVel.x) / cSquared);

            transform.localScale = new Vector3(originalScale.x * factorX, originalScale.y, originalScale.z);
        }
        else
        {
            transform.localScale = originalScale;
        }
    }

    void TimeDilation()
    {
        if (GameController.Instance.hasTimeDilation && relativeSpeed > epsilon)
        {
            float speedSquared = relativeSpeed * relativeSpeed;
            float factor = 1 - speedSquared / cSquared;
            timeScale = Mathf.Sqrt(factor);
        }
        else
        {
            timeScale = 1;
        }

        if (hasAnim)
        {
            anim.speed = timeScale;
        }
    }
}
