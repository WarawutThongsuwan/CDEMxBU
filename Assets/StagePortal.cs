using UnityEngine;
using UnityEngine.SceneManagement;

public class StagePortal : MonoBehaviour
{
    private bool playerEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller") && !playerEntered)
        {
            playerEntered = true;
            string next = ScoreManager.Instance.nextStage;
            SceneManager.LoadScene(next);
        }
    }
}
