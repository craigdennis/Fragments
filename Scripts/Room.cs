using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Room")]
public class Room : ScriptableObject
{
    [TextArea] public string description;
    public string roomName;
    public Exit[] exits;
    
    [Header("Combat Settings")]
    public bool hasCombat;
    public CombatEntityType enemyType;

    public bool HasValidExits => exits != null && exits.Length > 0;
    
    private void OnValidate()
    {
        if (hasCombat && enemyType == CombatEntityType.None)
        {
            Debug.LogWarning($"Room {name} has combat enabled but no enemy type set!");
        }
    }
}