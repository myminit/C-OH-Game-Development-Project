using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using Newtonsoft.Json;

public class SignUpManager : MonoBehaviour
{
    public TMP_InputField username;  
    public TMP_InputField email;     
    public TMP_InputField password; 
    public TMP_InputField confirmPassword;  
    public TMP_Text feedbackText; 

    public void OnSignUpButtonClicked()
    {
        if (string.IsNullOrEmpty(username.text) || string.IsNullOrEmpty(email.text) || 
            string.IsNullOrEmpty(password.text) || string.IsNullOrEmpty(confirmPassword.text))
        {
            feedbackText.text = "Please fill in all fields";
            feedbackText.color = Color.red;
            return;
        }

        if (password.text != confirmPassword.text)  
        {
            feedbackText.text = "Passwords do not match";
            feedbackText.color = Color.red; 
            return;
        }

        StartCoroutine(RegisterUser(username.text, email.text, password.text));
    }

    private IEnumerator RegisterUser(string username, string email, string password)
    {
        string url = "http://localhost:8080/api/users/register";

        var jsonData = new
    {
        username = username,
        email = email,
        password = password
    };

        string json = JsonConvert.SerializeObject(jsonData);

        UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();

        // UnityWebRequest www = new UnityWebRequest(url, "POST")
        // {
        //     uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(v)),
        //     downloadHandler = new DownloadHandlerBuffer()
        // };

        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            feedbackText.text = "Error: " + www.error;
            feedbackText.color = Color.red; 
            Debug.Log(www.error);
        }
        else
        {

            if (www.responseCode == 200)
            {
                feedbackText.text = "Registration successful!";
                feedbackText.color = new Color(0.067f, 0.686f, 0.153f); 
            }
            else
            {
                feedbackText.text = "Registration failed! Response Code: " + www.responseCode;
                feedbackText.color = Color.red;
            }
        }
    }
}
