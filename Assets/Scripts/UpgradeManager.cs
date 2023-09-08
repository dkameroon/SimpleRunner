using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private PlayerUpgradeData playerUpgradeData;
    [SerializeField] private UpgradeSystemUI upgradeSystemUI;

    private int coins;
    private int currentCoins;
    private int currentCostJumpForce;
    private int currentCostScoreMultiplier;
    private int currentLevelJumpForce = 1;
    private int currentLevelScoreMultiplier = 1;
    private bool IsMaximumLevelJumpForce = false;
    private bool IsMaximumLevelScoreMultiplier = false;

    private void Start()
    {
        currentLevelJumpForce = PlayerPrefs.GetInt(PlayerPrefsNames.CURRENT_LEVEL_JUMP_FORCE);
        currentLevelScoreMultiplier = PlayerPrefs.GetInt(PlayerPrefsNames.CURRENT_LEVEL_SCORE_MULTIPLIER);
        coins = PlayerPrefs.GetInt(PlayerPrefsNames.COLLECTED_COINS);
        currentCostJumpForce = playerUpgradeData.JumpForceByLevel[currentLevelJumpForce].Cost;
        currentCostScoreMultiplier = playerUpgradeData.ScoreMultiplierByLevel[currentLevelScoreMultiplier].Cost;
        UpdateVisual();
    }
    
    private void UpdateVisual()
    {
        upgradeSystemUI.UpdateCoinsText(coins);
        upgradeSystemUI.UpdateLevelJumpForceText(IsMaximumLevelJumpForce, currentLevelJumpForce);
        upgradeSystemUI.UpdateCostJumpForceText(IsMaximumLevelJumpForce, currentCostJumpForce);
        upgradeSystemUI.UpdateLevelScoreMultiplierText(IsMaximumLevelScoreMultiplier, currentLevelScoreMultiplier);
        upgradeSystemUI.UpdateCostScoreMultiplierText(IsMaximumLevelScoreMultiplier, currentCostScoreMultiplier);
    }

    private void UpdatingJumpForce()
    {
        if (coins >= currentCostJumpForce && !IsMaximumLevelJumpForce)
        {
            coins -= currentCostJumpForce;
            PlayerPrefs.SetInt(PlayerPrefsNames.CURRENT_COST_JUMP_FORCE, currentCostJumpForce);
            PlayerPrefs.SetInt(PlayerPrefsNames.COLLECTED_COINS, coins);
            PlayerPrefs.SetInt(PlayerPrefsNames.CURRENT_LEVEL_JUMP_FORCE, currentLevelJumpForce);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("No money!");
        }
    }

    private void UpdatingScoreMultiplier()
    {
        Debug.Log("0");
        if (coins >= currentCostScoreMultiplier && !IsMaximumLevelScoreMultiplier)
        {
            coins -= currentCostScoreMultiplier;
            PlayerPrefs.SetInt(PlayerPrefsNames.CURRENT_COST_SCORE_MULTIPLIER, currentCostScoreMultiplier);
            PlayerPrefs.SetInt(PlayerPrefsNames.COLLECTED_COINS, coins);
            PlayerPrefs.SetInt(PlayerPrefsNames.CURRENT_LEVEL_SCORE_MULTIPLIER, currentLevelScoreMultiplier);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("No money!");
        }
    }
    
    public void TryToUpgrade()
    {
        if (currentLevelJumpForce < 5 && !IsMaximumLevelJumpForce)
        {
            if (coins >= currentCostJumpForce)
            {
                UpdatingJumpForce();
                currentLevelJumpForce++;
                currentCostJumpForce = playerUpgradeData.JumpForceByLevel[currentLevelJumpForce].Cost;
                upgradeSystemUI.UpdateErrorMessage("");
            }
            else
            {
                upgradeSystemUI.UpdateErrorMessage("Insufficient coins");
            }
        }
        else
        {
            if (!IsMaximumLevelJumpForce && coins >= currentCostJumpForce)
            {
                UpdatingJumpForce();
            }
            IsMaximumLevelJumpForce = true;
            upgradeSystemUI.UpdateCostJumpForceText(IsMaximumLevelJumpForce, currentCostJumpForce);
            upgradeSystemUI.UpdateLevelJumpForceText(IsMaximumLevelJumpForce, currentLevelJumpForce);
            upgradeSystemUI.UpdateErrorMessage("");
        }
        UpdateVisual();
    }
    
    public void TryToUpgradeScoreMultiplier()
    {
        Debug.Log("1");
        if (currentLevelScoreMultiplier < 4 && !IsMaximumLevelScoreMultiplier)
        {
            if (coins >= currentCostJumpForce)
            {
                UpdatingScoreMultiplier();
                currentLevelScoreMultiplier++;
                currentCostScoreMultiplier = playerUpgradeData.ScoreMultiplierByLevel[currentLevelScoreMultiplier].Cost;
                upgradeSystemUI.UpdateErrorMessage("");
            }
            else
            {
                upgradeSystemUI.UpdateErrorMessage("Insufficient coins");
            }
        }
        else
        {
            if (!IsMaximumLevelScoreMultiplier && coins >= currentCostScoreMultiplier)
            {
                UpdatingScoreMultiplier();
            }
            IsMaximumLevelScoreMultiplier = true;
            upgradeSystemUI.UpdateCostScoreMultiplierText(IsMaximumLevelScoreMultiplier, currentCostScoreMultiplier);
            upgradeSystemUI.UpdateLevelScoreMultiplierText(IsMaximumLevelScoreMultiplier, currentLevelScoreMultiplier);
            upgradeSystemUI.UpdateErrorMessage("");
        }
        UpdateVisual();
    }
    
    public void ResetUpgradeSystem()
    {
        coins = 1800;
        IsMaximumLevelJumpForce = false;
        IsMaximumLevelScoreMultiplier = false;
        currentLevelJumpForce = 1;
        currentLevelScoreMultiplier = 1;
        PlayerPrefs.SetInt(PlayerPrefsNames.CURRENT_LEVEL_JUMP_FORCE, 1);
        PlayerPrefs.SetInt(PlayerPrefsNames.CURRENT_LEVEL_SCORE_MULTIPLIER, 1);
        PlayerPrefs.SetInt(PlayerPrefsNames.COLLECTED_COINS, coins);
        PlayerPrefs.Save();

        UpdateVisual();
    }
}