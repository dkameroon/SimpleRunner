using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button recordsButton;
    [SerializeField] private GameObject upgradeSystem;
    [SerializeField] private GameObject highscoresTables;
   
   
    public static MainMenuUI Instance { get;private set; }
       private void Awake()
       {
           Application.targetFrameRate = 240;
           QualitySettings.vSyncCount = 0;
           Instance = this;
           playButton.onClick.AddListener(() =>
           {
               SceneManager.LoadScene("GameScene");
           });
           upgradeButton.onClick.AddListener(() =>
           {
               upgradeSystem.gameObject.SetActive(true);
           });
           recordsButton.onClick.AddListener(() =>
           {
               highscoresTables.gameObject.SetActive(true);
           });
   
           Time.timeScale = 1f;
       }
    
}
