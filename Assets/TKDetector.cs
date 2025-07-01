using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TKDetector : MonoBehaviour
{
    public static bool isDetect = false;

    void OnTriggerEnter(Collider other)
    {
        // ตรวจสอบว่า Collider ที่เข้ามาคือคอนโทรลเลอร์หรือสิ่งที่เราสนใจ
        if (other.CompareTag("Controller")) // คุณต้องตั้ง tag ให้คอนโทรลเลอร์เป็น "Controller"
        {
            isDetect = true;
            Debug.Log("Trigger entered by controller");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            isDetect = false;
            Debug.Log("Trigger exited by controller");
        }
    }
}
