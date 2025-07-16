using UnityEngine;

public class bodyCheck : MonoBehaviour
{
    public GameObject uiObject; // UI ที่จะเปิด/ปิด

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            Debug.Log("Trigger entered by controller");
            if (uiObject != null)
                uiObject.SetActive(true);
                
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            Debug.Log("Trigger exited by controller");
            if (uiObject != null)
                uiObject.SetActive(false);
        }
    }
}
