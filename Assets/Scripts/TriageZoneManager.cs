using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriageZoneManager : MonoBehaviour
{
    public static TriageZoneManager Instance;
    public List<TriageZoneController> allZones = new List<TriageZoneController>();
 
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // หาโซน Green ที่เปิดใช้งาน
    public Transform GetNearestActiveGreenZone(Vector3 patientPosition)
    {
        Transform bestGreen = null;
        float minDistance = Mathf.Infinity;
 
        foreach (var zone in allZones)
        {
            if (zone != null && zone.gameObject.activeInHierarchy)
            {
                float dist = Vector3.Distance(patientPosition, zone.greenPoint.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    bestGreen = zone.greenPoint;
                }
            }
        }
 
        return bestGreen;
    }
 
    public TriageZoneController GetActiveZone()
    {
        foreach (var zone in allZones)
        {
            if (zone.gameObject.activeInHierarchy)
                return zone;
        }
        return null; // ไม่มีโซนที่เปิดอยู่
    }
}
