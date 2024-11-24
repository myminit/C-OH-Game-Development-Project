using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public void GoToRegisterScene()
    {
        SceneManager.LoadScene("Assets/Scene/Register.unity");
    }
    public void GoToLoginScene()
    {
        SceneManager.LoadScene("Assets/Scene/Login.unity"); 
    }
}
