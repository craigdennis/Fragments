using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "ScriptableObjects/Weapon")]
public class WeaponData : ScriptableObject
{
    // Essential Stats
    public string weaponName;
    public float baseDamage;
    public float attackCooldown;

    // Optional Stats
    public float criticalChance;
    public float criticalMultiplier;

    // Special Effects
    // public StatusEffect statusEffect;
    // public float effectChance;

  public int purchasePrice;
  public int sellValue;


    // Visuals and Audio
  /*  public GameObject weaponModel;
    public AnimationClip attackAnimation;
    public AudioClip attackSound; */
}

/* public enum DamageType { Physical, Magical, Fire, Ice, Poison }
public enum StatusEffect { None, Burn, Freeze, Poison, Stun } */