using UnityEngine;

public class BagActivator : MonoBehaviour
{
    public GameObject itemGroup;

    public void ShowItems()
    {
        if (itemGroup != null)
        {
            itemGroup.SetActive(true);

            // วางไว้หน้าผู้เล่น
            Transform playerHead = Camera.main.transform;
            itemGroup.transform.position = playerHead.position + playerHead.forward * 0.5f;
            itemGroup.transform.LookAt(playerHead);
        }
    }

    public void HideItems()
    {
        if (itemGroup != null)
        {
            itemGroup.SetActive(false);
        }
    }
}
