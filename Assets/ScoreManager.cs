using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public string nextStage = "Stage1"; // ค่า default เผื่อกรณี fallback

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

    public void SetMode(GameMode mode)
    {
        currentMode = mode;
    }

    public int GetScore()
    {
        string currentStage = SceneManager.GetActiveScene().name;
        return GetStageScore(currentStage);
    }

    public void AddScoreAuto(int amount)
    {
        string stage = SceneManager.GetActiveScene().name;
        AddScore(stage, amount);
    }

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

        PatientStatus.isScoreCounting = true;

        Debug.Log($"🎯 ได้คะแนน {amount} ที่ด่าน {stageName} (รวม: {totalScore})");
    }

    public int GetStageScore(string stageName)
    {
        return stageScores.ContainsKey(stageName) ? stageScores[stageName] : 0;
    }

    public int GetTotalScore()
    {
        return totalScore;
    }

    public void SetStageScore(string stageName, int score)
    {
        stageScores[stageName] = score;
    }

    public void ResetAllScores()
    {
        totalScore = 0;
        stageScores.Clear();
    }
}
