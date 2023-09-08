using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystemUI : MonoBehaviour
{
    [SerializeField] private Button quitButton;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Button jumpForceUpgradeButton;
    [SerializeField] private Button resetButton;
    [SerializeField] private TextMeshProUGUI levelOfUpgradeText;
    [SerializeField] private TextMeshProUGUI costOfUpgradeText;
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
        });
        resetButton.onClick.AddListener(() =>
        {
            upgradeManager.ResetUpgradeSystem();
            costOfUpgradeText.text = "Cost : 20";
            levelOfUpgradeText.text = "Level : 1";
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

    public void UpdateLevelText(bool isMaximumLevel, int jumpForce)
    {
        levelOfUpgradeText.text = isMaximumLevel ? "Level: Max" : "Level: " + jumpForce;
    }

    public void UpdateCostText(bool isMaximumLevel, int currentCost)
    {
        costOfUpgradeText.text = isMaximumLevel ? "" : "Cost: " + currentCost;
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
