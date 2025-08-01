using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stretcher1 : MonoBehaviour
{
     public float moveSpeed = 2f;             // ความเร็วในการเคลื่อนที่
    public Transform sleepPoint;             // จุดวางผู้ป่วยบนเปล

    private Vector3 targetPosition;
     public bool moving = false;
    private Transform patientToPickUp;

    public Transform redZone;
    public Transform yellowZone;


    void Start()
    {
        // ตัวอย่างการใช้ GameObject.Find โดยชื่อ (เปลี่ยนตามชื่อ GameObject จริงของคนไข้)
        GameObject patientGO = GameObject.Find("red1");
        if (patientGO != null)
        {
            SetTarget(patientGO.transform.position, patientGO.transform);
        }
        else
        {
            Debug.LogWarning("ไม่พบผู้ป่วยชื่อ RedPatient1");
        }
    }

    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                moving = false;
                Debug.Log("เปลถึงตัวผู้ป่วยแล้ว");

                if (patientToPickUp != null)
                {
                    PickUpPatient();
                }
            }
        }
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            moving = false;

            if (patientToPickUp != null && patientToPickUp.parent == sleepPoint)
            {
                patientToPickUp.SetParent(null); // วางผู้ป่วยลง
                Debug.Log("วางผู้ป่วยแล้ว");
            }
        }

    }

  public void SetTarget(Vector3 position, Transform patient)
{
      Debug.Log("เตียงกำลังมา");
    targetPosition = position;
    patientToPickUp = patient;
    moving = true;
}
    void PickUpPatient()
    {
        patientToPickUp.SetParent(sleepPoint);
        patientToPickUp.localPosition = Vector3.zero;
        Debug.Log("รับผู้ป่วยขึ้นเปลแล้ว");

        // ตรวจสอบว่าเป็น Red หรือ Yellow
        Red1Status status = patientToPickUp.GetComponent<Red1Status>();
        if (status == null)
        {
            Debug.LogError("ไม่พบ Red1Status บนตัวผู้ป่วย");
            return;
        }

        int triageLevel = status.greenYellowRedBlack;


        if (triageLevel == 3) // Red
        {
            targetPosition = redZone.position;
            Debug.Log("พาไปยัง Red Zone");
        }
        else if (triageLevel == 2) // Yellow
        {
            targetPosition = yellowZone.position;
            Debug.Log("พาไปยัง Yellow Zone");
        }
        else
        {
            Debug.LogWarning("ไม่ได้กำหนดโซนปลายทางสำหรับ triage level นี้");
            return;
        }

        moving = true;
        patientToPickUp.SetParent(sleepPoint); // ติดผู้ป่วยกับเปล
    }


    /*
        public Transform mountPoint;         // จุดวางคนไข้บนเปล
        public float moveSpeed = 2f;

        private Transform targetPatient;
        private Transform destination;

        private bool movingToPatient = false;
        private bool movingToDestination = false;

        // จุดปลายทางตาม tag
        public Transform redZone;
        public Transform yellowZone;
        public Transform greenZone;

        void Update()
        {
            if (movingToPatient && targetPatient != null)
            {
                MoveTowards(targetPatient.position);
            }
            else if (movingToDestination && destination != null)
            {
                MoveTowards(destination.position);
            }
        }

        void MoveTowards(Vector3 targetPos)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                if (movingToPatient)
                {
                    AttachPatientToStretcher();
                    SetDestinationByTag();
                    movingToPatient = false;
                    movingToDestination = true;
                }
                else if (movingToDestination)
                {
                    movingToDestination = false;
                    if (targetPatient != null)
                    {
                        targetPatient.SetParent(null); // ปล่อยคนไข้
                    }
                    targetPatient = null; // จบภารกิจ
                }
            }
        }

        void AttachPatientToStretcher()
        {
            targetPatient.SetParent(mountPoint);
            targetPatient.localPosition = Vector3.zero;
        }

        void SetDestinationByTag()
        {
            if (targetPatient.CompareTag("Red"))
                destination = redZone;
            else if (targetPatient.CompareTag("Yellow"))
                destination = yellowZone;
            else if (targetPatient.CompareTag("Green"))
                destination = greenZone;
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((other.CompareTag("Red") || other.CompareTag("Yellow") || other.CompareTag("Green")) && targetPatient == null)
            {
                targetPatient = other.transform;
                movingToPatient = true;
            }
        }
        */

}////Stretcher1
