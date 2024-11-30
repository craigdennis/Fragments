using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Armor", menuName = "Inventory/Armor")]
public class Armor : Item
{
    public int baseDefense;
    public ArmorSlot slot;
    public Dictionary<ResistanceType, float> resistances = new();
    
    public float GetResistance(ResistanceType type)
    {
        return resistances.TryGetValue(type, out float value) ? value : 0f;
    }
} 