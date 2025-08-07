using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForExplode : MonoBehaviour
{
    public GameObject explosion, gameOverTrigger;
    public AudioClip warningVoice;

    public TimerTrigger timerTrigger; // ← ลากอ้างอิงใน Inspector (ตัวหลักในฉาก)

    void Start()
    {
        Invoke("SpawnVoice", 60f);
        Invoke("SpawnExplode", 90f);
        Invoke("SpawnGameOverTrigger", 90f);
    }

    void SpawnVoice()
    {
        GetComponent<AudioSource>().PlayOneShot(warningVoice);
    }

    void SpawnExplode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }

    void SpawnGameOverTrigger()
    {
        GameObject go = Instantiate(gameOverTrigger, transform.position, transform.rotation);

        // หาและกำหนด TimerTrigger ให้ Prefab ที่เพิ่งเกิด
        TriggerGameOver triggerScript = go.GetComponent<TriggerGameOver>();
        if (triggerScript != null && timerTrigger != null)
        {
            triggerScript.SetTimerTrigger(timerTrigger);
        }
    }
}
