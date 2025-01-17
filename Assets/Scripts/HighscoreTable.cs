using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class HighscoreTable : MonoBehaviour {

    [SerializeField] private Button quitButton;
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake() {
        PlayerPrefs.DeleteKey("highscoreTable");
        quitButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

       if (highscores == null) {
            Debug.Log("Initializing table with default values...");
            AddHighscoreEntry(1200, "CMK");
            AddHighscoreEntry(800, "JOE");
            AddHighscoreEntry(763, "DAV");
            AddHighscoreEntry(592, "CAT");
            AddHighscoreEntry(560, "MAX");
            AddHighscoreEntry(510, "AAA");
            jsonString = PlayerPrefs.GetString("highscoreTable");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }
        bool playerHasRecord = false;

        float playerScoreString = PlayerPrefs.GetFloat(PlayerPrefsNames.HIGH_SCORE);
        int playerScore = Mathf.RoundToInt(playerScoreString);
        Debug.Log(playerScore);


        
        if (highscores.highscoreEntryList.Count < 10 || playerScore > highscores.highscoreEntryList[highscores.highscoreEntryList.Count - 1].score)
        {
            HighscoreEntry playerEntry = new HighscoreEntry { score = playerScore, name = "Player" };

            highscores.highscoreEntryList.Add(playerEntry);

            highscores.highscoreEntryList.Sort((a, b) => b.score.CompareTo(a.score));

            if (highscores.highscoreEntryList.Count > 10)
            {
                highscores.highscoreEntryList.RemoveAt(10);
            }

            playerHasRecord = true;
        }

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++) {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++) {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score) {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList) {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 200f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank) {
        default:
            rankString = rank + "th"; break;

        case 1: rankString = "1st"; break;
        case 2: rankString = "2nd"; break;
        case 3: rankString = "3rd"; break;
        }

        entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = name;

        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);

        if (name == "Player") {
            entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().color = Color.green;
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().color = Color.green;
        } 

        switch (rank) {
        default:
            entryTransform.Find("trophy").gameObject.SetActive(false);
            break;
        case 1:
            entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("FFD200");
            break;
        case 2:
            entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("C6C6C6");
            break;
        case 3:
            entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("B76F56");
            break;

        }

        transformList.Add(entryTransform);
    }

    private void AddHighscoreEntry(int score, string name) {

        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };
    
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null) {
            highscores = new Highscores() {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }
        highscores.highscoreEntryList.Add(highscoreEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores {
        public List<HighscoreEntry> highscoreEntryList;
    }


    [System.Serializable] 
    private class HighscoreEntry {
        public int score;
        public string name;
    }

}
