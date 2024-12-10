using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections;
using MyGameNamespace;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject leaderboardItemPrefab;
    public Transform contentPanel;

    public TMP_Text Rank;
    public TMP_Text Time;

    private List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();

    private const string ApiUrl = "http://localhost:8080/api/ranking";

    void Start()
    {
        StartCoroutine(FetchLeaderboardData());
    }

    IEnumerator FetchLeaderboardData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(ApiUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                try
                {
                    leaderboardEntries = JsonConvert.DeserializeObject<List<LeaderboardEntry>>(request.downloadHandler.text);
                    if (leaderboardEntries == null || leaderboardEntries.Count == 0)
                    {
                        Debug.LogWarning("Leaderboard data is empty or null.");
                        yield break;
                    }
                    DisplayLeaderboard();
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error parsing JSON: " + e.Message);
                }
            }
            else
            {
                Debug.LogError("Failed to fetch leaderboard data: " + request.error);
            }
        }
    }

    void DisplayLeaderboard()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        int currentRank = 1;

        foreach (LeaderboardEntry entry in leaderboardEntries)
        {
            GameObject newItem = Instantiate(leaderboardItemPrefab, contentPanel);
            TextMeshProUGUI[] texts = newItem.GetComponentsInChildren<TextMeshProUGUI>();
            RectTransform panel = newItem.GetComponent<RectTransform>();

            if (texts.Length >= 4)
            {

                double totalTime = entry.totalTime;

                int minutes = (int)(totalTime / 60); // นาที
                int seconds = (int)(totalTime % 60); // วินาที
                int milliseconds = (int)((totalTime - (int)totalTime) * 1000); // มิลลิวินาที
                texts[0].text = currentRank.ToString();
                texts[1].text = entry.name;
                texts[2].text = entry.lastLevel;
                texts[3].text = string.Format("{0:D2}:{1:D2}:{2:D2}", minutes, seconds, milliseconds / 10);

                if (int.Parse(DataManager.UserID) == entry.userId)
                {
                    Rank.text = currentRank.ToString();
                    // Time.text = entry.totalTime.ToString("F2");

                    Time.text = string.Format("{0:D2}:{1:D2}:{2:D2}", minutes, seconds, milliseconds / 10);
                    panel.GetComponent<UnityEngine.UI.Image>().color = new Color(0.067f, 0.686f, 0.153f);
                }
            }

            if (currentRank == 1)
                panel.GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 0.84f, 0.0f);
            else if (currentRank == 2)
                panel.GetComponent<UnityEngine.UI.Image>().color = new Color(0.75f, 0.75f, 0.75f);
            else if (currentRank == 3)
                panel.GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.5f, 0.2f);


            currentRank++;
        }
    }
}

public class LeaderboardEntry
{
    public int rankId { get; set; }
    public int userId { get; set; }
    public string name { get; set; }
    public string lastLevel { get; set; }
    public float totalTime { get; set; }
}
