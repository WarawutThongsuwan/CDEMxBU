using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunShooterSimple : MonoBehaviour
{
    public AudioSource gunShotSound;
    public ActionBasedController controller;
    public float triggerThreshold = 0.1f;

    private bool wasPressed = false;

    void Update()
    {
        if (controller && controller.activateActionValue != null)
        {
            float triggerValue = controller.activateActionValue.action.ReadValue<float>();

            if (triggerValue > triggerThreshold && !wasPressed && TKDetector.isDetect == true)
            {
                wasPressed = true;
                gunShotSound.Play();
            }

            if (triggerValue < triggerThreshold)
            {
                wasPressed = false;
            }
        }
    }
}
