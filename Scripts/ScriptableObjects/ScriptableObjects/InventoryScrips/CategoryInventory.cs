using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CategoryInventory
{
    public int maxSlots = 50;
    public List<Item> items = new();
    
    public bool IsFull => items.Count >= maxSlots;
    
    public bool AddItem(Item item)
    {
        if (maxSlots <= 0)
        {
            Debug.LogWarning("Inventory category has invalid max slots (0 or less)");
            return false;
        }
        
        if (IsFull)
        {
            Debug.LogWarning($"Cannot add item - inventory is full ({items.Count}/{maxSlots})");
            return false;
        }
        
        items.Add(item);
        return true;
    }
    
    public bool RemoveItem(Item item)
    {
        return items.Remove(item);
    }
}