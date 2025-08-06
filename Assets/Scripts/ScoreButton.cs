using UnityEngine;
using UnityEngine.UI;

public class ScoreButton : MonoBehaviour
{
    public int scoreToAdd = 10;              // คะแนนที่ต้องการส่งเมื่อกด
    public string stageNameOverride = "";    // ถ้ามีการระบุชื่อด่านแบบ custom
    private Button button;
    private bool hasClicked = false;         // ป้องกันการกดซ้ำ

    void Start()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        if (hasClicked) return;

        string stage = string.IsNullOrEmpty(stageNameOverride)
            ? UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            : stageNameOverride;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(stage, scoreToAdd);
        }

        hasClicked = true;
        button.interactable = false; // ปิดปุ่มไม่ให้กดซ้ำ

        Debug.Log($"✅ ปุ่มคะแนน {scoreToAdd} ถูกกดในด่าน {stage}");
    }
}
