using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class InventorySaveData
{
    public List<string> weaponIds = new();
    public List<string> armorIds = new();
    public List<string> potionIds = new();
    public List<string> foodIds = new();
    public List<string> keyItemIds = new();
    public string equippedWeaponId;
    public Dictionary<ArmorSlot, string> equippedArmorIds = new();
} 