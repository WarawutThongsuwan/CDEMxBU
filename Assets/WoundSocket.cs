using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WoundSocket : XRSocketInteractor
{
    public BleedingController bleedingTarget;
    public string allowedTag = "Tourniquet";
    public GameObject objectToActivate; // ← ออปเจคที่จะเปิดเมื่อเลือดหยุด

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        var mono = interactable.transform.GetComponent<MonoBehaviour>();
        if (mono != null && mono.CompareTag(allowedTag))
        {
            return base.CanSelect(interactable);
        }

        return false;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        Debug.Log("เปะ Tag");

        if (bleedingTarget != null)
        {
            ScoreManager.Instance.AddScoreAuto(10);
            bleedingTarget.StopBleeding();
            Debug.Log("เลือดหยุดเมื่อสายรัดเสียบเข้ารูแผล");

            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
        }
    }
}
