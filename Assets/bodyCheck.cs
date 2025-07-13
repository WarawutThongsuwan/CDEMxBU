using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class bodyCheck : MonoBehaviour
{
    public static bool isDetect = false;
 
    [Header("UI ที่ต้องการแสดง")]
    public GameObject targetUI;  // ลิงก์ไปยัง UI Panel ใน Inspector
 
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            isDetect = true;
            Debug.Log("Trigger entered by controller");
 
            if (targetUI != null)
                targetUI.SetActive(true); // แสดง UI
        }
    }
 
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            isDetect = false;
            Debug.Log("Trigger exited by controller");
 
            if (targetUI != null)
                targetUI.SetActive(false); // ซ่อน UI
        }
    }
}