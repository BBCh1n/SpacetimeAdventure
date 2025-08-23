using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainGenerator : MonoBehaviour
{
    public GameObject chainPrefab;
    public Transform chainsParent;
    public float chainSpacing = 0.5f;

    public void GenerateChains(List<Vector2> pathPoints)
    {
        if (pathPoints != null || pathPoints.Count >= 2)
        {
            for (int i = 0; i < pathPoints.Count - 1; i++)
            {
                GenerateChainSegment(pathPoints[i], pathPoints[i + 1]);
            }

            if (pathPoints[0] != pathPoints[^1])
            {
                GenerateChainSegment(pathPoints[^1], pathPoints[0]);
            }
        }
    }

    private void GenerateChainSegment(Vector2 startPos, Vector2 endPos)
    {
        Vector2 segmentVec = endPos - startPos;
        int steps = Mathf.FloorToInt(segmentVec.magnitude / chainSpacing);
        Vector2 stepVec = segmentVec.normalized * chainSpacing;

        for (int j = 0; j < steps; j++)
        {
            Vector2 chainPos = startPos + stepVec * j;
            GameObject chain = Instantiate(chainPrefab, chainsParent);
            chain.transform.position = chainPos;
        }
    }
}
