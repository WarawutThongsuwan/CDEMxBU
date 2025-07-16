using UnityEngine;

public class BleedingController : MonoBehaviour
{
    public GameObject bloodEffectPrefab;
    private GameObject currentBloodEffect;

    private bool isBleeding = false;

    void Start()
    {
        StartBleeding(); // 🩸 เริ่มไหลเลือดทันทีเมื่อเกมเริ่ม
    }


    public void StartBleeding()
    {
        if (!isBleeding)
        {
            isBleeding = true;
            currentBloodEffect = Instantiate(bloodEffectPrefab, transform.position, Quaternion.identity, transform);
        }
    }

    public void StopBleeding()
    {
        if (isBleeding)
        {
            isBleeding = false;
            if (currentBloodEffect != null)
                Destroy(currentBloodEffect);
        }
    }
}
