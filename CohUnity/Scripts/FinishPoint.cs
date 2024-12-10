using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using MyGameNamespace;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    public Transform player;
    private int[] check = {2, 4, 6, 8, 10, 12, 14, 16, 18, 20};
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")  && DataManager.Qaust - 1 == check[DataManager.Level - 1])
        {
            DataManager.BoxNum=2;
            DataManager.Level += 1;
            DataManager.IndexBox = 0;
            // ResetPlayerPosition();
            DataManager.ResetPosition();
            StartCoroutine(SendMarkLevelRequest(DataManager.Level));
            SceneManager.LoadScene("Assets/Scene/SelectLevels.unity");
        }
    }

    private IEnumerator SendMarkLevelRequest(int levelId)
    {
        string api = $"http://localhost:8080/api/levels/completion-status/{levelId}";
        
        // กำหนด payload JSON
        string jsonData = "{\"status\":\"correct\"}";

        using (UnityWebRequest request = new UnityWebRequest(api, "PUT"))
        {
            byte[] jsonToSend = System.Text.Encoding.UTF8.GetBytes(jsonData); // แปลง JSON เป็น byte[]
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // ส่งคำขอและรอคำตอบ
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Level marked as correct successfully: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError($"Error marking Level as correct: {request.error}");
            }
        }
    }

    // public void ResetPlayerPosition()
    // {
    //     PlayerPrefs.DeleteAll(); 
    //     PlayerPrefs.Save();
    //     player.position = new Vector3(-6.19f, -0.37f, 0);
    //     float x = PlayerPrefs.GetFloat("PlayerX");
    //     float y = PlayerPrefs.GetFloat("PlayerY");
    //     float z = PlayerPrefs.GetFloat("PlayerZ");
    //     PlayerPrefs.Save();
    //     Debug.Log("Player position reset.");
    // }

}

