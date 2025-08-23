using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    public static CloudController Instance;

    private RelativityController rc;

    public float moveSpeed = 2;
    private float maxX = 31;
    private Vector3 startPos;

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

    // Start is called before the first frame update
    void Start()
    {
        rc = GetComponent<RelativityController>();
        rc.SetObjectVel(new Vector2(moveSpeed, 0));

        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaMove = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(deltaMove, 0, 0);

        if (transform.position.x >= maxX)
        {
            transform.position = startPos;
        }
    }
}
