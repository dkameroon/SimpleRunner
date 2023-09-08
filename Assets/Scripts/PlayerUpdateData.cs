using UnityEngine;

[CreateAssetMenu(fileName = "PlayerUpgrade", menuName = "Player/Upgrade")]
public class PlayerUpgradeData : ScriptableObject
{
    public UpgradeItem[] JumpForceByLevel;
    public UpgradeItem[] ScoreMultiplierByLevel;
}

[System.Serializable]
public struct UpgradeItem
{
    public float Value;
    public int Cost;
}