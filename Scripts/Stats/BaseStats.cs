using UnityEngine;

public class BaseStats : MonoBehaviour
{
    public float maxHealth = 100f;
    [HideInInspector]
    public float currentHealth;

    public float attackDamage = 10f;
    public float attackCooldown = 2f;
    [HideInInspector]
    public float cooldownTimer = 0f;

    public float defence = 0f;
    public float accuracy = 1f;

    public float luck;

    public WeaponData equippedWeapon;
    public ArmorData equippedArmor;

    // Default weapon for unarmed combat
    private static WeaponData defaultFists;

    void Awake()
    {
        // Create default fists weapon if it doesn't exist
        if (defaultFists == null)
        {
            defaultFists = ScriptableObject.CreateInstance<WeaponData>();
            defaultFists.weaponName = "Fists";
            defaultFists.baseDamage = 5f;
            defaultFists.attackCooldown = 1f;
            defaultFists.criticalChance = 0.05f;
            defaultFists.criticalMultiplier = 1.5f;
        }
    }

    public virtual void Initialize()
    {

        cooldownTimer = 0f;

        // Add armor bonuses if equipped
        if (equippedArmor != null)
        {
            maxHealth += equippedArmor.healthBonus;
            defence += equippedArmor.defence;
        }

        // Ensure there's always a weapon equipped
        if (equippedWeapon == null)
        {
            equippedWeapon = defaultFists;
        }
        // Set current health after all modifiers
        currentHealth = maxHealth;

          CombatUIManager uiManager = FindObjectOfType<CombatUIManager>();
    if (uiManager != null)
    {
        if (this is PlayerStats)
        {
            uiManager.UpdatePlayerHealth(currentHealth, maxHealth);
            }
            else if (this is EnemyStats)
            {
                uiManager.UpdateEnemyHealth(currentHealth, maxHealth);
            }
        }
    }

    public WeaponData GetWeapon()
    {
        return equippedWeapon ?? defaultFists;
    }

    public virtual void TakeDamage(float damage)
    {
        float actualDamage = Mathf.Max(damage - defence, 0);
        currentHealth = Mathf.Max(currentHealth - actualDamage, 0);
        
        // Find and update UI
        CombatUIManager uiManager = FindObjectOfType<CombatUIManager>();
        if (uiManager != null)
        {
            if (this is PlayerStats)
            {
                uiManager.UpdatePlayerHealth(currentHealth, maxHealth);
            }
            else if (this is EnemyStats)
            {
                uiManager.UpdateEnemyHealth(currentHealth, maxHealth);
            }
        }
    }

    public virtual bool IsDead()
    {
        return currentHealth <= 0;
    }
}