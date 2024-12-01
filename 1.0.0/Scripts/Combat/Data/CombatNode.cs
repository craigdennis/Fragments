using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewCombatNode", menuName = "Combat Node", order = 54)]
public class CombatNode : ScriptableObject
{
    [Header("Node Information")]
    public string nodeName;

    [Header("Story Node")]
    public StoryNode storyNode;

    [Header("Enemies")]
    public List<EnemyData> enemyDataList;

    [Header("Environment")]
    public Sprite backgroundImage;

    [Header("Difficulty Settings")]
    public DifficultyLevel difficulty;

    [Header("Rewards")]
    public List<Reward> rewards;

    [Header("Special Conditions")]
    public string specialConditions; // Or a more complex data structure

    // You can add methods here if needed
}

public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard,
    Insane
}

[System.Serializable]
public class Reward
{
    public string rewardName;
    public int amount;
    // Add other reward-related fields
}