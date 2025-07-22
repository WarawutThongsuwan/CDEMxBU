using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreButton10 : MonoBehaviour
{
    [System.Serializable]
    public class ScoreButton
    {
        public string id;
        public Button button;
        public GameObject targetUI;
    }

    public List<ScoreButton> scoreButtons = new List<ScoreButton>();

    private HashSet<string> clickedButtons = new HashSet<string>();

    private void Start()
    {
        foreach (var sb in scoreButtons)
        {
            if (sb.button != null && sb.targetUI != null)
            {
                string currentId = sb.id;
                sb.button.onClick.AddListener(() => OnButtonClicked(currentId));
            }
            else
            {
                Debug.LogWarning($"ยังไม่ได้กำหนดปุ่มหรือ UI ให้กับ ID: {sb.id}");
            }
        }
    }

    void OnButtonClicked(string id)
    {
        if (clickedButtons.Contains(id))
        {
            Debug.Log($"ปุ่ม {id} ถูกกดไปแล้ว → ไม่ให้คะแนนซ้ำ");
            return;
        }

        clickedButtons.Add(id);

        // ให้คะแนน 10
        ScoreManager.Instance.AddScoreAuto(10);
        Debug.Log($"ให้คะแนน 10 จากปุ่ม {id}");

        // แสดง UI ที่ตรงกับปุ่มนั้น
        var target = scoreButtons.Find(b => b.id == id);
        if (target != null && target.targetUI != null)
        {
            target.targetUI.SetActive(true);
        }
    }
}
