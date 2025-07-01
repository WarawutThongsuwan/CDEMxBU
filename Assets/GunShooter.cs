using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunShooter : MonoBehaviour
{
    public AudioSource gunShotSound;
    public InputActionProperty triggerAction; // เชื่อมกับ Input System

    void Update()
    {
        if (triggerAction.action.WasPressedThisFrame())
        {
            gunShotSound.Play();
        }
    }
}