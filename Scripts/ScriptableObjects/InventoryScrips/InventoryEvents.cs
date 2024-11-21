using UnityEngine;

public static class InventoryEvents
{
    public static event System.Action<Item> OnItemAdded;
    public static event System.Action<Item> OnItemRemoved;
    public static event System.Action<Item> OnItemUsed;
    public static event System.Action<Weapon> OnWeaponEquipped;
    public static event System.Action<Armor> OnArmorEquipped;
    public static event System.Action OnInventoryFull;

    public static void RaiseItemAdded(Item item) => OnItemAdded?.Invoke(item);
    public static void RaiseItemRemoved(Item item) => OnItemRemoved?.Invoke(item);
    public static void RaiseItemUsed(Item item) => OnItemUsed?.Invoke(item);
    public static void RaiseWeaponEquipped(Weapon weapon) => OnWeaponEquipped?.Invoke(weapon);
    public static void RaiseArmorEquipped(Armor armor) => OnArmorEquipped?.Invoke(armor);
    public static void RaiseInventoryFull() => OnInventoryFull?.Invoke();
} 