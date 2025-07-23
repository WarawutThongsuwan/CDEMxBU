using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackOpener : MonoBehaviour
{
    public GameObject inventoryUI; // ใส่ Canvas ที่แสดง UI
    private bool isUIOpen = false;
 
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller")) // ตั้ง tag ที่มือหรือ controller
        {
            inventoryUI.SetActive(true);
            isUIOpen = true;
        }
    }
 
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Controller") && isUIOpen)
        {
            inventoryUI.SetActive(false);
            isUIOpen = false;
        }
    }
}