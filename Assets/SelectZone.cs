using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectZone : MonoBehaviour
{
    static public int selectZoneStage;

    public GameObject triageUiZone, treatmentUiZone, parkingUiZone, loadingUiZone;
    // Start is called before the first frame update
    void Start()
    {
        selectZoneStage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectZoneStage == 1)
        {
            triageUiZone.SetActive(true);
            treatmentUiZone.SetActive(false);
            parkingUiZone.SetActive(false);
            loadingUiZone.SetActive(false);
        }
        if (selectZoneStage == 2)
        {
            triageUiZone.SetActive(false);
            treatmentUiZone.SetActive(true);
            parkingUiZone.SetActive(false);
            loadingUiZone.SetActive(false);
        }
        if (selectZoneStage == 3)
        {
            triageUiZone.SetActive(false);
            treatmentUiZone.SetActive(false);
            parkingUiZone.SetActive(true);
            loadingUiZone.SetActive(false);
        }
        if (selectZoneStage == 4)
        {
            triageUiZone.SetActive(false);
            treatmentUiZone.SetActive(false);
            parkingUiZone.SetActive(false);
            loadingUiZone.SetActive(true);
        }

        if (selectZoneStage == 5)
        {
            SceneManager.LoadScene("Stage3");

        }


    }

    // ---------------------------PlayerPrefs-------------------------------------

    public int triageZone;
    public int treatmentZone;
    public int parkingZone;
    public int loadingZone;

    
    public int button;
    public void button1()
    {


        button = 1;
        SelectZoneStageNo();
        selectZoneStage++;
    }

    public void button2()
    {

        button = 2;
        SelectZoneStageNo();
                          selectZoneStage++;
    }

    public void button3()
    {

        button = 3;
        SelectZoneStageNo();
                           selectZoneStage++;
    }

    public void button4()
    {

        button = 4;
        SelectZoneStageNo();
                           selectZoneStage++;
    }

    public void button5()
    {

        button = 5;
        SelectZoneStageNo();
                           selectZoneStage++;
    }

    public void button6()
    {

        button = 6;
        SelectZoneStageNo();
                           selectZoneStage++;
    }

    public void SelectZoneStageNo ()
    { 

     if (SelectZone.selectZoneStage == 1)
        {
            TrtriageZone();
        }

        if (SelectZone.selectZoneStage == 2)
        {
            TreatmentZone();
        }
        if (SelectZone.selectZoneStage == 3)
        {
            ParkingZone();
        }
        if (SelectZone.selectZoneStage == 4)
        {
            LoadingZone();
        }
    }

    public void TrtriageZone()
    {
        PlayerPrefs.SetInt("TrtriageZone", button);
        print("Pref TrtriageZone " + PlayerPrefs.GetInt("TrtriageZone") );
    }
    public void TreatmentZone()
    {
        PlayerPrefs.SetInt("TreatmentZone", button);
                print("Pref TreatmentZone " + PlayerPrefs.GetInt("TreatmentZone") );
    }
    public void ParkingZone()
    {
        PlayerPrefs.SetInt("ParkingZone", button);
           print("Pref ParkingZone " + PlayerPrefs.GetInt("ParkingZone") );
    }
    public void LoadingZone()
    {
        PlayerPrefs.SetInt("LoadingZone", button);
            print("Pref LoadingZone " + PlayerPrefs.GetInt("LoadingZone") );
    }



// ---------------------------PlayerPrefs-------------------------------------
}
