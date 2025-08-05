using UnityEngine;

public class GunShooterSimple : MonoBehaviour
{
    [Header("Radius for calling patients")]
    public float callRadius = 2f;

    [Header("Test Call Button")]
    public bool callPatients = false;

    private bool hasCalledOnce = false;

    void Update()
    {
        // กดจาก Inspector
        if (callPatients)
        {
            callPatients = false;
            CallNearbyPatients();
        }
        
    }

    void CallNearbyPatients()
    {
        if (hasCalledOnce)
        {
            Debug.Log("เรียกไปแล้วครั้งนึง ไม่สามารถเรียกซ้ำได้");
            return;
        }

        hasCalledOnce = true;

        PatientStatus[] patients = FindObjectsOfType<PatientStatus>();
        int count = 0;
        int totalToCall = 0;

        foreach (PatientStatus p in patients)
        {
            if (p.status == 1)
                totalToCall++;
        }

        if (totalToCall == 0)
        {
            Debug.Log("ไม่มีผู้ป่วยที่ status == 1 ให้เรียก");
            return;
        }

        foreach (PatientStatus patient in patients)
        {
            if (patient.status == 1)
            {
                float angle = 360f / totalToCall * count;
                Vector3 offset = new Vector3(
                    Mathf.Cos(angle * Mathf.Deg2Rad),
                    0f,
                    Mathf.Sin(angle * Mathf.Deg2Rad)
                ) * callRadius;

                Vector3 targetPos = transform.position + offset;

                patient.MoveToPosition(targetPos);

                Debug.Log($"เรียก {patient.name} → เดินไปยัง {targetPos}");
                count++;
            }
        }
    }
}
