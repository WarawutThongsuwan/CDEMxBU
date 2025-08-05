using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TriageSocket : XRSocketInteractor
{
    [Header("Patient Reference")]
    public PatientStatus targetPatient;

    [Header("Allowed Tags")]
    public List<string> allowedTags = new List<string> { "TriageGreen", "TriageYellow", "TriageRed", "TriageBlack" };

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        var mono = interactable.transform.GetComponent<MonoBehaviour>();
        if (mono != null && allowedTags.Contains(mono.tag))
        {
            return base.CanSelect(interactable);
        }
        return false;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (targetPatient != null)
        {
            string tag = args.interactableObject.transform.tag;
            int colorCode = GetColorCodeFromTag(tag);

            targetPatient.triageColor = colorCode;
            Debug.Log($"{targetPatient.name} ถูกติด {tag} → triageColor = {colorCode}");
        }
        else
        {
            Debug.LogWarning("ยังไม่ได้เชื่อม targetPatient ใน Inspector");
        }
    }

    private int GetColorCodeFromTag(string tag)
    {
        switch (tag)
        {
            case "TriageGreen": return 1;
            case "TriageYellow": return 2;
            case "TriageRed": return 3;
            case "TriageBlack": return 4;
            default: return 0;
        }
    }
}
