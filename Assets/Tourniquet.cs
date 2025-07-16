using UnityEngine;

public class Tourniquet : MonoBehaviour
{
    public BleedingController targetBleedingController; // เลือกออบเจกต์ A ไว้ใน Inspector

    private bool hasStopped = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasStopped) return;

        if (other.CompareTag("WoundPoint")) // Tag ของออบเจกต์ B
        {
            Debug.Log("Tourniquet touched wound");

            if (targetBleedingController != null)
            {
                targetBleedingController.StopBleeding();
            }

            hasStopped = true;
        }
    }
}
