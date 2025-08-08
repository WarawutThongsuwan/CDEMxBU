using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerTrigger : MonoBehaviour
{
    public float timeLimit = 10f;
    static public float timer;
    private bool isTiming = false;
    private bool hasTriggered = false;

    public ManageZone manageZone; // ← ลากมาจาก Inspector

    [Header("UI")]
    public GameObject storyEndUI;         // UI ปุ่มด่านถัดไป (Story Mode)
    public GameObject freeEndUI;          // UI คะแนน + กลับเมนู (FreePlay Mode)
    public GameObject forceEndButtonObj;  // 🔴 ปุ่ม ForceEnd

    public TextMeshProUGUI timerText;         // ตัวแสดงเวลา
    public TextMeshProUGUI finalScoreText;    // ตัวแสดงคะแนน (เฉพาะ FreePlay)

    public GameObject objectToDisable;

    public GameObject targetObject;      // Object A ที่ให้ Player ชน
    public GameObject explosionObject;   // GameObject ระเบิดที่จะแสดง
    private bool explosionTriggered = false;
    private bool isPlayerInside = false;

     public bool isSend = true;
    


    void Update()
    {
        if (isTiming)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("F1");

            // เงื่อนไขเวลาเหลือ <= 210 และยังไม่เรียกระเบิด
            // if (timer <= 210f )
            // {


            //     if (explosionObject != null)
            //     {
            //         explosionObject.SetActive(true); // แสดงระเบิด
            //     }

            //     if (isPlayerInside)
            //     {
            //         isTiming = false;
            //         TimeUp(); // จบเกมทันที
            //     }
            // }
      
            if (timer <= 200 && isSend == true)
            {

                ScoreManager.Instance.AddScoreAuto(10);
                isSend = false;
                Debug.Log("SendScore 10");
            }


            if (timer <= 0)
            {
                isTiming = false;
                TimeUp();
            }
        }
    }


    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player") && !hasTriggered)
    //     {
    //         hasTriggered = true;
    //         timer = timeLimit;
    //         if (manageZone != null)
    //         {
    //             manageZone.ActivateZones();
    //         }
    //         isTiming = true;

    //         // 🔴 แสดงปุ่ม ForceEnd ตอนเริ่มนับเวลา
    //         if (forceEndButtonObj != null)
    //             forceEndButtonObj.SetActive(true);

    //         // 🔴 ปิด GameObject ที่ระบุไว้
    //         if (objectToDisable != null)
    //             objectToDisable.SetActive(false);
    //     }
    // }

    public void TimeUp()
    {
        // 🔴 ซ่อนปุ่ม ForceEnd เมื่อหมดเวลา
        if (forceEndButtonObj != null)
            forceEndButtonObj.SetActive(false);

        // เช็คโหมดปัจจุบัน
        if (ScoreManager.Instance.currentMode == ScoreManager.GameMode.Story)
        {
            string currentScene = SceneManager.GetActiveScene().name;

            // ✅ ถ้าอยู่ใน Stage3 ให้แสดงคะแนนรวม
            if (currentScene == "Stage3")
            {
                int total = ScoreManager.Instance.GetTotalScore();
                finalScoreText.text = "Total Score : " + total + " / 1280 ";
                finalScoreText.gameObject.SetActive(true); // ให้แน่ใจว่า Text ถูกเปิดแสดง
            }

            storyEndUI.SetActive(true); // เปิด UI เนื้อเรื่อง
        }
        else
        {
            string currentStage = SceneManager.GetActiveScene().name;
            int score = ScoreManager.Instance.GetStageScore(currentStage);

            finalScoreText.text = "Score: " + score.ToString();
            finalScoreText.gameObject.SetActive(true);
            freeEndUI.SetActive(true);  // เปิด UI FreePlay
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ถ้ายังไม่เคยเริ่มนับเวลา
            if (!hasTriggered)
            {
                hasTriggered = true;
                timer = timeLimit;
                if (manageZone != null)
                {
                    manageZone.ActivateZones();
                }
                isTiming = true;

                // แสดงปุ่ม ForceEnd
                if (forceEndButtonObj != null)
                    forceEndButtonObj.SetActive(true);

                // ปิด GameObject ที่ระบุไว้
                if (objectToDisable != null)
                    objectToDisable.SetActive(false);
            }

            // ตรวจสอบว่าชนกับ GameObject A หรือไม่
            if (targetObject != null && other.gameObject == targetObject)
            {
                isPlayerInside = true;
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject == targetObject)
        {
            isPlayerInside = false;
        }
    }


    // เรียกจากปุ่มในเกมตอนเล่น
    public void ForceEnd()
    {
        if (isTiming)
        {
            timer = 0f;
            isTiming = false;
            TimeUp();
        }
    }

    // เรียกจากปุ่มใน storyEndUI
    public void GoToNextStage()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string nextScene = "";

        switch (currentScene)
        {
            case "Stage1":
                nextScene = "Stage2";
                break;
            case "Stage2":
                nextScene = "ZoneSelect";
                break;
            case "ZoneSelect":
                nextScene = "Stage3";
                break;
            case "Stage3":
                nextScene = "EndScene"; // หรือกลับเมนู
                break;
            default:
                Debug.LogWarning("ไม่พบด่านถัดไปจาก: " + currentScene);
                break;
        }

        if (!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    // เรียกจากปุ่มใน freeEndUI
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
