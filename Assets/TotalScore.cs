using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TotalScore : MonoBehaviour
{
   
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshPro>().text = "Total Score : " + GameObject.Find("Gamemanager").GetComponent<ScoreManager>().totalScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
