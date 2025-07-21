using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StagePortalUI : MonoBehaviour
{
    public Button goToStage1Button;

    private void Start()
    {
        if (goToStage1Button != null)
        {
            goToStage1Button.onClick.AddListener(LoadStage1);
        }
        else
        {
            Debug.LogWarning("ยังไม่ได้ตั้งปุ่ม goToStage1Button ใน Inspector");
        }
    }

    private void LoadStage1()
    {
        SceneManager.LoadScene("Stage1"); // หรือใช้ชื่อ Scene ที่ต้องการ
    }
}
