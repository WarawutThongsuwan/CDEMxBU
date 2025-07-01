using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour
{
    public GameObject capsule1;
    void Update()
    {
    
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Animator>().SetInteger("Stage", 0);
            transform.Rotate(0,1,0);
        }
        if (Input.GetKey(KeyCode.B))
        {
            GetComponent<Animator>().SetInteger("Stage", 1);
            
        }
        if (Input.GetKey(KeyCode.C))
        {
            GetComponent<Animator>().SetInteger("Stage", 2);
            transform.Translate(0,0,0.01f);
        }

    }
    

}
