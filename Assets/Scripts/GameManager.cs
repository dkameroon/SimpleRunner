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
    [SerializeField] private List<Transform> wallGraffitiSpawnPoints;
    [SerializeField] private List<Transform> groundGraffitiSpawnPoints;
    [SerializeField] private List<GameObject> graffitiList;
    private int scoreMultiplierLevel;
    

    public bool IsNewHighScore;

    private List<GameObject> obstacles = new List<GameObject>();

    private void Awake()
    {
        Application.targetFrameRate = 240;
        QualitySettings.vSyncCount = 0;
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
        StartCoroutine(SpawnGroundGraffiti());
        StartCoroutine(SpawnWallGraffiti());
        currentCoins = PlayerPrefs.GetInt(PlayerPrefsNames.COLLECTED_COINS);
        scoreMultiplierLevel = PlayerPrefs.GetInt(PlayerPrefsNames.CURRENT_LEVEL_SCORE_MULTIPLIER);
        coinsText.text = currentCoins.ToString();
    }

    private void Update()
    {
        if (shouldCount)
        {
            score += Time.deltaTime * playerUpgradeData.ScoreMultiplierByLevel[scoreMultiplierLevel].Value;
            UpdateVisualScore();
        }

        if (GameOver)
        {
            Time.timeScale = 0f;
            GameOverUI.SetActive(true);
        }

        if (score > PlayerPrefs.GetFloat(PlayerPrefsNames.HIGH_SCORE, 0f))
        {
            PlayerPrefs.SetFloat(PlayerPrefsNames.HIGH_SCORE, score);
            PlayerPrefs.Save();
            IsNewHighScore = true;
        }
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

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            float waitTime = Random.Range(3f, 5f);
            yield return new WaitForSeconds(waitTime);
            int randomIndex = Random.Range(0, obstaclesPrefabs.Count);
            GameObject selectedPrefab = obstaclesPrefabs[randomIndex];
            Quaternion spawnRotation = Quaternion.Euler(0f, 90f, 0f);
            GameObject newObstacle = Instantiate(selectedPrefab, spawnPoint.position, spawnRotation);
            obstacles.Add(newObstacle);
        }
    }
    
    private IEnumerator SpawnWallGraffiti()
    {
        while (true)
        {
            float waitTime = Random.Range(4f, 6f);
            yield return new WaitForSeconds(waitTime);
            int randomIndex = Random.Range(0, graffitiList.Count);
            int randomIndexWallSpawnPoint = Random.Range(0, wallGraffitiSpawnPoints.Count);
            Transform spawnPointWall = wallGraffitiSpawnPoints[randomIndexWallSpawnPoint];
            GameObject selectedPrefab = graffitiList[randomIndex];
            Quaternion spawnRotation = Quaternion.Euler(0f, -90f, 0f);
            GameObject newWallGraffiti = Instantiate(selectedPrefab, spawnPointWall.position, spawnRotation);
            obstacles.Add(newWallGraffiti);
        }
    }
    
    private IEnumerator SpawnGroundGraffiti()
    {
        while (true)
        {
            float waitTime = Random.Range(10f, 17f);
            yield return new WaitForSeconds(waitTime);
            int randomIndex = Random.Range(0, graffitiList.Count);
            int randomIndexGroundSpawnPoint = Random.Range(0, groundGraffitiSpawnPoints.Count);
            Transform spawnPointGround = groundGraffitiSpawnPoints[randomIndexGroundSpawnPoint];
            GameObject selectedPrefab = graffitiList[randomIndex];
            Quaternion spawnRotation = Quaternion.Euler(0f, -90f, 0f);
            GameObject newGroundGraffiti = Instantiate(selectedPrefab, spawnPointGround.position, spawnRotation);
            obstacles.Add(newGroundGraffiti);
        }
    }

    private IEnumerator SpawnCoins()
    {
        while (true)
        {
            float waitTime = Random.Range(6f, 15f);
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
        StartCoroutine(nameof(SpawnObstacles));
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
            yield return new WaitForSeconds(1.0f);

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

    public void IncreaseCoins(int value)
    {
        currentCoins += value;
        coinsText.text = currentCoins.ToString();
    }

    public int GetTotalCoins()
    {
        return currentCoins;
    }
    
    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0f : 1f;

        if (isGamePaused)
        {
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
    }
    
}