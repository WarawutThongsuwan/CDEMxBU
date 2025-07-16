using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public enum GameMode { Story, FreePlay }
    public GameMode currentMode = GameMode.Story;

    public int totalScore = 0;
    public Dictionary<string, int> stageScores = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScoreAuto(int amount)
    {
        string stage = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        AddScore(stage, amount);
    }


    // เพิ่มคะแนน พร้อมระบุชื่อด่าน
    public void AddScore(string stageName, int amount)
    {
        if (!stageScores.ContainsKey(stageName))
        {
            stageScores[stageName] = 0;
        }

        stageScores[stageName] += amount;

        if (currentMode == GameMode.Story)
        {
            totalScore += amount;
        }

        Debug.Log($"🎯 ได้คะแนน {amount} ที่ด่าน {stageName} (รวม: {totalScore})");
    }

    public int GetStageScore(string stageName)
    {
        return stageScores.ContainsKey(stageName) ? stageScores[stageName] : 0;
    }

    public void ResetAllScores()
    {
        totalScore = 0;
        stageScores.Clear();
    }

}
