using UnityEngine;
using UnityEngine.SceneManagement;
using MyGameNamespace;
using Unity.VisualScripting;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    // [SerializeField] AudioSource musicSource;
    public Transform player;
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }


    public void Home()
    {
        SceneManager.LoadScene("Scene/SelectLevels");
    //    ResetPlayerPosition();
        DataManager.ResetPosition();
        DataManager.IndexBox = 0;
    //    musicSource.Stop();
       Time.timeScale = 1;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        // ResetPlayerPosition();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        DataManager.IndexBox = 0;
        DataManager.ResetPosition();
        Time.timeScale = 1;
    }

    // public void ResetPlayerPosition()
    // {
    //     player.position = new Vector3(-6.19f, -0.37f, 0);
    //     float x = PlayerPrefs.GetFloat("PlayerX");
    //     float y = PlayerPrefs.GetFloat("PlayerY");
    //     float z = PlayerPrefs.GetFloat("PlayerZ");
    //     PlayerPrefs.Save();
        
    //     player.position = new Vector3(x, y, z);
    //     Debug.Log("Player position reset.");
    // }
}

