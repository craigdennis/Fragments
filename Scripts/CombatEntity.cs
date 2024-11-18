using UnityEngine;

[CreateAssetMenu(menuName = "Combat/CombatEntity")]
public class CombatEntity : ScriptableObject
{
    public string entityName;
    public float attackCooldown; // Time between attacks
    public int damage;
    public int maxHealth;

    public Weapon equippedWeapon;
    public AudioClip attackSound;

    [HideInInspector] public int currentHealth;

    public void Initialize()
    {
        currentHealth = maxHealth;
    }

    public bool TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            return true; // Entity is defeated
        }
        return false;
    }
}
