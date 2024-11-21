using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Room")]
public class Room : ScriptableObject
{
    [TextArea] public string description;
    public string roomName;
    public Exit[] exits;
    public RoomParagraphs detailedDescription;
    
    [Header("Combat Settings")]
    public bool hasCombat;
    public CombatEntityType enemyType;

    [Header("Item Pickup")]
    public Item itemInRoom;
    public string pickupButtonText = "Pick up";
    [Tooltip("Use {0} as placeholder for item name")]
    public string pickupDescription = "You picked up {0}";
    
    public bool HasValidExits => exits != null && exits.Length > 0;
    public bool HasDetailedDescription => detailedDescription != null && 
                                        detailedDescription.paragraphs != null && 
                                        detailedDescription.paragraphs.Length > 0;
    public bool HasPickableItem => itemInRoom != null;
    
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(pickupButtonText))
        {
            pickupButtonText = "Pick up";
        }
        if (string.IsNullOrEmpty(pickupDescription))
        {
            pickupDescription = "You picked up {0}";
        }
        
        if (hasCombat && enemyType == CombatEntityType.None)
        {
            Debug.LogWarning($"Room {name} has combat enabled but no enemy type set!");
        }
    }
}