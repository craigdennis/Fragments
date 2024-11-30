using UnityEngine;



public class CombatInstance
{
    private CombatEntity data;
    private static readonly Weapon defaultWeapon = CreateDefaultWeapon();
    public bool IsPlayer { get; private set; }
    
    // Properties to access the data
    public string entityName => data.entityName;
    public float maxHealth => data.maxHealth;
    public float currentHealth { get; private set; }
    public Weapon equippedWeapon => data.equippedWeapon ?? defaultWeapon;
    public float damage => data.damage;
    public float attackCooldown => data.attackCooldown;

    private static Weapon CreateDefaultWeapon()
    {
        var weapon = ScriptableObject.CreateInstance<Weapon>();
        weapon.weaponName = "Fists";
        weapon.weaponDamage = 1;
        weapon.attackSpeed = 1f;
        return weapon;
    }

    public CombatInstance(CombatEntity entityData, bool isPlayer = false)
    {
        data = entityData;
        IsPlayer = isPlayer;
    }

    public void Initialize()
    {
        currentHealth = maxHealth;
    }

    public bool TakeDamage(float damageAmount)
    {
        currentHealth = Mathf.Max(0, currentHealth - damageAmount);
        
        if (IsPlayer)
        {
            GameEvents.RaisePlayerHealthChanged(currentHealth, maxHealth);
        }
        else
        {
            GameEvents.RaiseEnemyHealthChanged(currentHealth, maxHealth);
        }
        
        return currentHealth <= 0;
    }
}