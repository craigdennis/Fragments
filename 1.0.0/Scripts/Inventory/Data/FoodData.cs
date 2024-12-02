using UnityEngine;

[CreateAssetMenu(fileName = "NewFood", menuName = "ScriptableObjects/Food")]
public class FoodData : ScriptableObject
{
    public string foodName;       // Name of the food
    public int healthRecovery;    // Amount of health restored
    public string description;    // Description of the item

    public int purchasePrice;     // Price to buy the item
    public int sellValue;         // Resale value
}