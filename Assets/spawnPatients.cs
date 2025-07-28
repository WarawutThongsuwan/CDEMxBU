using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPatients : MonoBehaviour
{
    public GameObject patientsPrefab;
    public bool isSpwan;


    void Update()
    {
        if (isSpwan == false)
        {
            StartCoroutine(LetSpawn());
        }
    }

    IEnumerator LetSpawn()
    {
           isSpwan = true;
        yield return new WaitForSeconds(1f); // หน่วงเวลา 1 วินาที
        Instantiate(patientsPrefab, transform.position, transform.rotation);
        
     
    }
    
    
}
