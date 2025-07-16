using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class BagManagerVR : MonoBehaviour
{
    [Header("Socket ช่องเก็บของ")]
    public XRSocketInteractor[] sockets = new XRSocketInteractor[4];

    [Header("UI และปุ่มยืนยัน")]
    public GameObject submitUI;
    public Button submitButton;

    [Header("ฝาปิดกระเป๋า")]
    public GameObject bagLidObject;

    private bool hasSubmitted = false;
    private List<string> countedItemIDs = new List<string>();

    private void Start()
    {
        submitUI.SetActive(false);
        bagLidObject.SetActive(false);

        if (submitButton != null)
            submitButton.onClick.AddListener(OnSubmitClicked);
    }

    private void Update()
    {
        if (!submitUI.activeSelf && AllSocketsFilled())
        {
            submitUI.SetActive(true);
        }
    }

    bool AllSocketsFilled()
    {
        foreach (var socket in sockets)
        {
            if (!socket.hasSelection)
                return false;
        }
        return true;
    }

    void OnSubmitClicked()
    {
        if (hasSubmitted) return;
        hasSubmitted = true;

        int score = 0;

        foreach (var socket in sockets)
        {
            if (socket.selectTarget != null)
            {
                BagItemData item = socket.selectTarget.GetComponent<BagItemData>();
                if (item != null && item.isScorable)
                {
                    if (!countedItemIDs.Contains(item.itemID))
                    {
                        countedItemIDs.Add(item.itemID);
                        score += item.scoreValue;
                    }
                    else
                    {
                        Debug.Log($"ของซ้ำ: {item.itemID} → ไม่นับคะแนน");
                    }
                }
            }
        }

        ScoreManager.Instance.AddScoreAuto(10); // ไม่ต้องระบุชื่อด่านเอง

        Debug.Log("คะแนนรอบนี้: " + score);

        submitUI.SetActive(false);
        if (bagLidObject != null)
            bagLidObject.SetActive(true);
    }
}
