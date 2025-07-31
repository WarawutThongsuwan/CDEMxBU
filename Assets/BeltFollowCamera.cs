using UnityEngine;

public class BeltFollowCamera : MonoBehaviour
{
    public Transform cameraTransform;   // กล้องของ VR Player (เช่น Main Camera)
    public float yOffset = -0.8f;       // ความสูงของเข็มขัดเทียบจากกล้อง (ค่าลบเพราะอยู่ต่ำกว่า)
    public float followSpeed = 10f;     // ความเร็วในการตาม

    void Update()
    {
        if (cameraTransform == null)
            return;

        // คำนวณตำแหน่งใหม่ (X,Z ตามกล้อง, Y ตาม offset)
        Vector3 targetPosition = new Vector3(
            cameraTransform.position.x,
            cameraTransform.position.y + yOffset,
            cameraTransform.position.z
        );

        // ย้ายตำแหน่งแบบ smooth
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

        // หมุนเฉพาะในแกน Y (Yaw)
        Vector3 euler = transform.eulerAngles;
        euler.y = cameraTransform.eulerAngles.y;
        transform.eulerAngles = euler;
    }
}
