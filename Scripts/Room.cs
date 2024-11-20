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

    public bool HasValidExits => exits != null && exits.Length > 0;
    public bool HasDetailedDescription => detailedDescription != null && 
                                        detailedDescription.paragraphs != null && 
                                        detailedDescription.paragraphs.Length > 0;
    
    private void OnValidate()
    {
        if (hasCombat && enemyType == CombatEntityType.None)
        {
            Debug.LogWarning($"Room {name} has combat enabled but no enemy type set!");
        }
    }
}