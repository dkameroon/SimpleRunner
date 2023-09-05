using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject congratulationText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button resetButton;
    [SerializeField] private GameManager gameManager;
    


    private void Awake() {
        restartButton.onClick.AddListener(() => {
            SceneManager.LoadScene("GameScene");
            Time.timeScale = 1f;
        });
        menuButton.onClick.AddListener(() => {
            SceneManager.LoadScene("MainMenuScene");
        });
        resetButton.onClick.AddListener(() => {
            ResetHighScore();
        });
        
    }

    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        float loadedHighScore = PlayerPrefs.GetFloat("HighScore");
        highScoreText.text = "High Score : " + Mathf.FloorToInt(loadedHighScore).ToString();;
        if (GameManager.Instance.IsNewHighScore)
        {
            congratulationText.SetActive(true);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ResetHighScore()
    {
        PlayerPrefs.SetFloat("HighScore", 0f);
        PlayerPrefs.Save();
    }
    
}
