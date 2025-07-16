using UnityEngine;

public class BagItemData : MonoBehaviour
{
    public string itemID = "item1";      // ตั้งชื่อเฉพาะ
    public int scoreValue = 10;          // คะแนนที่ให้
    public bool isScorable = true;       // true = ให้คะแนน
    [HideInInspector] public Vector3 originalScale; // สำหรับจำขนาดเดิม
}
