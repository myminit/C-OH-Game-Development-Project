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
    public RectTransform character;

    private string apiUrl = "http://localhost:8080/api/levels/playTable";
    private List<LevelStatus> levelStatuses = new List<LevelStatus>();
    private Dictionary<int, bool> previousStatuses = new Dictionary<int, bool>();

    private Vector2[] levelPositions = new Vector2[]
    {
        new Vector2(-676f, 268f),
        new Vector2(-384f, 268f),
        new Vector2(-85f, 268f),
        new Vector2(217f, 268f),
        new Vector2(505f, 268f),
        new Vector2(770f, 268f),
        new Vector2(-542f, -30f),
        new Vector2(-242f, -30f),
        new Vector2(351f, -30f),
        new Vector2(679f, -30f),
    };

    void Start()
    {
        int userId = int.Parse(DataManager.UserID);
        StartCoroutine(GetLevelStatusFromAPI(userId));
        DataManager.IndexBox = 0;
        DataManager.BoxNum = 2;

        for (int i = 0; i < levelImages.Length; i++)
        {
            int levelIndex = i + 1; 
            levelImages[i].GetComponent<Button>().onClick.AddListener(() => AttemptToLoadLevel(levelIndex));
        }
        Time.timeScale = 1;
    }

    IEnumerator GetLevelStatusFromAPI(int userId)
    {
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

        UpdateCharacterPosition();
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

    private void UpdateCharacterPosition()
    {
        LevelStatus lastPlayableLevel = null;

        foreach (var status in levelStatuses)
        {
            if (status.complete)
            {
                lastPlayableLevel = status;
            }
        }

        if (lastPlayableLevel != null)
        {
            int levelId = lastPlayableLevel.levelId;

            if (levelId > 0 && levelId <= levelPositions.Length)
            {
                Vector2 targetPosition = levelPositions[levelId - 1];
                character.anchoredPosition = targetPosition;
            }
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
        DataManager.ResetPosition();
        string sceneName = "level " + levelIndex; // Scene ชื่อเป็น "Level1", "Level2", ..., "Level10"
        DataManager.Level = levelIndex;
        DataManager.Qaust = levelIndex*2-1;
        SceneManager.LoadScene("Scene/Levels/"+sceneName);
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
