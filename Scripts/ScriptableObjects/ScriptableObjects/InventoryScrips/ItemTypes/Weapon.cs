using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    [Header("Base Stats")]
      public string weaponName;      // Match CombatInstance property
    public int weaponDamage;       // Match CombatInstance property
    public float attackSpeed;
    [Range(-0.5f, 0.5f)]
    public float accuracyModifier;
    
       [Header("Condition Effects")]
    public ConditionType conditionType;
    public float conditionChance;
    public float conditionDuration;
    
    [Header("Condition-Specific Values")]
    public float cooldownIncrease;     // For Cold
    public float freezeDuration;       // For Shock
    public int poisonDamage;           // For Poison
    public float accuracyReduction;    // For Blindness
    
    [Header("Durability")]
    public float maxDurability;
    public float currentDurability;
}