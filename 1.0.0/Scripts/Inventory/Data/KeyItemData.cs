using UnityEngine;

[CreateAssetMenu(fileName = "NewKeyItem", menuName = "ScriptableObjects/KeyItem")]
public class KeyItemData : ScriptableObject
{
    public string keyItemName;    // Name of the key item
    public string description;    // Description or lore of the item
    public bool isConsumable;     // Indicates if the item is consumed on use

    public string unlockCondition;  // What the key item unlocks (e.g., "Gate", "Door")
}