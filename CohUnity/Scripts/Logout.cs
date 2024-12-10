using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;
using MyGameNamespace;
using System.Collections;

public class LogoutManager : MonoBehaviour
{
    [System.Obsolete]
    public void OnLogoutButtonClicked()
    {
        StartCoroutine(LogoutUser());
    }

    [System.Obsolete]
    private IEnumerator LogoutUser()
    {
        string url = "http://localhost:8080/api/users/logout";

        UnityWebRequest www = UnityWebRequest.Post(url, string.Empty); 
        www.SetRequestHeader("Content-Type", "application/json"); 

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Logout error.");
        }
        else
        {

            DataManager.UserID = null;
            SceneManager.LoadScene("Assets/Scene/Login.unity");
        }
    }
}
