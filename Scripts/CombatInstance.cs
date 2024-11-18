using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;



public class CombatInstance
{
    private CombatEntity data;
    private static readonly Weapon defaultWeapon = CreateDefaultWeapon();
    
    // Properties to access the data
    public string entityName => data.entityName;
    public float maxHealth => data.maxHealth;
    public float currentHealth { get; private set; }
    public Weapon equippedWeapon => data.equippedWeapon ?? defaultWeapon;  // Use default if no weapon
    public float damage => data.damage;
    public float attackCooldown => data.attackCooldown;
    public float attackTimer { get; set; }

    private static Weapon CreateDefaultWeapon()
    {
        var weapon = ScriptableObject.CreateInstance<Weapon>();
        weapon.weaponName = "Fists";
        weapon.weaponDamage = 1;
        weapon.attackSpeed = 1f;
        return weapon;
    }

    public CombatInstance(CombatEntity entityData)
    {
        data = entityData;
    }

    public void Initialize()
    {
        currentHealth = maxHealth;
        attackTimer = 0f;
    }

    public bool TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"{entityName} health: {currentHealth}/{maxHealth}");
        return currentHealth <= 0;  // Returns true if entity is defeated
    }
}