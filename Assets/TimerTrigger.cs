using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerTrigger : MonoBehaviour
{
    public float timeLimit = 10f;
    private float timer;
    private bool isTiming = false;

    public GameObject storyEndUI;        // UI ด่านถัดไป (Story Mode)
    public GameObject freeEndUI;         // UI คะแนน + กลับเมนู (FreePlay Mode)

    public TextMeshProUGUI timerText;    // ตัวแสดงเวลา
    public TextMeshProUGUI scoreText;    // ตัวแสดงคะแนน (ใน FreePlay เท่านั้น)

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
        if (other.CompareTag("Player"))
        {
            timer = timeLimit;
            isTiming = true;
        }
    }

    void TimeUp()
    {
        if (ScoreManager.Instance.currentMode == ScoreManager.GameMode.Story)
        {
            storyEndUI.SetActive(true); // เปิด UI เนื้อเรื่อง
        }
        else
        {
            // แสดงคะแนนของด่านนี้
            int score = ScoreManager.Instance.GetStageScore("Stage2"); // หรือชื่อด่านอื่น

            scoreText.text = "คะแนน: " + score.ToString();

            freeEndUI.SetActive(true);  // เปิด UI FreePlay
        }
    }

    // เรียกโดยปุ่มใน storyEndUI
    public void GoToNextStage()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string nextScene = "";

        // กำหนดลำดับด่านต่อไปตามชื่อ
        switch (currentScene)
        {
            case "Stage1":
                nextScene = "Stage2";
                break;
            case "Stage2":
                nextScene = "Stage3";
                break;
            default:
                Debug.LogWarning("ไม่รู้จักด่านถัดไปของ " + currentScene);
                break;
        }

        if (!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    // เรียกโดยปุ่มใน freeEndUI
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
