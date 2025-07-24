using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RespawnOnGrab : MonoBehaviour
{
    public GameObject prefabToRespawn;
    public float respawnCooldown = 1f;
    private float lastRespawnTime = 0f;

    private Vector3 originalLocalPosition;
    private Quaternion originalLocalRotation;
    private Transform parentGroup;
    private bool hasBeenGrabbed = false;
    private XRGrabInteractable grab;

    private void Start()
    {
        parentGroup = transform.parent;

        originalLocalPosition = transform.localPosition;
        originalLocalRotation = transform.localRotation;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrabbed);
        grab.selectExited.AddListener(OnReleased);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (hasBeenGrabbed || Time.time - lastRespawnTime < respawnCooldown)
            return;

        hasBeenGrabbed = true;
        lastRespawnTime = Time.time;

        // 🔁 เสกของใหม่ในตำแหน่งเดิม
        GameObject newObj = Instantiate(prefabToRespawn);
        newObj.transform.SetParent(parentGroup);
        newObj.transform.localPosition = originalLocalPosition;
        newObj.transform.localRotation = originalLocalRotation;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        // ✅ เมื่อปล่อยไอเทม:
        // 1. ย้ายออกจากกลุ่ม
        transform.SetParent(null);

        // 2. เปิดการใช้แรงโน้มถ่วง
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }

        // 3. ❌ ถอด Listener และปิด Script นี้ (หยุดการเสกของ)
        if (grab != null)
        {
            grab.selectEntered.RemoveListener(OnGrabbed);
            grab.selectExited.RemoveListener(OnReleased);
        }

        Destroy(this); // ทำลาย Script ทิ้ง
    }
}
