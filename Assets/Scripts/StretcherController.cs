using UnityEngine;

public class StretcherController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    private Vector3 targetPosition;
    private bool moving = false;

    [Header("Mounting")]
    public Transform sleepPoint;
    private Transform patientToPickUp;
    private PatientStatus patientStatus;

    [Header("Zones")]
    public Transform redZone;
    public Transform yellowZone;
    public Transform greenZone;

    private enum State { Idle, ToPatient, ToZone }
    private State currentState = State.Idle;

    void Update()
    {
        if (!moving) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            Debug.Log("ถึงจุดหมายแล้ว");

            switch (currentState)
            {
                case State.ToPatient:
                    AttachPatient();
                    // ไม่ตั้ง moving = false และ currentState = Idle ที่นี่ ให้ AttachPatient เป็นคนจัดการ
                    break;

                case State.ToZone:
                    DropPatient();
                    // ตั้ง moving = false และ currentState = Idle ที่นี่
                    moving = false;
                    currentState = State.Idle;
                    break;
            }
        }
    }




    public bool IsBusy()
    {
        return currentState != State.Idle;
    }

    // เรียกใช้จาก PatientStatus.cs เพื่อให้เปลวิ่งมา
    public void SetTarget(Transform patientTransform, PatientStatus status)
    {
        patientToPickUp = patientTransform;
        patientStatus = status;
        targetPosition = patientTransform.position;
        moving = true;
        currentState = State.ToPatient;
    }

    void AttachPatient()
    {
        if (patientToPickUp == null) return;
    
        patientToPickUp.SetParent(sleepPoint);
        patientToPickUp.localPosition = Vector3.zero;
        Debug.Log($"{patientToPickUp.name} ขึ้นเปลแล้ว");
    
        TriageZoneController activeZone = TriageZoneManager.Instance.GetActiveZone();
    
        if (activeZone == null)
        {
            Debug.LogWarning("ยังไม่มี TriageZone ที่เปิดใช้งาน");
            return;
        }
    
        switch (patientStatus.triageColor)
        {
            case 3: // Red
                targetPosition = activeZone.redPoint.position;
                break;
            case 2: // Yellow
                targetPosition = activeZone.yellowPoint.position;
                break;
            case 1: // Green
                targetPosition = activeZone.greenPoint.position;
                break;
            default:
                Debug.LogWarning("ไม่รู้ว่าจะพาไปโซนไหน");
                return;
        }
    
        moving = true;
        currentState = State.ToZone;
    }

    void DropPatient()
    {
        if (patientToPickUp != null)
        {
            Debug.Log($"{patientToPickUp.name} ถูกวางในโซนแล้ว");

            patientToPickUp.SetParent(null);
            patientToPickUp.position = targetPosition;

             // เปลี่ยน status = 10
            PatientStatus status = patientToPickUp.GetComponent<PatientStatus>();
            if (status != null)
            {
                status.status = 10;
                Debug.Log($"{patientToPickUp.name} เปลี่ยนเป็นสถานะ 10 แล้ว");
            }

            patientToPickUp = null;
        }

        moving = false;
        currentState = State.Idle;

    }
}
