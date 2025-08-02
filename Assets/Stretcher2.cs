using UnityEngine;

public class Stretcher2 : MonoBehaviour
{
    public float moveSpeed = 2f;

    private Vector3 targetPosition;
    private bool moving = false;

    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                moving = false;
                Debug.Log("ถึงเป้าหมายแล้ว");
            }
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        moving = true;
    }
}
