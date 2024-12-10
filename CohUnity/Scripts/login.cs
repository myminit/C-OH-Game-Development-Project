using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using MyGameNamespace;
using Newtonsoft.Json;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_Text feedbackText;

    [System.Obsolete]
    public void OnLoginButtonClicked()
    {
        string username = this.username.text;
        string password = this.password.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "Please fill in both fields";
            feedbackText.color = Color.red;  
            return;
        }

        StartCoroutine(LoginUser(username, password));
    }

    [System.Obsolete]
    private IEnumerator LoginUser(string username, string password)
    {
        string url = "http://localhost:8080/api/users/login";

        var loginData = new
        {
            username = username,
            password = password
        };

        string jsonData = JsonConvert.SerializeObject(loginData);

        UnityWebRequest www = new UnityWebRequest(url, "POST");
        www.SetRequestHeader("Content-Type", "application/json");

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            feedbackText.text = "Password or username is incorrect";
            feedbackText.color = Color.red;
        }
        else
        {
            string jsonResponse = www.downloadHandler.text;

            if (jsonResponse.StartsWith("{"))
            {
                LoginResponse response = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);

                if (response.status == "success")
                {
                    DataManager.UserID = response.userID;

                    feedbackText.text = "Login Successful";
                    feedbackText.color = new Color(0.067f, 0.686f, 0.153f);

                    SceneManager.LoadScene("Assets/Scene/SelectLevels.unity");
                }
            }
        }
    }
}

[System.Serializable]
public class LoginResponse
{
    public string status;
    public string userID;
    public string message;
}
