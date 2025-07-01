using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BagUIController : MonoBehaviour
{
    public ActionBasedController controller;       // ตัวควบคุม VR เช่น XR Controller
    public GameObject bagUI;                       // GameObject ของ UI กระเป๋า
    public float triggerThreshold = 0.1f;          // ค่าที่ใช้ตรวจว่า trigger ถูกกดพอหรือยัง

    private bool wasPressed = false;
    private bool isUIVisible = false;

    void Update()
    {
        if (controller && controller.activateActionValue != null)
        {
            float triggerValue = controller.activateActionValue.action.ReadValue<float>();

            // เมื่อกด Trigger
            if (triggerValue > triggerThreshold && !wasPressed)
            {
                wasPressed = true;
                ToggleBagUI();  // สลับการแสดงผล UI
            }

            // ปล่อย Trigger
            if (triggerValue < triggerThreshold)
            {
                wasPressed = false;
            }
        }
    }

    void ToggleBagUI()
    {
        isUIVisible = !isUIVisible;
        bagUI.SetActive(isUIVisible);
    }
}
