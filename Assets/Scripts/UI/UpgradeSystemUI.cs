using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UpgradeSystemUI : MonoBehaviour
{
    [SerializeField] private Button quitButton;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Button jumpForceUpgradeButton;
    [SerializeField] private Button scoreMultipierUpgradeButton;
    [SerializeField] private Button resetButton;
    [SerializeField] private TextMeshProUGUI levelOfUpgradeJumpForceText;
    [SerializeField] private TextMeshProUGUI costOfUpgradeJumpForceText;
    [SerializeField] private TextMeshProUGUI levelOfUpgradeScoreMultiplierText;
    [SerializeField] private TextMeshProUGUI costOfUpgradeScoreMultiplierText;
    [SerializeField] private PlayerUpgradeData playerUpgradeData;
    [SerializeField] private TextMeshProUGUI errorMessageText;
    
    [SerializeField] private UpgradeManager upgradeManager;

    private void Awake() {
        errorMessageText.text = "";
        quitButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        jumpForceUpgradeButton.onClick.AddListener(() =>
        {
            upgradeManager.TryToUpgrade();
            SoundManager.Instance.PlayLevelUpSound(Camera.main.transform.position,1f);
        });
        scoreMultipierUpgradeButton.onClick.AddListener(() =>
        {
            upgradeManager.TryToUpgradeScoreMultiplier();
            SoundManager.Instance.PlayLevelUpSound(Camera.main.transform.position,1f);
        });
        
        resetButton.onClick.AddListener(() =>
        {
            upgradeManager.ResetUpgradeSystem();
            costOfUpgradeJumpForceText.text = "Cost : 20";
            levelOfUpgradeJumpForceText.text = "Level : 1";
            costOfUpgradeScoreMultiplierText.text = "Cost : 60";
            levelOfUpgradeScoreMultiplierText.text = "Level : 1";
        });
    }

    private void Start()
    {
        Hide();
    }
    
    public void UpdateCoinsText(int coins)
    {
        coinsText.text = coins.ToString();
    }

    public void UpdateLevelJumpForceText(bool isMaximumLevel, int jumpForce)
    {
        levelOfUpgradeJumpForceText.text = isMaximumLevel ? "Level: Max" : "Level: " + jumpForce;
    }

    public void UpdateCostJumpForceText(bool isMaximumLevel, int currentCost)
    {
        costOfUpgradeJumpForceText.text = isMaximumLevel ? "" : "Cost: " + currentCost;
    }
    
    public void UpdateLevelScoreMultiplierText(bool isMaximumLevel, int jumpForce)
    {
        levelOfUpgradeScoreMultiplierText.text = isMaximumLevel ? "Level: Max" : "Level: " + jumpForce;
    }

    public void UpdateCostScoreMultiplierText(bool isMaximumLevel, int currentCost)
    {
        costOfUpgradeScoreMultiplierText.text = isMaximumLevel ? "" : "Cost: " + currentCost;
    }

    public void UpdateErrorMessage(string message)
    {
        errorMessageText.text = message;
    }
    
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
