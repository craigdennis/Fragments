using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory Categories")]
    [SerializeField] private CategoryInventory weaponInventory = new() { maxSlots = 10 };
    [SerializeField] private CategoryInventory armorInventory = new() { maxSlots = 10 };
    [SerializeField] private CategoryInventory potionInventory = new() { maxSlots = 5 };
    [SerializeField] private CategoryInventory foodInventory = new() { maxSlots = 5 };
    [SerializeField] private CategoryInventory keyItemInventory = new() { maxSlots = 20 };
    
    [Header("Equipment")]
    private Weapon equippedWeapon;
    private Dictionary<ArmorSlot, Armor> equippedArmor = new();
    
    public event System.Action OnInventoryChanged;
    public event System.Action OnEquipmentChanged;

    private void Awake()
    {
        // Initialize inventory categories with default slots if not set
        if (weaponInventory.maxSlots <= 0) weaponInventory.maxSlots = 10;
        if (armorInventory.maxSlots <= 0) armorInventory.maxSlots = 10;
        if (potionInventory.maxSlots <= 0) potionInventory.maxSlots = 5;
        if (foodInventory.maxSlots <= 0) foodInventory.maxSlots = 5;
        if (keyItemInventory.maxSlots <= 0) keyItemInventory.maxSlots = 20;
    }

    public List<Weapon> GetWeapons() => weaponInventory.items.OfType<Weapon>().ToList();
    public List<Armor> GetArmor() => armorInventory.items.OfType<Armor>().ToList();
    public List<Potion> GetPotions() => potionInventory.items.OfType<Potion>().ToList();
    public List<Food> GetFood() => foodInventory.items.OfType<Food>().ToList();
    public List<KeyItem> GetKeyItems() => keyItemInventory.items.OfType<KeyItem>().ToList();

    public bool AddItem(Item item)
    {
        CategoryInventory targetInventory = item switch
        {
            Weapon _ => weaponInventory,
            Armor _ => armorInventory,
            Potion _ => potionInventory,
            Food _ => foodInventory,
            KeyItem _ => keyItemInventory,
            _ => null
        };

        if (targetInventory == null)
        {
            Debug.LogWarning($"No matching inventory category found for item type: {item.GetType().Name}");
            return false;
        }

        if (targetInventory.IsFull)
        {
            Debug.LogWarning($"Cannot add {item.itemName} - {targetInventory.GetType().Name} is full! ({targetInventory.items.Count}/{targetInventory.maxSlots})");
            return false;
        }

        bool added = targetInventory.AddItem(item);
        if (added)
        {
            Debug.Log($"Added {item.itemName} to inventory");
            OnInventoryChanged?.Invoke();
        }
        return added;
    }

    public bool RemoveItem(Item item)
    {
        CategoryInventory targetInventory = item switch
        {
            Weapon _ => weaponInventory,
            Armor _ => armorInventory,
            Potion _ => potionInventory,
            Food _ => foodInventory,
            KeyItem _ => keyItemInventory,
            _ => null
        };

        if (targetInventory == null) return false;
        bool removed = targetInventory.RemoveItem(item);
        if (removed) OnInventoryChanged?.Invoke();
        return removed;
    }

    // Equipment Methods
    public bool EquipWeapon(Weapon weapon)
    {
        if (weapon == null) return false;
        
        // Unequip current weapon if any
        if (equippedWeapon != null)
        {
            UnequipWeapon();
        }
        
        equippedWeapon = weapon;
        weapon.isEquipped = true;
        OnEquipmentChanged?.Invoke();
        return true;
    }

    public bool UnequipWeapon()
    {
        if (equippedWeapon == null) return false;
        
        equippedWeapon.isEquipped = false;
        equippedWeapon = null;
        OnEquipmentChanged?.Invoke();
        return true;
    }

    public bool EquipArmor(Armor armor)
    {
        if (armor == null) return false;
        
        // Unequip current armor in that slot if any
        if (equippedArmor.ContainsKey(armor.slot))
        {
            UnequipArmor(armor.slot);
        }
        
        equippedArmor[armor.slot] = armor;
        armor.isEquipped = true;
        OnEquipmentChanged?.Invoke();
        return true;
    }

    public bool UnequipArmor(ArmorSlot slot)
    {
        if (!equippedArmor.ContainsKey(slot)) return false;
        
        equippedArmor[slot].isEquipped = false;
        equippedArmor.Remove(slot);
        OnEquipmentChanged?.Invoke();
        return true;
    }

    public bool ValidateInventorySetup()
    {
        // Check if inventory categories are properly initialized
        if (weaponInventory == null || armorInventory == null || 
            potionInventory == null || foodInventory == null || 
            keyItemInventory == null)
        {
            return false;
        }

        // Check if max slots are valid
        if (weaponInventory.maxSlots <= 0 || armorInventory.maxSlots <= 0 ||
            potionInventory.maxSlots <= 0 || foodInventory.maxSlots <= 0 ||
            keyItemInventory.maxSlots <= 0)
        {
            return false;
        }

        return true;
    }
}

public static class GameFlags
{
    private static HashSet<string> activeFlags = new();
    
    public static bool HasFlag(string flag) => activeFlags.Contains(flag);
    public static void SetFlag(string flag) => activeFlags.Add(flag);
    public static void ClearFlag(string flag) => activeFlags.Remove(flag);
}