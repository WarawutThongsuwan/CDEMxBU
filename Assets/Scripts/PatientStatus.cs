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
    public Animator animator;
    public StretcherController assignedStretcher; // ลิงก์เปลที่รับผู้ป่วยคนนี้

    [Header("Timer")]
    public float timeCountdown = 0f;

    void Update()
    {
        switch (status)
        {
            case 1:
                TimeCount();
                break;

            case 6:
                if (assignedStretcher == null)
                {
                    assignedStretcher = StretcherManager.Instance.GetAvailableStretcher();

                    if (assignedStretcher != null)
                    {
                        assignedStretcher.SetTarget(transform, this);
                    }
                    else
                    {
                        Debug.LogWarning("ไม่มีเปลว่างในตอนนี้");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriageTag") && status != 6)
        {
            Debug.Log($"{gameObject.name} ชนกับ TriageTag → เปลี่ยนเป็นสถานะ 6");
            status = 6;
            PlayerScore.score += 1;
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
