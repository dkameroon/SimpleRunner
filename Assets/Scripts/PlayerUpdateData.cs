using UnityEngine;

[CreateAssetMenu(fileName = "PlayerUpgrade", menuName = "Player/Upgrade")]
public class PlayerUpgradeData : ScriptableObject
{
    public int cost;
    public int levelOfUpgrade;
}