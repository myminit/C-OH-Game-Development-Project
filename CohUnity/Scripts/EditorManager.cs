using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
using MyGameNamespace;
using System.Collections.Concurrent;
using UnityEngine.SceneManagement;

public class EditorManager : MonoBehaviour
{
    [Header("Description Panel")]
    public GameObject descriptionPanel;
    public TextMeshProUGUI gameTitleText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI descriptionText;
    public AudioSource sound;

    public Button nextButton;

    [Header("Code Editor Panel")]
    public GameObject codeEditorPanel;
    public TextMeshProUGUI exampleText;
    public TMP_InputField userInputField;
    public Button submitButton;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI settimeText;
    public GameObject feedbackPopup; 
    public TextMeshProUGUI feedbackText; 
    public GameObject agentCharacter;
    public Button closeButton; 

    [Header("Code Tester")]
    public CodeTester codeTester; 
    private bool isCorrectCheck = false;


    // ตัวแปรจำลองข้อมูลที่มาจากฐานข้อมูล
    private string questTitle = "Level 1";
    private string questDescription = "You have just left your home and ventured into the dark, mysterious forest. Your journey is dangerous, and you only have 3 hearts to begin with. In this quest, you need to write a program that declares an integer variable to store the number of hearts you have, reminding you of your limited health as you continue your adventure.";
    private string questTask = "Write a program that declares an integer variable to store the number of hearts in a game.Your program should print 'You have 3 hearts at the start.' to the console.";
    private string questHint = "Use `int` to declare an integer variable, and `printf()` to print it.";
    private string correctAnswer = "You have 3 hearts at the start.";

    private DateTime startTime; // บันทึกเวลาเริ่มต้น
    private bool isQuestComplete = false; // ติดตามสถานะการทำเควส
    private List<data> questData = new List<data>();

    private int currentQuestId = DataManager.Qaust;
    private string apiQ;
    private string apiM = "http://localhost:8080/api/quests/mark-correct";
    private int levelBack;

    void Start()
    {
        isCorrectCheck = false;
        // ตั้งค่าเริ่มต้นให้ Description Panel แสดงผล
        descriptionPanel.SetActive(true);
        codeEditorPanel.SetActive(false);

        nextButton.onClick.AddListener(GoToCodeEditor);
        closeButton.onClick.AddListener(CloseFeedbackPopup);
        submitButton.onClick.AddListener(CheckAnswer);
        Debug.Log(currentQuestId);
        apiQ = $"http://localhost:8080/api/quests/{currentQuestId}";
        StartCoroutine(FetchQuestData());
    }

    IEnumerator FetchQuestData()
{
    using (UnityWebRequest request = UnityWebRequest.Get(apiQ))
    {
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            try
            {
                data quest = JsonConvert.DeserializeObject<data>(request.downloadHandler.text);

                if (quest != null)
                {
                    questTitle = "level " + quest.LevelId;
                    levelBack = quest.LevelId;
                    questDescription = quest.Spoil;
                    questTask = quest.QuestQ;
                    questHint = quest.Hint;
                    correctAnswer = quest.QuestA;

                    Debug.Log(correctAnswer);

                    UpdateQuest(); 
                }
                else
                {
                    Debug.LogWarning("ข้อมูลของเควสเป็นค่าว่าง (null)");
                }
            }
            catch (Exception e)
            {
                Debug.LogError("เกิดข้อผิดพลาดในการแปลง JSON: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("ไม่สามารถดึงข้อมูลเควสได้: " + request.error);
        }
    }
}


    private void UpdateQuest()
    {
        gameTitleText.text = "COH! Coding Game";
        levelText.text = questTitle;
        descriptionText.text = questDescription;
    }

    private void GoToCodeEditor()
    {
        if (!isQuestComplete)
        {
            startTime = DateTime.Now;
            isQuestComplete = false;
            StartCoroutine(UpdateTimer());
        }
        
        // ซ่อน Description Panel และแสดง Code Editor Panel
        descriptionPanel.SetActive(false);
        codeEditorPanel.SetActive(true);
        feedbackPopup.SetActive(false);

        // รีเซ็ตข้อมูลต่างๆ ใน Code Editor Panel
        feedbackText.text = "";
        userInputField.text = "";
        exampleText.text = questTask + "\n<color=#FBDD4D><b>Hint:</b></color> " + questHint;

    }

    private System.Collections.IEnumerator UpdateTimer()
    {
        // อัปเดตเวลาทุกๆ 1 วินาที
        while (!isQuestComplete)
        {
            TimeSpan timeElapsed = DateTime.Now - startTime;
            string timeFormatted = string.Format("{0:D2}:{1:D2}:{2:D2}", timeElapsed.Minutes, timeElapsed.Seconds, timeElapsed.Milliseconds / 10);
            timerText.text = timeFormatted; // แสดงเวลาใน Code Editor Panel
            yield return new WaitForSeconds(0.1f); 
        }
    }
    private void CheckAnswer()
    {
        string userCode = userInputField.text.Trim();

        if (codeTester == null)
        {
            feedbackText.text = "Error: CodeTester not assigned!";
            Debug.LogError("CodeTester is not assigned in the Inspector!");
            return; 
        }

        feedbackPopup.SetActive(false);
        feedbackText.text = ""; 
        agentCharacter.SetActive(false);
        
        // ใช้ CodeTester เพื่อตรวจสอบโค้ด
        string expectedOutput = correctAnswer.Trim(); // ใช้ expectedOutput จากตัวแปรที่เตรียมไว้
        codeTester.TestCode(expectedOutput, userCode, (isCorrect) =>
        {
            isCorrectCheck = isCorrect;
            if (isCorrect)
            {
                // หยุดจับเวลาเมื่อคำตอบถูกต้อง
                isQuestComplete = true;
                TimeSpan timeElapsed = DateTime.Now - startTime; 
                MarkQuestAsCorrect(currentQuestId, timeElapsed);
                string timeFormatted = string.Format("{0:D2}:{1:D2}:{2:D2}", timeElapsed.Minutes, timeElapsed.Seconds, timeElapsed.Milliseconds / 10);

                settimeText.text = $"{timeFormatted}";

                feedbackPopup.SetActive(true);
                feedbackText.text = $"Correct!\nYou completed the quest.";
                agentCharacter.SetActive(false);
                
                // Invoke(nameof(CloseEditorPanel), 2.5f); 
                // GetComponent<Collider2D>().enabled = false;
                DataManager.Qaust+=1;
                DataManager.BoxNum-=1;
                // SceneManager.LoadScene("Scene/Levels/level "+ levelBack);
                Debug.Log(DataManager.Qaust);
            }
            else
            {
                feedbackText.text = "Wrong! Try again.";
                feedbackPopup.SetActive(true);
                agentCharacter.SetActive(true);
            }
        });
    }

    public void CloseFeedbackPopup()
    {
        feedbackPopup.SetActive(false); 
        if(isCorrectCheck){
            SceneManager.LoadScene("Scene/Levels/level "+ levelBack);
        };
    }

    private void CloseEditorPanel()
    {
        
        codeEditorPanel.SetActive(false);
        descriptionPanel.SetActive(false);

        if (sound != null)
        {
            sound.Stop();
        }

        // อัปเดตข้อมูลของเควสถัดไป (ในอนาคตอาจดึงจากฐานข้อมูล)
        UpdateQuest();
    }

    public void MarkQuestAsCorrect(int questId, TimeSpan time)
    {
        StartCoroutine(SendMarkCorrectRequest(questId, time));
    }

    private IEnumerator SendMarkCorrectRequest(int questId, TimeSpan time)
    {
        double totalSeconds = time.TotalSeconds;
        Debug.Log(totalSeconds);
        // สร้างฟอร์มข้อมูลที่จะส่งไปในคำขอ
        WWWForm form = new WWWForm();
        form.AddField("questId", questId);
        form.AddField("time", totalSeconds.ToString());

        using (UnityWebRequest request = UnityWebRequest.Put(apiM, form.data))
        {
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            yield return request.SendWebRequest(); // ส่งคำขอและรอคำตอบ

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Quest marked as correct successfully: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError($"Error marking quest as correct: {request.error}");
            }
        }
    }

}

public class data
{
    public int QuestId { get; set; }
    public int LevelId { get; set; }
    public string QuestQ { get; set; }
    public string QuestA { get; set; }
    public string Spoil { get; set; }
    public string Hint { get; set; }
}