using UnityEngine;

[CreateAssetMenu(fileName = "NewPotion", menuName = "ScriptableObjects/Potion")]
public class PotionData : ScriptableObject
{
    public string potionName;     // Name of the potion
    public int healthRecovery;    // Amount of health restored
    public bool isRare;           // Indicates if the potion is rare
    public string description;    // Description of the potion

    public int purchasePrice;     // Price to buy the potion
    public int sellValue;         // Resale value
}