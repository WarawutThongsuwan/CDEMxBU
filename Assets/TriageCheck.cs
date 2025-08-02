using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriageCheck : MonoBehaviour
{
    public StretcherController stretcher; // ลากเปลมาใส่ใน Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriageTag"))
        {
            Debug.Log("แปะแล้ว");

           
        }
    }
}
