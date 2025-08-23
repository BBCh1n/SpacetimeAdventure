using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUI : MonoBehaviour
{
    private float spacing = 100;

    public GameObject heartPrefab;
    private List<GameObject> heartList = new List<GameObject>();

    private int currHP;
    private int prevHP = -1;

    void LateUpdate()
    {
        currHP = PlayerHealth.Instance.currentHP;

        if (currHP != prevHP)
        {
            UpdateHearts();
            prevHP = currHP;
        }
    }

    void UpdateHearts()
    {
        foreach (GameObject heart in heartList)
        {
            Destroy(heart);
        }
        heartList.Clear();

        for (int i = 0; i < currHP; i++)
        {
            Vector3 heartPos = transform.position + new Vector3(i * spacing, 0, 0);
            GameObject heart = Instantiate(heartPrefab, heartPos, Quaternion.identity, transform);
            heart.name = $"Heart_{i}";
            heartList.Add(heart);
        }
    }
}
