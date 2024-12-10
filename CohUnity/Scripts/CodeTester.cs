using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using Newtonsoft.Json;
using TMPro;

public class CodeTester : MonoBehaviour
{
    public TextMeshProUGUI output;
    private const string pistonAPIUrl = "https://emkc.org/api/v2/piston/execute";

    public void TestCode(string expectedOutput, string userCode, Action<bool> callback)
    {
        if (callback == null)
        {
            Debug.LogError("Callback is null!");
            return;
        }

        string language = "c++";
        string version = "10.2.0";

        StartCoroutine(RunCodeInPiston(userCode, language, version, expectedOutput, callback));
    }

    private IEnumerator RunCodeInPiston(string userCode, string language, string version, string expectedOutput, Action<bool> callback)
    {
        // สร้างอ็อบเจ็กต์สำหรับส่งคำขอ
        var jsonRequest = new
        {
            language = language,
            version = version,
            files = new[]
            {
                new { name = "main", content = userCode } 
            },
            stdin = "" 
        };

        // ใช้ Newtonsoft.Json แปลงอ็อบเจ็กต์เป็น JSON
        string jsonData = JsonConvert.SerializeObject(jsonRequest);
        Debug.Log("JSON Data: " + jsonData);

        // สร้างคำขอ HTTP
        UnityWebRequest request = new UnityWebRequest(pistonAPIUrl, UnityWebRequest.kHttpVerbPOST)
        {
            uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData)),
            downloadHandler = new DownloadHandlerBuffer()
        };
        request.SetRequestHeader("Content-Type", "application/json");

        // ส่งคำขอและรอผลลัพธ์
        yield return request.SendWebRequest();
        

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;

            // แปลงผลลัพธ์ JSON ที่ได้รับกลับมา
            var responseData = JsonConvert.DeserializeObject<PistonResponse>(responseText);

            if (responseData != null && responseData.run != null)
            {
                string outputAPI = responseData.run.output.Trim();

                Debug.Log($"Output from API: {outputAPI}");
                output.text = outputAPI;
                Debug.Log($"Expected output: \n{expectedOutput}");

                // ตรวจสอบว่าผลลัพธ์ตรงกับคำตอบที่คาดหวังหรือไม่
                bool isCorrect = string.Equals(outputAPI, expectedOutput.Trim(), StringComparison.OrdinalIgnoreCase);
                callback(isCorrect);
            }
            else
            {
                Debug.LogError("Invalid response structure: " + responseText);
                callback(false);
            }
        }
        else
        {
            Debug.LogError("Request failed: " + request.error);
            callback(false);
        }
    }

    // คลาสสำหรับแปลง JSON ของ API Response
    public class PistonResponse
    {
        public RunResult run;
    }

    public class RunResult
    {
        public string output;
    }
}



