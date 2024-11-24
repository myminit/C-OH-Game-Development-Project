using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using System.Collections;
using System.Collections.Generic;
using MyGameNamespace;

public class LevelManager : MonoBehaviour
{
    public Image[] levelImages; 
    public Sprite[] levelCompleteSprites; 
    public Sprite[] levelIncompleteSprites; 

    private string apiUrl = "http://localhost:8080/api/levels/playTable";
    private List<LevelStatus> levelStatuses = new List<LevelStatus>();

    private Dictionary<int, bool> previousStatuses = new Dictionary<int, bool>();

    void Start()
    {
        int userId = int.Parse(DataManager.UserID);
 
        StartCoroutine(GetLevelStatusFromAPI(userId));

        for (int i = 0; i < levelImages.Length; i++)
        {
            int levelIndex = i + 1; 
            levelImages[i].GetComponent<Button>().onClick.AddListener(() => AttemptToLoadLevel(levelIndex));
        }
    }

    IEnumerator GetLevelStatusFromAPI(int userId)
{
    // Using userId as a query parameter
    string url = apiUrl + "?userId=" + userId;

    using (UnityWebRequest www = UnityWebRequest.Get(url))
    {
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = www.downloadHandler.text;

            if (jsonResponse.StartsWith("[") && jsonResponse.EndsWith("]"))
            {
                LevelStatusListWrapper wrapper = JsonUtility.FromJson<LevelStatusListWrapper>("{\"levelStatuses\":" + jsonResponse + "}");

                if (wrapper != null)
                {
                    levelStatuses = wrapper.levelStatuses;
                    UpdateLevelStatus();
                }
                else
                {
                    Debug.LogError("Failed to parse JSON: " + jsonResponse);
                }
            }
            else
            {
                Debug.LogError("Invalid JSON format: " + jsonResponse);
            }
        }
        else
        {
            Debug.LogError("Request failed: " + www.error + " | URL: " + url);
        }
    }
}


    void UpdateLevelStatus()
    {
        for (int i = 0; i < levelStatuses.Count; i++)
        {
            LevelStatus status = levelStatuses[i];

            if (!previousStatuses.ContainsKey(status.levelId) || previousStatuses[status.levelId] != status.complete)
            {
                Image levelImage = levelImages[status.levelId - 1];
                
                if (status.complete)
                {
                    levelImage.sprite = levelCompleteSprites[status.levelId - 1];
                    AdjustLevelImageSize(levelImage, true); 
                }
                else
                {
                    levelImage.sprite = levelIncompleteSprites[status.levelId - 1];
                    AdjustLevelImageSize(levelImage, false); 
                }

                levelImage.preserveAspect = true;

                previousStatuses[status.levelId] = status.complete;
            }
        }
    }

    private void AdjustLevelImageSize(Image levelImage, bool isCompleted)
    {
        RectTransform rectTransform = levelImage.GetComponent<RectTransform>();

        if (isCompleted)
        {
            rectTransform.sizeDelta = new Vector2(180, 180);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }
        else
        {
            rectTransform.sizeDelta = new Vector2(180, 180); 
        }
    }

    private void AttemptToLoadLevel(int levelIndex)
    {
        LevelStatus status = levelStatuses.Find(ls => ls.levelId == levelIndex);

        if (status != null && status.complete)
        {
            LoadLevelScene(levelIndex);
        }
    }

    private void LoadLevelScene(int levelIndex)
    {
        string sceneName = "Level" + levelIndex; // Scene ชื่อเป็น "Level1", "Level2", ..., "Level10"
        // SceneManager.LoadScene(sceneName);
    }

    [System.Serializable]
    public class LevelStatus
    {
        public int userId;
        public int levelId;
        public bool complete;
    }

    [System.Serializable]
    public class LevelStatusListWrapper
    {
        public List<LevelStatus> levelStatuses;
    }
}
