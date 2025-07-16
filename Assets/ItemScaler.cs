using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ItemScaler : MonoBehaviour
{
    private BagItemData data;
    private XRGrabInteractable grab;

    public Vector3 scaleWhenInBag = new Vector3(0.3f, 0.3f, 0.3f);

    private void Awake()
    {
        data = GetComponent<BagItemData>();
        grab = GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        if (data != null)
        {
            data.originalScale = transform.localScale;
        }

        grab.selectEntered.AddListener(OnSelect);
        grab.selectExited.AddListener(OnDeselect);
    }

    void OnSelect(SelectEnterEventArgs args)
    {
        if (args.interactor is XRSocketInteractor)
        {
            transform.localScale = scaleWhenInBag;
        }
    }

    void OnDeselect(SelectExitEventArgs args)
    {
        transform.localScale = data.originalScale;
    }
}
