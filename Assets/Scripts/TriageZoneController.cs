using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriageZoneController : MonoBehaviour
{
    public Transform redPoint;
    public Transform yellowPoint;
    public Transform greenPoint;
 
    public bool isActive = false; // ให้กำหนดว่า zone นี้กำลังเปิดอยู่
 
    private void OnDrawGizmos()
    {
        if (isActive)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position + Vector3.up * 0.5f, new Vector3(5, 1, 5));
        }
    }
}
