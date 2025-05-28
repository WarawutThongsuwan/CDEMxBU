using UnityEngine;
using UnityEngine.SceneManagement;  // เพิ่ม namespace นี้เพื่อให้สามารถใช้ SceneManager ของ Unity ได้

public class SceneController : MonoBehaviour
{
    // เปลี่ยน scene ตามชื่อ
    public void Play()
    {
        SceneManager.LoadScene("stage2");  // ใช้ UnityEngine.SceneManagement.SceneManager
    }

    public void Menu()
    {
        SceneManager.LoadScene("stage1");  // ใช้ UnityEngine.SceneManagement.SceneManager
    }

    public void Stage3()
    {
        SceneManager.LoadScene("stage3");  // ใช้ UnityEngine.SceneManagement.SceneManager
    }

    public void Stage4()
    {
        SceneManager.LoadScene("stage4");  // ใช้ UnityEngine.SceneManagement.SceneManager
    }

    // ออกจากเกม
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
