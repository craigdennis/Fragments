 using UnityEngine;

public class ItemPickupManager : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;

    public void AttemptPickup(Item item)
    {
        if (playerInventory.AddItem(item))
        {
            Debug.Log($"Picked up {item.itemName}");
            // Optionally, trigger an event or update UI
        }
        else
        {
            Debug.Log("Inventory full or item cannot be picked up.");
            // Optionally, notify the player
        }
    }
}