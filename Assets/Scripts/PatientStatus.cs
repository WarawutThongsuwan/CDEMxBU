using UnityEngine;


public class PatientStatus : MonoBehaviour
{
    [Header("Status")]
    [Tooltip("1: CanWalk | 2: CannotWalk | 3: Breeding | 4: Dead | 5: NotBreathing | 6: yellow2 | 7: ")]
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
    public float walkSpeed = 0.5f;

    private float stretcherCheckCooldown = 2f; // เช็คทุก 2 วิ
    private float lastCheckTime = 0f;

    public Animator animator; // 1=ยืน, 2=เดิน, 3=นอน , 4=ตาย, 5=ท่าเปิดทางเดินหายใจ

    public GameObject objectYellow;
    public GameObject objectNormal;
    static public bool isScoreCounting;

    void Update()
    {
        switch (status)
        {
            case 1://ถ้าเดินได้
                if (triageColor == 1)//ถ้าแปะสีเขียว
                {
                    
                    if (greenZone == null)
                    {
                        greenZone = TriageZoneManager.Instance.GetNearestActiveGreenZone(transform.position);
                        if (greenZone == null)
                        {
                            Debug.LogWarning($"{gameObject.name} ยังไม่มี GreenZone ที่เปิดใช้งานให้เดินไป");
                            return;
                        }
                    }
                    // isScoreCounting = true;
                    MoveToGreenZone();
                }

                if (triageColor == 2) //ถ้าป้ายมาแปะเป็นสีเหลืองจะไม่ได้คะแนน
                {
                    animator.SetInteger("movement", 3);
                    if (Time.time - lastCheckTime >= stretcherCheckCooldown) //check stretcher
                    {
                        lastCheckTime = Time.time;

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
                    }
                }

                if (triageColor == 3) //ถ้าป้ายมาแปะเป็นสีแดงจะไม่ได้คะแนน
                {
                    animator.SetInteger("movement", 3);
                    if (Time.time - lastCheckTime >= stretcherCheckCooldown) //check stretcher
                    {
                        lastCheckTime = Time.time;

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
                    }
                }

                break;

            case 2://ถ้าเดินไม่ได้
                animator.SetInteger("movement", 3);
                if (triageColor == 2) //ถ้าป้ายมาแปะเป็นสีเหลืองจะได้คะแนน
                {
                    
                    if (Time.time - lastCheckTime >= stretcherCheckCooldown) //check stretcher
                    {
                        lastCheckTime = Time.time;

                        if (assignedStretcher == null || assignedStretcher.IsBusy() == false)
                        {
                            assignedStretcher = StretcherManager.Instance.GetAvailableStretcher();

                            if (assignedStretcher != null)
                            {
                                ScoreManager.Instance.AddScoreAuto(10);
                                assignedStretcher.SetTarget(transform, this);
                                Debug.Log($"{gameObject.name} เรียกเปลเรียบร้อยแล้ว");
                            }
                            else
                            {
                                Debug.LogWarning($"{gameObject.name} ยังไม่มีเปลว่าง");
                            }
                        }
                    }
                }

                if (triageColor == 3) //ถ้าป้ายมาแปะเป็นสีแดงจะไม่ได้คะแนน
                {
                    if (Time.time - lastCheckTime >= stretcherCheckCooldown) //check stretcher
                    {
                        lastCheckTime = Time.time;

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
                    }
                }
                TimeCount();
                if (timeCountdown >= 180)
                {
                    status = 5;
                }
                break;

            case 3://ถ้าเลือดออก
                TimeCount();
                animator.SetInteger("movement", 3);

                if (triageColor == 2) //ถ้าป้ายมาแปะเป็นสีเหลืองจะไม่ได้คะแนน
                {
                    if (Time.time - lastCheckTime >= stretcherCheckCooldown) //check stretcher
                    {
                        lastCheckTime = Time.time;

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
                    }
                }

                if (triageColor == 3) //ถ้าป้ายมาแปะเป็นสีแดงจะได้คะแนน
                {
                    
                    if (Time.time - lastCheckTime >= stretcherCheckCooldown) //check stretcher
                    {
                        lastCheckTime = Time.time;

                        if (assignedStretcher == null || assignedStretcher.IsBusy() == false)
                        {
                            assignedStretcher = StretcherManager.Instance.GetAvailableStretcher();

                            if (assignedStretcher != null)
                            {
                                ScoreManager.Instance.AddScoreAuto(10);
                                assignedStretcher.SetTarget(transform, this);
                                Debug.Log($"{gameObject.name} เรียกเปลเรียบร้อยแล้ว");
                            }
                            else
                            {
                                Debug.LogWarning($"{gameObject.name} ยังไม่มีเปลว่าง");
                            }
                        }
                    }
                }
                if (timeCountdown >= 115)
                {
                    animator.SetInteger("movement", 4);
                    if (timeCountdown >= 120)
                    {
                        status = 4;
                    }
                }
                break;

            case 4://ถ้าตายแล้ว
                TimeCount();

                animator.SetInteger("movement", 4);
                if (triageColor == 2) //ถ้าป้ายมาแปะเป็นสีเหลืองจะไม่ได้คะแนน
                {
                    if (Time.time - lastCheckTime >= stretcherCheckCooldown) //check stretcher
                    {
                        lastCheckTime = Time.time;

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
                    }
                }

                if (triageColor == 3) //ถ้าป้ายมาแปะเป็นสีแดงจะไม่ได้คะแนน
                {
                    if (Time.time - lastCheckTime >= stretcherCheckCooldown) //check stretcher
                    {
                        lastCheckTime = Time.time;

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
                    }
                }

                if (triageColor == 4) //ถ้าป้ายมาแปะเป็นสีดำจะได้คะแนน
                {
                    ScoreManager.Instance.AddScoreAuto(10);
                }
                BecomeBlack();
                break;

            case 5:
                TimeCount();
                if (objectYellow != null)
                {
                    objectYellow.SetActive(true);
                }
                if (objectNormal != null)
                {
                    objectNormal.SetActive(false);
                }
                if (triageColor == 2) //ถ้าป้ายมาแปะเป็นสีเหลืองจะไม่ได้คะแนน
                {
                    if (Time.time - lastCheckTime >= stretcherCheckCooldown) //check stretcher
                    {
                        lastCheckTime = Time.time;

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
                    }
                }

                if (triageColor == 3) //ถ้าป้ายมาแปะเป็นสีแดงจะได้คะแนน
                {
                    
                    if (Time.time - lastCheckTime >= stretcherCheckCooldown) //check stretcher
                    {
                        lastCheckTime = Time.time;

                        if (assignedStretcher == null || assignedStretcher.IsBusy() == false)
                        {
                            assignedStretcher = StretcherManager.Instance.GetAvailableStretcher();

                            if (assignedStretcher != null)
                            {
                                ScoreManager.Instance.AddScoreAuto(10);
                                assignedStretcher.SetTarget(transform, this);
                                Debug.Log($"{gameObject.name} เรียกเปลเรียบร้อยแล้ว");
                            }
                            else
                            {
                                Debug.LogWarning($"{gameObject.name} ยังไม่มีเปลว่าง");
                            }
                        }
                    }
                }
                if (timeCountdown >= 295)
                {
                    animator.SetInteger("movement", 4);
                    if (timeCountdown >= 300)
                    {
                        status = 4;
                    }
                }
                break;
            case 6://ถ้าเดินไม่ได้ และไม่เจ็บมาก
                animator.SetInteger("movement", 3);
                if (triageColor == 2) //ถ้าป้ายมาแปะเป็นสีเหลืองจะได้คะแนน
                {
                    
                    if (Time.time - lastCheckTime >= stretcherCheckCooldown) //check stretcher
                    {
                        lastCheckTime = Time.time;

                        if (assignedStretcher == null || assignedStretcher.IsBusy() == false)
                        {
                            assignedStretcher = StretcherManager.Instance.GetAvailableStretcher();

                            if (assignedStretcher != null)
                            {
                                ScoreManager.Instance.AddScoreAuto(10);
                                assignedStretcher.SetTarget(transform, this);
                                Debug.Log($"{gameObject.name} เรียกเปลเรียบร้อยแล้ว");
                            }
                            else
                            {
                                Debug.LogWarning($"{gameObject.name} ยังไม่มีเปลว่าง");
                            }
                        }
                    }
                }

                if (triageColor == 3) //ถ้าป้ายมาแปะเป็นสีแดงจะไม่ได้คะแนน
                {
                    if (Time.time - lastCheckTime >= stretcherCheckCooldown) //check stretcher
                    {
                        lastCheckTime = Time.time;

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
                    }
                }

                break;
            case 7:
                TimeCount();
                animator.SetInteger("movement", 4);
                if (timeCountdown >= 5)
                {
                    status = 4;
                }
                break;
        }

        

    }


    void TimeCount()
    {
        timeCountdown += Time.deltaTime;
    }

    //void OnTriggerEnter(Collider other)
    //{
        //if (other.CompareTag("TriageTag") && status != 6)
        //{
            //Debug.Log($"{gameObject.name} ชนกับ TriageTag → เปลี่ยนเป็นสถานะ 6");
            //status = 6;
            //PlayerScore.score += 1;
        //}
    //}

    void MoveToGreenZone()
    {
        //ScoreManager.Instance.AddScoreAuto(10);
        //isScoreCounting = false;
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
            ScoreManager.Instance.AddScoreAuto(10);
            animator.SetInteger("movement", 1);
            Debug.Log($"{gameObject.name} ถึงโซนสีเขียวแล้ว → status = 10");
            
            
        }
    }

    public void MoveToPosition(Vector3 position)
    {
        StopAllCoroutines();
        StartCoroutine(MoveToTarget(position));
    }

    private System.Collections.IEnumerator MoveToTarget(Vector3 target)
    {
        Debug.Log($"Set animator movement to 2 for {gameObject.name}");
        animator.SetInteger("movement", 2); // เดิน
        

        while (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(target.x, 0, target.z)) > 0.1f)
        {
            Vector3 currentPos = transform.position;
            Vector3 targetPos = new Vector3(target.x, currentPos.y, target.z);

            Vector3 dir = (targetPos - currentPos).normalized;

            transform.position = Vector3.MoveTowards(currentPos, targetPos, walkSpeed * Time.deltaTime);

            if (dir != Vector3.zero)
            {
                Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
            }

            yield return null;
        }

        animator.SetInteger("movement", 1); // หยุดเดิน กลับเป็นยืน
    }


    void BecomeBlack()
    {
        if (animator != null)
            animator.enabled = false;

        if (bloodEffect != null)
            bloodEffect.SetActive(false);
        
        status = 10;
    }
}

