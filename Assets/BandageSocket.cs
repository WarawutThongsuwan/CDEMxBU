using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class BandageSocket : XRSocketInteractor
{
    [Header("Target to Change Material")]
    public Renderer targetRenderer;        // ออปเจคที่จะเปลี่ยน Material
    public Material newMaterial;           // วัสดุใหม่ที่จะเปลี่ยน

    public GameObject objectToActivate;

    [Header("Allowed Tag")]
    public string allowedTag = "BandageTag"; // แท็กที่อนุญาตให้เสียบ

    [Header("Debug")]
    public bool testApplyMaterial = false; // ติ๊กแล้วจะเปลี่ยนวัสดุทันที

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
        ApplyMaterial();
        Destroy(args.interactableObject.transform.gameObject);
    }

    public void ApplyMaterial()
    {
        if (targetRenderer != null && newMaterial != null)
        {
            ScoreManager.Instance.AddScoreAuto(10);
            targetRenderer.material = newMaterial;
            Debug.Log("เปลี่ยน Material เรียบร้อยแล้ว");
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }

        }
        else
        {
            Debug.LogWarning("ยังไม่ได้กำหนด targetRenderer หรือ newMaterial");
        }
    }

    private void Update()
    {
        // ถ้าติ๊กใน Inspector → ทำงานแล้วรีเซ็ตกลับ
        if (testApplyMaterial)
        {
            ApplyMaterial();
            testApplyMaterial = false;
        }
    }
}