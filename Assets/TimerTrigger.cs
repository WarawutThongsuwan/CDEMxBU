using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerTrigger : MonoBehaviour
{
    public float timeLimit = 10f;
    private float timer;
    private bool isTiming = false;
    private bool hasTriggered = false;

    [Header("UI")]
    public GameObject storyEndUI;        // UI ปุ่มด่านถัดไป (Story Mode)
    public GameObject freeEndUI;         // UI คะแนน + กลับเมนู (FreePlay Mode)

    public TextMeshProUGUI timerText;    // ตัวแสดงเวลา
    public TextMeshProUGUI finalScoreText; // ตัวแสดงคะแนน (เฉพาะ FreePlay)

    void Update()
    {
        if (isTiming)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("F1");

            if (timer <= 0)
            {
                isTiming = false;
                TimeUp();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;     // ป้องกันเหยียบซ้ำ
            timer = timeLimit;
            isTiming = true;
        }
    }

    void TimeUp()
    {
        // เช็คโหมดปัจจุบัน
        if (ScoreManager.Instance.currentMode == ScoreManager.GameMode.Story)
        {
            storyEndUI.SetActive(true); // เปิด UI เนื้อเรื่อง
        }
        else
        {
            string currentStage = SceneManager.GetActiveScene().name;
            int score = ScoreManager.Instance.GetStageScore(currentStage);

            finalScoreText.text = "Score: " + score.ToString();
            freeEndUI.SetActive(true);  // เปิด UI FreePlay
        }
    }

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
                nextScene = "Stage3";
                break;
            case "Stage3":
                nextScene = "EndScene"; // หรือกลับเมนู ฯลฯ
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
