using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red1Status : MonoBehaviour
{

    
    static public int p_red1_status;
    public int greenYellowRedBlack; // green = 1  Yellow= 2  Red 3  Black4
    void Start()
    {
        p_red1_status = 1;
        greenYellowRedBlack = 3;
       
    }

    void Update()
    {

        switch (p_red1_status)
        {
            case 1:
                TimeCount();
                break;

            case 2:
              
                break;

            case 3:
                
                break;
            case 4:
                
                break;

            case 5:
                
                break;
            case 6:
                //TimeCount();
                 
                 GameObject.Find("Stretcher (3)").GetComponent<Stretcher1>().SetTarget(transform.position, this.transform);
                 
                break;

            case 7:
                BecomBlack();
                break;


        }

        if (timeCountdown > 5)
        {
            // p_red1_status = 7;// 
          
        }

    }//Update

    //จับเวลา ....Min  --> p_red1_status = 0
    public float timeCountdown;
    public void TimeCount()
    {
        timeCountdown += Time.deltaTime;
    }


    //walk   --> p_red1_status = 1
    // Blood    --> p_red1_status = 2
    // Taking  --> p_red1_status = 3
    // Injury   --> p_red1_status = 4
    // Breath  --> p_red1_status = 5

    //----------------- Tag  -------------p_red1_status = 6
    
    /// 
    /// check Tag
    

private void OnTriggerEnter(Collider other)
{
        if (other.CompareTag("TriageTag") )
        {

            PlayerScore.score += 1;
            p_red1_status = 6;
            Debug.Log("สถานะ 6 แล้ว มีเปลมาชน");
        

           
    }
}





 //----------------- Red to Black -------------p_red1_status = 7
    public GameObject red1;
    public GameObject red1_blood;
   
    void BecomBlack()
    {
        print("BecomeBlack");

        greenYellowRedBlack = 4;

        //StopAnimetion
        red1.GetComponent<Animator>().enabled = false;
        //StopBlood
        red1_blood.SetActive(false);
        //CantCheckAnything
    }




    // Start is called before the first frame update
   

    // Update is called once per frame
   
}
