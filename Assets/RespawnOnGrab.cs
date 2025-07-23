using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RespawnOnGrab : MonoBehaviour
{
    public GameObject prefabToRespawn;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    private void Start()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        var grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Instantiate(prefabToRespawn, spawnPosition, spawnRotation);
    }
}
