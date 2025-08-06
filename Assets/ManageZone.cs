using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManageZone : MonoBehaviour
{

    public GameObject triageZone1, triageZone2, triageZone3, triageZone4, triageZone5, triageZone6;
    public GameObject treatmentZone1, treatmentZone2, treatmentZone3, treatmentZone4, treatmentZone5, treatmentZone6;
    public GameObject parking1, parking2, parking3, parking4, parking5, parking6;
    public GameObject loading1, loading2, loading3, loading4, loading5, loading6;

    // Start is called before the first frame update
    public void ActivateZones()
    {
        switch (PlayerPrefs.GetInt("TrtriageZone"))
        {
            case 1:
                triageZone1.SetActive(true);
                break;
            case 2:
                triageZone2.SetActive(true);
                break;
            case 3:
                triageZone3.SetActive(true);
                break;
            case 4:
                triageZone4.SetActive(true);
                break;
            case 5:
                triageZone5.SetActive(true);
                break;
            case 6:
                triageZone6.SetActive(true);
                break;
        }//TrtriageZone

        switch (PlayerPrefs.GetInt("TreatmentZone"))
        {
            case 1:
                treatmentZone1.SetActive(true);
                break;
            case 2:
                treatmentZone2.SetActive(true);
                break;
            case 3:
                treatmentZone3.SetActive(true);
                break;
            case 4:
                treatmentZone4.SetActive(true);
                break;
            case 5:
                treatmentZone5.SetActive(true);
                break;
            case 6:
                treatmentZone6.SetActive(true);
                break;
        }//TreatmentZone

        switch (PlayerPrefs.GetInt("ParkingZone"))
        {
            case 1:
                parking1.SetActive(true);
                break;
            case 2:
                parking2.SetActive(true);
                break;
            case 3:
                parking3.SetActive(true);
                break;
            case 4:
                parking4.SetActive(true);
                break;
            case 5:
                parking5.SetActive(true);
                break;
            case 6:
                parking6.SetActive(true);
                break;
        }//ParkingZone
        
        switch (PlayerPrefs.GetInt("LoadingZone"))
        {
            case 1:
                loading1.SetActive(true);
                break;
            case 2:
                loading2.SetActive(true);
                break;
            case 3:
                loading3.SetActive(true);
                break;
            case 4:
                loading4.SetActive(true);
                break;
            case 5:
                loading5.SetActive(true);
                break;
            case 6:
                loading6.SetActive(true);
                break;
        }//LoadingZone
     
    }//Start
 




    

    // Update is called once per frame
    void Update()
    {

    }


}//ManageZone
