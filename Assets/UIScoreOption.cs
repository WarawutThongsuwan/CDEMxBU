using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreOption : MonoBehaviour
{
    public Button a1Button;
    public Button a2Button;
    public Button b1Button;
    public Button b2Button;
    public Button c1Button;
    public Button c2Button;

    private HashSet<string> clickedButtons = new HashSet<string>();

    private void Start()
    {
        // ผูกแต่ละปุ่มกับฟังก์ชันให้คะแนน
        a1Button.onClick.AddListener(() => OnOptionClicked("a1"));
        a2Button.onClick.AddListener(() => OnOptionClicked("a2"));
        b1Button.onClick.AddListener(() => OnOptionClicked("b1"));
        b2Button.onClick.AddListener(() => OnOptionClicked("b2"));
        c1Button.onClick.AddListener(() => OnOptionClicked("c1"));
        c2Button.onClick.AddListener(() => OnOptionClicked("c2"));
    }

    void OnOptionClicked(string id)
    {
        if (clickedButtons.Contains(id))
        {
            Debug.Log($"ปุ่ม {id} กดไปแล้ว → ไม่ให้คะแนนซ้ำ");
            return;
        }

        clickedButtons.Add(id);

        int point = 0;

        // เงื่อนไขให้คะแนนเฉพาะ a1, b1, c1
        if (id == "a1" || id == "b1" || id == "c1")
        {
            point = 5; // ใส่คะแนนที่ต้องการ
            ScoreManager.Instance.AddScoreAuto(10); // ไม่ต้องระบุชื่อด่านเอง

            Debug.Log($"ให้คะแนน {point} จากปุ่ม {id}");
        }
        else
        {
            Debug.Log($"ปุ่ม {id} ไม่มีคะแนน");
        }
    }
}
