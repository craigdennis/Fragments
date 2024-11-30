using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemy Data", order = 51)]
public class EnemyData : ScriptableObject
{
    public string enemyName = "Enemy";
    public float maxHealth = 100f;
    public float attackDamage = 10f;
    public float attackCooldown = 2f;
    public float accuracy = 0.8f; //Default accuracy is 80%
    public float defence = 5f;
    // Add other enemy-specific stats as needed

    public WeaponData defaultWeapon;

    public ArmorData defaultArmor;

}


    
    