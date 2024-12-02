using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Lists to store items by category
    public List<WeaponData> weaponItems = new List<WeaponData>();
    public List<ArmorData> armorItems = new List<ArmorData>();
    public List<FoodData> foodItems = new List<FoodData>();
    public List<PotionData> potionItems = new List<PotionData>();
    public List<KeyItemData> keyItems = new List<KeyItemData>();

    // Add an item to the appropriate category list
    public void AddItem(ScriptableObject item)
    {
        if (item is WeaponData weapon)
        {
            weaponItems.Add(weapon);
            Debug.Log($"Added weapon: {weapon.weaponName}");
        }
        else if (item is ArmorData armor)
        {
            armorItems.Add(armor);
            Debug.Log($"Added armor: {armor.armorName}");
        }
        else if (item is FoodData food)
        {
            foodItems.Add(food);
            Debug.Log($"Added food: {food.foodName}");
        }
        else if (item is PotionData potion)
        {
            potionItems.Add(potion);
            Debug.Log($"Added potion: {potion.potionName}");
        }
        else if (item is KeyItemData keyItem)
        {
            keyItems.Add(keyItem);
            Debug.Log($"Added key item: {keyItem.keyItemName}");
        }
        else
        {
            Debug.LogWarning("Attempted to add an unsupported item type.");
        }
    }

    // Remove an item from the appropriate category list
    public void RemoveItem(ScriptableObject item)
    {
        if (item is WeaponData weapon)
        {
            weaponItems.Remove(weapon);
            Debug.Log($"Removed weapon: {weapon.weaponName}");
        }
        else if (item is ArmorData armor)
        {
            armorItems.Remove(armor);
            Debug.Log($"Removed armor: {armor.armorName}");
        }
        else if (item is FoodData food)
        {
            foodItems.Remove(food);
            Debug.Log($"Removed food: {food.foodName}");
        }
        else if (item is PotionData potion)
        {
            potionItems.Remove(potion);
            Debug.Log($"Removed potion: {potion.potionName}");
        }
        else if (item is KeyItemData keyItem)
        {
            keyItems.Remove(keyItem);
            Debug.Log($"Removed key item: {keyItem.keyItemName}");
        }
        else
        {
            Debug.LogWarning("Attempted to remove an unsupported item type.");
        }
    }

    // Retrieve all items of a specific category
    public List<ScriptableObject> GetItemsByCategory(string category)
    {
        switch (category.ToLower())
        {
            case "weapon":
                return new List<ScriptableObject>(weaponItems);
            case "armor":
                return new List<ScriptableObject>(armorItems);
            case "food":
                return new List<ScriptableObject>(foodItems);
            case "potion":
                return new List<ScriptableObject>(potionItems);
            case "keyitem":
                return new List<ScriptableObject>(keyItems);
            default:
                Debug.LogWarning("Invalid category specified.");
                return null;
        }
    }
}