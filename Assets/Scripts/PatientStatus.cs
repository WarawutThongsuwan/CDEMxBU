using UnityEngine;


public class PatientStatus : MonoBehaviour
{
    [Header("Status")]
    [Tooltip("1: Walk | 2: Bleeding | 3: Carrying | 4: Injury | 5: Breathing | 6: Call Stretcher | 7: Dead")]
    public int status = 1; // เปลี่ยนได้จาก Inspector

    [Tooltip("1=Green, 2=Yellow, 3=Red, 4=Black")]
    public int triageColor = 3;

    [Header("References")]
    public GameObject bloodEffect;

    public StretcherController assignedStretcher; // ลิงก์เปลที่รับผู้ป่วยคนนี้

    [Header("Timer")]
    public float timeCountdown = 0f;

    [Header("Green Zone")]
    public Transform greenZone; // กำหนดจาก Inspector
    public float walkSpeed = 1.5f;

    public Animator animator; // 1=เดิน, 2=นั่ง, , 3=ลุกขึ้น ,7=ตาย,

    void Update()
    {
        switch (status)
        {
            case 1:
                TimeCount();
                break;

            case 2:
                if (triageColor == 1 && greenZone != null)
                {
                    MoveToGreenZone();
                    
                }
                break;

            case 6:
                // ถ้ายังไม่มีเปล assigned หรือเปลนั้นกำลังยุ่ง
                if (assignedStretcher == null || assignedStretcher.IsBusy() == false)
                {
                    assignedStretcher = StretcherManager.Instance.GetAvailableStretcher();

                    if (assignedStretcher != null)
                    {
                        assignedStretcher.SetTarget(transform, this);
                        Debug.Log($"{gameObject.name} เรียกเปลเรียบร้อยแล้ว");
                    }
                    else
                    {
                        Debug.LogWarning($"{gameObject.name} ยังไม่มีเปลว่าง");
                    }
                }
                break;

            case 7:
                BecomeBlack();
                break;
        }

        

    }


    void TimeCount()
    {
        timeCountdown += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriageTag") && status != 6)
        {
            Debug.Log($"{gameObject.name} ชนกับ TriageTag → เปลี่ยนเป็นสถานะ 6");
            status = 6;
            PlayerScore.score += 1;
        }
    }

    void MoveToGreenZone()
    {
        Vector3 direction = (greenZone.position - transform.position).normalized;
        animator.SetInteger("movement", 2);

        // เดินไปตำแหน่ง greenZone
        transform.position = Vector3.MoveTowards(transform.position, greenZone.position, walkSpeed * Time.deltaTime);

        // หันหน้าไปทิศทางเดิน (ลบ Y เพราะไม่อยากให้เงย/ก้ม)
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // 5f = ความเร็วหมุน
        }

        float distance = Vector3.Distance(transform.position, greenZone.position);
        if (distance < 0.1f)
        {
            status = 10;
            animator.SetInteger("movement", 1);
            Debug.Log($"{gameObject.name} ถึงโซนสีเขียวแล้ว → status = 10");
        }
    }


    void BecomeBlack()
    {
        if (triageColor != 4) // ถ้ายังไม่เป็นดำ
        {
            Debug.Log($"{gameObject.name} ตาย (เป็นสีดำ)");
            triageColor = 4;

            if (animator != null)
                animator.enabled = false;

            if (bloodEffect != null)
                bloodEffect.SetActive(false);
        }
    }
}

