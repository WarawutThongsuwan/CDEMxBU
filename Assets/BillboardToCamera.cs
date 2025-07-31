using UnityEngine;

public class BillboardToCamera : MonoBehaviour
{
    public Transform targetCamera;  // ตั้งกล้องที่ต้องการให้ UI หันไปหา

    void Start()
    {
        // ถ้ายังไม่ตั้งกล้อง จะใช้ main camera เป็นค่า default
        if (targetCamera == null)
        {
            if (Camera.main != null)
            {
                targetCamera = Camera.main.transform;
            }
        }
    }

    void LateUpdate()
    {
        if (targetCamera != null)
        {
            // ทำให้ UI หันหน้าตรงข้ามกล้อง (เพื่อไม่กลับด้าน)
            transform.forward = targetCamera.forward;
        }
    }
}
