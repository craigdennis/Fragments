using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Key Item", menuName = "Inventory/Key Item")]
public class KeyItem : Item
{
    [Header("Key Item Properties")]
    public string uniqueId;          // Unique identifier for this key item
    public bool isQuestItem;         // Is this item part of a quest
    public string questId;           // Associated quest ID
    public InteractionType interactionType;
    public string[] requiredFlags;   // Game state flags required to use this item
    
    public bool CanUse()
    {
        // Check if all required flags are met
        foreach (var flag in requiredFlags)
        {
            if (!GameFlags.HasFlag(flag))  // You'll need to implement GameFlags system
                return false;
        }
        return true;
    }
    
    public bool UseKeyItem()
    {
        if (!CanUse()) return false;
        
        switch (interactionType)
        {
            case InteractionType.Unlock:
                // Unlock something
                break;
            case InteractionType.Quest:
                // Progress quest
                break;
            case InteractionType.Trade:
                // Enable trading
                break;
        }
        return true;
    }
} 