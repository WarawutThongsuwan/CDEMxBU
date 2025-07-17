using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject subMenuPanel;

    private void Start()
    {
        // เริ่มต้นโชว์แค่เมนูหลัก
        mainMenuPanel.SetActive(true);
        subMenuPanel.SetActive(false);
    }

    public void OnPlayPressed()
    {
        mainMenuPanel.SetActive(false);
        subMenuPanel.SetActive(true);
    }

    public void OnBackPressed()
    {
        mainMenuPanel.SetActive(true);
        subMenuPanel.SetActive(false);
    }

    public void OnExitPressed()
    {
        Application.Quit();
        Debug.Log("ออกจากเกม");
    }

    public void OnFullProcessPressed()
    {
        ScoreManager.Instance.ResetAllScores();
        ScoreManager.Instance.currentMode = ScoreManager.GameMode.Story;
        ScoreManager.Instance.nextStage = "Stage1";  // กำหนดด่านที่จะเริ่มหลังเดินเข้าบล็อกใน CityMap
        SceneManager.LoadScene("CityMap");           // ไปซีนเมืองก่อน
    }

    public void OnStagePressed(string stageName)
    {
        ScoreManager.Instance.ResetAllScores();
        
        if (stageName == "Stage1")
        {
            // ถ้าเป็น Stage1 ให้ไป CityMap ก่อน
            ScoreManager.Instance.currentMode = ScoreManager.GameMode.FreePlay;
            ScoreManager.Instance.nextStage = "Stage1";
            SceneManager.LoadScene("CityMap");
        }
        else
        {
            // ถ้าเป็น Stage2, Stage3... ให้ไปซีนตรง ๆ
            ScoreManager.Instance.currentMode = ScoreManager.GameMode.FreePlay;
            SceneManager.LoadScene(stageName);
        }
    }

}
