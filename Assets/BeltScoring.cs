using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BeltScoring : MonoBehaviour
{
    [Header("Socket ของเข็มขัด")]
    public XRSocketInteractor leftSocket;
    public XRSocketInteractor rightSocket;

    [Header("ID ของวัตถุที่ให้คะแนน")]
    public string itemA_ID = "itemA"; // วัตถุด้านขวา
    public string itemB_ID = "itemB"; // วัตถุด้านซ้าย

    [Header("คะแนนที่ให้")]
    public int itemAScore = 5;
    public int itemBScore = 5;

    private bool itemAScored = false;
    private bool itemBScored = false;

    private void Update()
    {
        CheckSocketForItem(rightSocket, itemA_ID, ref itemAScored, itemAScore);
        CheckSocketForItem(leftSocket, itemB_ID, ref itemBScored, itemBScore);
    }

    void CheckSocketForItem(XRSocketInteractor socket, string expectedID, ref bool alreadyScored, int scoreValue)
    {
        if (alreadyScored) return;

        if (socket.hasSelection)
        {
            var obj = socket.selectTarget;
            var data = obj.GetComponent<BagItemData>();

            if (data != null && data.itemID == expectedID)
            {
                alreadyScored = true;
                ScoreManager.Instance.AddScoreAuto(10); // ไม่ต้องระบุชื่อด่านเอง

                Debug.Log($"🎯 ได้คะแนน {scoreValue} จาก {expectedID} ที่ช่อง {socket.name}");
            }
        }
    }
}
