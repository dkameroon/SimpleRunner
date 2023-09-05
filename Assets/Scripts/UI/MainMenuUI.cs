using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private GameObject upgradeSystem;
   
   
    public static MainMenuUI Instance { get;private set; }
       private void Awake()
       {
           Instance = this;
           playButton.onClick.AddListener(() =>
           {
               SceneManager.LoadScene("GameScene");
           });
           upgradeButton.onClick.AddListener(() =>
           {
               upgradeSystem.gameObject.SetActive(true);
           });
   
           Time.timeScale = 1f;
       }
    
}
