using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WoundSocket : XRSocketInteractor
{
    public BleedingController bleedingTarget;
    public string allowedTag = "Tourniquet";

    // ใช้ API ใหม่: IXRSelectInteractable
    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        var mono = interactable.transform.GetComponent<MonoBehaviour>();
        if (mono != null && mono.CompareTag(allowedTag))
        {
            return base.CanSelect(interactable);
        }

        return false;
    }

    // ใช้ SelectEnterEventArgs ตาม API ใหม่
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        Debug.Log("เปะ Tag");

        if (bleedingTarget != null)
        {
            bleedingTarget.StopBleeding();
            Debug.Log("เลือดหยุดเมื่อสายรัดเสียบเข้ารูแผล");
        }
    }
}