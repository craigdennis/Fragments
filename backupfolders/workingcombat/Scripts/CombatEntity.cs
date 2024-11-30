using UnityEngine;

[CreateAssetMenu(menuName = "Combat/CombatEntity")]
public class CombatEntity : ScriptableObject
{
    public string entityName;
    public float attackCooldown;
    public int damage;
    public int maxHealth;
    public Weapon equippedWeapon;
    public AudioClip attackSound;
}