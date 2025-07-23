using UnityEngine;

public class BagActivator : MonoBehaviour
{
    public GameObject itemGroup; // ตั้งค่าใน Inspector ให้เป็น GameObject ที่รวม 7 แบบ

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            if (itemGroup != null)
            {
                itemGroup.SetActive(true);
                // ให้อยู่หน้าผู้เล่น:
                Transform playerHead = Camera.main.transform;
                itemGroup.transform.position = playerHead.position + playerHead.forward * 0.5f;
                itemGroup.transform.LookAt(playerHead); // หันเข้าหาผู้เล่น
            }
        }
    }
}
