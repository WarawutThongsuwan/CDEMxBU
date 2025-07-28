using UnityEngine;
using System.Collections.Generic;

public class RandomChildPositioner : MonoBehaviour
{
    public Transform parent;
    public float areaSize = 30f;
    public float minDistance = 1.0f; // ระยะห่างขั้นต่ำระหว่างตำแหน่ง

    private List<Vector3> usedPositions = new List<Vector3>();

    void Start()
    {
        if (parent == null)
        {
            Debug.LogError("Parent not assigned.");
            return;
        }

        foreach (Transform child in parent)
        {
            Vector3 position = GetUniqueRandomPosition();
            child.localPosition = position;
        }
    }

    Vector3 GetUniqueRandomPosition()
    {
        int maxAttempts = 100;
        int attempts = 0;

        float halfArea = areaSize / 2f;

        while (attempts < maxAttempts)
        {
            float x = Random.Range(-halfArea, halfArea);
            float z = Random.Range(-halfArea, halfArea);
            Vector3 candidate = new Vector3(x, 0f, z);

            if (IsFarEnough(candidate))
            {
                usedPositions.Add(candidate);
                return candidate;
            }

            attempts++;
        }

        Debug.LogWarning("Couldn't find non-overlapping position. Returning (0,0,0).");
        return Vector3.zero; // fallback ถ้าหาตำแหน่งไม่เจอ
    }

    bool IsFarEnough(Vector3 candidate)
    {
        foreach (Vector3 pos in usedPositions)
        {
            if (Vector3.Distance(candidate, pos) < minDistance)
                return false;
        }
        return true;
    }
}
