using UnityEngine;


public class TestInventoryPopulation : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private Weapon[] testWeapons;
    [SerializeField] private Armor[] testArmor;
    [SerializeField] private Potion[] testPotions;

    [ContextMenu("Populate Test Items")]
    public void PopulateTestItems()
    {
        foreach (var weapon in testWeapons)
        {
            playerInventory.AddItem(weapon);
        }
        foreach (var armor in testArmor)
        {
            playerInventory.AddItem(armor);
        }
        foreach (var potion in testPotions)
        {
            playerInventory.AddItem(potion);
        }
    }

    [ContextMenu("Clear Inventory")]
    public void ClearInventory()
    {
        // Implementation depends on your PlayerInventory clear method
        // You might need to add this method to PlayerInventory
    }
} 