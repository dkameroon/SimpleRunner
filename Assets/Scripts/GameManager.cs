using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get;private set; }
    
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;
    
    [SerializeField] private float score;
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform coinSpawnPoint;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI coinsText;
    
    [SerializeField] private Button pauseButton;
    [SerializeField] private PlayerUpgradeData playerUpgradeData;
    private int currentCoins = 0;
    public bool shouldCount = true;
    public static bool GameOver;
    private bool isGamePaused = false;
    [SerializeField] private List<GameObject> obstaclesPrefabs;
    private int scoreMultiplierLevel;
    

    public bool IsNewHighScore;

    private List<GameObject> obstacles = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        pauseButton.onClick.AddListener(() =>
        {
            TogglePauseGame();
        });
    }

    private void Start()
    {
        IsNewHighScore = false;
        GameOver = false;
        Time.timeScale = 1f;
        StartGameCoroutine();
        StartCoroutine(RemoveObstacles());
        StartCoroutine(SpawnCoins());
        currentCoins = PlayerPrefs.GetInt(PlayerPrefsNames.COLLECTED_COINS, 0);
        scoreMultiplierLevel = PlayerPrefs.GetInt(PlayerPrefsNames.CURRENT_LEVEL_SCORE_MULTIPLIER);
        coinsText.text = currentCoins.ToString();
    }

    private void Update()
    {
        Debug.Log(scoreMultiplierLevel);
        ScoreRewarding();
        GameOverTrue();
        HighScoreSaving();
    }

    private void ScoreRewarding()
    {
        if (shouldCount)
        {
            score += Time.deltaTime * playerUpgradeData.ScoreMultiplierByLevel[scoreMultiplierLevel].Value;
            UpdateVisualScore();
        }
    }
    private void GameOverTrue()
    {
        if (GameOver == true)
        {
            Time.timeScale = 0f;
            GameOverUI.SetActive(true);
        }
    }

    private IEnumerator SpawnObstaclesAndCoins()
    {
        while (true)
        {
            float waitTime = Random.Range(1f, 2f);
            yield return new WaitForSeconds(waitTime);
            int randomIndex = Random.Range(0, obstaclesPrefabs.Count);
            GameObject selectedPrefab = obstaclesPrefabs[randomIndex];
            GameObject newObstacle = Instantiate(selectedPrefab, spawnPoint.position, Quaternion.identity);
            obstacles.Add(newObstacle);
        }
    }

    private IEnumerator SpawnCoins()
    {
        while (true)
        {
            float waitTime = Random.Range(5f, 10f);
            yield return new WaitForSeconds(waitTime);
            
            Instantiate(coinPrefab, coinSpawnPoint.position, Quaternion.identity);
        }
    }
    

    private void HighScoreSaving()
    {
        if (score > PlayerPrefs.GetFloat(PlayerPrefsNames.HIGH_SCORE,0f))
        {
            PlayerPrefs.SetFloat(PlayerPrefsNames.HIGH_SCORE, score);
            PlayerPrefs.Save();
            IsNewHighScore = true;
        }
    }

    private void UpdateVisualScore()
    {
        if (!shouldCount) return;
        scoreText.text = "Score : " + Mathf.FloorToInt(score).ToString();
        
    }

    private void StartGameCoroutine()
    {
        StartCoroutine(nameof(SpawnObstaclesAndCoins));
    }

    public void StartGame()
    {
        shouldCount = true;
        StartGameCoroutine();
        SpawnCoins();
    }

    
    private IEnumerator RemoveObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f); // Adjust the frequency as needed.

            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                Vector3 playerPosition = player.transform.position;

                for (int i = obstacles.Count - 1; i >= 0; i--)
                {
                    GameObject obstacle = obstacles[i];

                    if (obstacle != null)
                    {
                        Vector3 obstaclePosition = obstacle.transform.position;

                        // Check if the obstacle is behind the player (in the negative Z direction).
                        if (obstaclePosition.z + 3.5f < playerPosition.z)
                        {
                            obstacles.RemoveAt(i);
                            Destroy(obstacle);
                        }
                    }
                    else
                    {
                        obstacles.RemoveAt(i);
                    }
                }
            }
        }
    }

    public void IncreaseCoins(int v)
    {
        currentCoins += v;
        coinsText.text = currentCoins.ToString();
    }
    
    public int GetTotalCoins()
    {
        return currentCoins;
    }
    
    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this,EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
    }
    
}