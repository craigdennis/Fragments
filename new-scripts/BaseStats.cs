using UnityEngine;

public class BaseStats : MonoBehaviour
{
    public float maxHealth = 100f;
    [HideInInspector]
    public float currentHealth;

    public float attackDamage = 10f;
    public float attackCooldown = 2f; // in seconds
    [HideInInspector]
    public float cooldownTimer = 0f;

    public float defence = 0f;
    public float accuracy = 1f; // Value between 0 and 1, representing a percentage chance to hit (1 = 100%)

    public float luck;

    public WeaponData equippedWeapon;
    public ArmorData equippedArmor;
 

    public virtual void Initialize()
    {
        maxHealth += equippedArmor.healthBonus;
        currentHealth = maxHealth;
        cooldownTimer = 0f;
        defence += equippedArmor.defence;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;
    }

    public virtual bool IsDead()
    {
        return currentHealth <= 0;
    }
}