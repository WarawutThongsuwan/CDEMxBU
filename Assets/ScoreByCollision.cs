using UnityEngine;

public class ScoreByCollision : MonoBehaviour
{
    public GameObject targetUI;          // UI ที่จะแสดงเมื่อชน
    public int point = 10;               // คะแนนที่จะให้เมื่อชน
    private bool hasScored = false;      // ป้องกันให้คะแนนซ้ำ

    private void OnTriggerEnter(Collider other)
    {
        if (hasScored)
        {
            Debug.Log($"{gameObject.name} ให้คะแนนไปแล้ว");
            return;
        }

        if (other.CompareTag("Controller"))
        {
            // ให้คะแนน
            ScoreManager.Instance.AddScoreAuto(point);
            hasScored = true;

            // แสดง UI
            if (targetUI != null)
            {
                targetUI.SetActive(true);
            }

            Debug.Log($"ชนกับ {other.name} → ให้คะแนน {point} จาก {gameObject.name}");
        }
    }
}
