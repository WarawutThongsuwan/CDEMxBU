using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stretcher1 : MonoBehaviour
{

    public Transform mountPoint;         // จุดวางคนไข้บนเปล
    public float moveSpeed = 2f;

    private Transform targetPatient;
    private Transform destination;

    private bool movingToPatient = false;
    private bool movingToDestination = false;

    // จุดปลายทางตาม tag
    public Transform redZone;
    public Transform yellowZone;
    public Transform greenZone;

    void Update()
    {
        if (movingToPatient && targetPatient != null)
        {
            MoveTowards(targetPatient.position);
        }
        else if (movingToDestination && destination != null)
        {
            MoveTowards(destination.position);
        }
    }

    void MoveTowards(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            if (movingToPatient)
            {
                AttachPatientToStretcher();
                SetDestinationByTag();
                movingToPatient = false;
                movingToDestination = true;
            }
            else if (movingToDestination)
            {
                movingToDestination = false;
                if (targetPatient != null)
                {
                    targetPatient.SetParent(null); // ปล่อยคนไข้
                }
                targetPatient = null; // จบภารกิจ
            }
        }
    }

    void AttachPatientToStretcher()
    {
        targetPatient.SetParent(mountPoint);
        targetPatient.localPosition = Vector3.zero;
    }

    void SetDestinationByTag()
    {
        if (targetPatient.CompareTag("Red"))
            destination = redZone;
        else if (targetPatient.CompareTag("Yellow"))
            destination = yellowZone;
        else if (targetPatient.CompareTag("Green"))
            destination = greenZone;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Red") || other.CompareTag("Yellow") || other.CompareTag("Green")) && targetPatient == null)
        {
            targetPatient = other.transform;
            movingToPatient = true;
        }
    }
}
