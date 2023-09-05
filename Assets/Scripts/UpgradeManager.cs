using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private PlayerUpgradeData playerUpgradeData;
    [SerializeField] private UpgradeSystemUI upgradeSystemUI;

    private int coins;
    private int currentCoins;
    private int currentCost;
    private int currentLevel = 1;
    private bool IsMaximumLevel = false;

    private void Start()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        coins = PlayerPrefs.GetInt("CollectedCoins");
        currentCost = playerUpgradeData.JumpForceByLevel[currentLevel].Cost;
        UpdateVisual();
    }
    
    private void UpdateVisual()
    {
        upgradeSystemUI.UpdateCoinsText(coins);
        upgradeSystemUI.UpdateLevelText(IsMaximumLevel, currentLevel);
        upgradeSystemUI.UpdateCostText(IsMaximumLevel, currentCost);
    }

    private void UpdatingJumpForce()
    {
        if (coins >= currentCost && !IsMaximumLevel)
        {
            coins -= currentCost;
            PlayerPrefs.SetInt("CurrentCostJumpForce", currentCost);
            PlayerPrefs.SetInt("CollectedCoins", coins);
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("No money!");
        }
    }

    public void TryToUpgrade()
    {
        if (currentLevel < 5 && !IsMaximumLevel)
        {
            if (coins >= currentCost)
            {
                UpdatingJumpForce();
                currentLevel++;
                currentCost = playerUpgradeData.JumpForceByLevel[currentLevel].Cost;
                upgradeSystemUI.UpdateErrorMessage("");
            }
            else
            {
                upgradeSystemUI.UpdateErrorMessage("Insufficient coins");
            }
        }
        else
        {
            if (!IsMaximumLevel && coins >= currentCost)
            {
                UpdatingJumpForce();
            }
            IsMaximumLevel = true;
            upgradeSystemUI.UpdateCostText(IsMaximumLevel, currentCost);
            upgradeSystemUI.UpdateLevelText(IsMaximumLevel, currentLevel);
            upgradeSystemUI.UpdateErrorMessage("");
        }
        UpdateVisual();
    }
    
    public void ResetUpgradeSystem()
    {
        // Reset relevant variables and PlayerPrefs here
        coins = 1800;
        IsMaximumLevel = false;
        currentLevel = 1;
        PlayerPrefs.SetInt("CurrentLevel", 0);
        PlayerPrefs.SetInt("CollectedCoins", coins);
        PlayerPrefs.Save();

        UpdateVisual();
    }
}