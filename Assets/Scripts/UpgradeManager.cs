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
    private int jumpForce = 1;
    private int finalJumpForce;
    private bool IsMaximumLevel = false;

    private void Start()
    {
        coins = PlayerPrefs.GetInt("CollectedCoins");
        currentCost = playerUpgradeData.cost;
        UpdateVisual();
    }
    
    private void UpdateVisual()
    {
        upgradeSystemUI.UpdateCoinsText(coins);
        upgradeSystemUI.UpdateLevelText(IsMaximumLevel, jumpForce);
        upgradeSystemUI.UpdateCostText(IsMaximumLevel, currentCost);
    }

    private void UpdatingJumpForce()
    {
        if (coins >= currentCost && !IsMaximumLevel)
        {
            coins -= currentCost;
            PlayerPrefs.SetInt("CurrentCostJumpForce", currentCost);
            PlayerPrefs.SetInt("CollectedCoins", coins);
            PlayerPrefs.SetInt("JumpForce", jumpForce);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("No money!");
        }
    }

    public void TryToUpgrade()
    {
        if (jumpForce < 3 && !IsMaximumLevel)
        {
            if (coins >= currentCost)
            {
                UpdatingJumpForce();
                currentCost += 20;
                jumpForce++;
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
            upgradeSystemUI.UpdateLevelText(IsMaximumLevel, jumpForce);
            upgradeSystemUI.UpdateErrorMessage("");
        }
        UpdateVisual();
    }
    
    public void ResetUpgradeSystem()
    {
        // Reset relevant variables and PlayerPrefs here
        coins = 300;
        currentCost = 20;
        IsMaximumLevel = false;
        jumpForce = 1;

        PlayerPrefs.SetInt("JumpForce", 0);
        PlayerPrefs.SetInt("CollectedCoins", 300);
        PlayerPrefs.SetInt("CurrentCostJumpForce", 20);
        PlayerPrefs.Save();

        UpdateVisual();
    }
}