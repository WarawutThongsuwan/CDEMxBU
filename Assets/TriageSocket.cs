using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TriageSocket : XRSocketInteractor
{
    [Header("Patient Reference")]
    public PatientStatus targetPatient;        // คนไข้ที่จะเปลี่ยนค่า

    [Header("Allowed Tag")]
    public string allowedTag = "TriageTag";    // แท็กที่อนุญาตให้เสียบ

    // เช็คว่าของที่จะเสียบตรง Tag ที่กำหนดไหม
    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        var mono = interactable.transform.GetComponent<MonoBehaviour>();
        if (mono != null && mono.CompareTag(allowedTag))
        {
            return base.CanSelect(interactable);
        }
        return false;
    }

    // เมื่อเสียบวัตถุเข้าซ็อกเก็ต
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (targetPatient != null)
        {
            targetPatient.triageColor = 1;  // เปลี่ยนสีเป็นเขียว
            Debug.Log($"{targetPatient.name} ถูกติด TriageTag → triageColor = 1");
        }
        else
        {
            Debug.LogWarning("ยังไม่ได้เชื่อม targetPatient ใน Inspector");
        }
    }
}
