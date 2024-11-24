using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Food", menuName = "Inventory/Food")]
public class Food : Item
{
    [Header("Healing Properties")]
    public int healthRestored;
    public float consumeTime;
    public bool isPerishable;
    public float spoilTime;
    
    public bool UseFood()
    {
        if (isPerishable && spoilTime <= 0) return false;
        // Implementation for using food would go here
        return true;
    }
} 