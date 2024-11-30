using UnityEngine;

public class EnemyStats : BaseStats
{
    public EnemyData enemyData;

    public override void Initialize()
    {
        if (enemyData != null)
        {
            maxHealth = enemyData.maxHealth;
            attackDamage = enemyData.attackDamage;
            attackCooldown = enemyData.attackCooldown;
            accuracy = enemyData.accuracy;

            defence = enemyData.defence;



            
            // Assign accuracy from EnemyData
            // Initialize other stats as needed
        }
        else
        {
            Debug.LogError("EnemyData is not assigned to EnemyStats.");
        }

        base.Initialize();

        // Set the enemy's cooldownTimer to attackCooldown so the enemy waits before attacking
        cooldownTimer = attackCooldown;
    }
}