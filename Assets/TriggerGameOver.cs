using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameOver : MonoBehaviour
{
    private TimerTrigger timerTrigger;

    public void SetTimerTrigger(TimerTrigger trigger)
    {
        timerTrigger = trigger;
    }

    void Start()
    {
        Debug.Log("สร้างแล้ว");
        Destroy(gameObject, 5);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ชนแล้ว");
            if (timerTrigger != null)
            {
                timerTrigger.TimeUp();
            }
            else
            {
                Debug.LogWarning("ยังไม่มีการกำหนด TimerTrigger ให้ TriggerGameOver");
            }
        }
    }
}