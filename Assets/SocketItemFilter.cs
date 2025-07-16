using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketItemFilter : MonoBehaviour
{
    public string allowedItemID = "itemA"; // ใส่ itemA หรือ itemB ตามต้องการ

    private XRSocketInteractor socket;

    private void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    public bool CanSelect(XRBaseInteractable interactable)
    {
        var data = interactable.GetComponent<BagItemData>();
        if (data == null) return false;

        return data.itemID == allowedItemID;
    }

    private void OnEnable()
    {
        if (socket != null)
        {
            socket.selectEntered.AddListener(HandleSelectEntered);
            socket.selectExited.AddListener(HandleSelectExited);
        }
    }

    private void OnDisable()
    {
        if (socket != null)
        {
            socket.selectEntered.RemoveListener(HandleSelectEntered);
            socket.selectExited.RemoveListener(HandleSelectExited);
        }
    }

    private void HandleSelectEntered(SelectEnterEventArgs args)
    {
        if (!CanSelect(args.interactableObject.transform.GetComponent<XRBaseInteractable>()))
        {
            socket.interactionManager.CancelInteractableSelection(args.interactableObject);
        }
    }

    private void HandleSelectExited(SelectExitEventArgs args)
    {
        // ไม่ต้องทำอะไร แต่ฟังก์ชันต้องอยู่เพื่อ unregister ได้
    }
}
