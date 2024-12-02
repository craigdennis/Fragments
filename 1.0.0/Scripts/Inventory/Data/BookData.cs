using UnityEngine;

[CreateAssetMenu(fileName = "NewBook", menuName = "ScriptableObjects/Book")]
public class BookData : KeyItemData
{
    // The stat this book affects (e.g., max health, attack damage)
    public string affectedStat;

    // The amount by which the stat is increased
    public float statIncreaseAmount;

    // Override to ensure books cannot be sold
    public override int GetSellValue()
    {
        return 0; // Books cannot be sold
    }

    // Method to apply the book's effect to the player stats
    public void ApplyEffect(PlayerStats playerStats)
    {
        switch (affectedStat.ToLower())
        {
            case "maxhealth":
                playerStats.maxHealth += statIncreaseAmount;
                break;
            case "attackdamage":
                playerStats.attackDamage += statIncreaseAmount;
                break;
            case "defence":
                playerStats.defence += statIncreaseAmount;
                break;
            case "accuracy":
                playerStats.accuracy += statIncreaseAmount;
                break;
            case "luck":
                playerStats.luck += statIncreaseAmount;
                break;
            default:
                Debug.LogWarning($"Stat '{affectedStat}' is not valid for modification.");
                break;
        }

        // Ensure the current health reflects the updated max health
        if (affectedStat.ToLower() == "maxhealth")
        {
            playerStats.currentHealth = playerStats.maxHealth;
        }

        Debug.Log($"{itemName} used. {affectedStat} increased by {statIncreaseAmount}.");
    }
}