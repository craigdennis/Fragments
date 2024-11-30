using UnityEngine;

public class CombatManager : MonoBehaviour
{
    // References to the Player and Enemy Stats
    public PlayerStats playerStats;

    // Reference to EnemyData ScriptableObject
    public EnemyData enemyData;

    // Reference to the UI Manager
    public CombatUIManager uiManager;

    // Internal EnemyStats instance
    private EnemyStats enemyStats;

    void Start()
    {
        // Initialize player stats
        playerStats.Initialize();

        // Initialize enemy stats using the EnemyData
        InitializeEnemy();

        // Update UI at the start
        uiManager.UpdatePlayerHealth(playerStats.currentHealth, playerStats.maxHealth);
        uiManager.UpdateEnemyHealth(enemyStats.currentHealth, enemyStats.maxHealth);
    }

    void InitializeEnemy()
    {
        // Create a new GameObject for the enemy
        GameObject enemyObject = new GameObject("Enemy");
        enemyStats = enemyObject.AddComponent<EnemyStats>();

        // Assign the EnemyData to the EnemyStats
        enemyStats.enemyData = enemyData;

        // Initialize the EnemyStats
        enemyStats.Initialize();
    }

    void Update()
    {
        // Update cooldown timers
        if (playerStats.cooldownTimer > 0)
            playerStats.cooldownTimer -= Time.deltaTime;

        if (enemyStats.cooldownTimer > 0)
            enemyStats.cooldownTimer -= Time.deltaTime;
        else
        {
            EnemyAttack();
            enemyStats.cooldownTimer = enemyStats.attackCooldown; // Reset enemy cooldown
        }

        // Check for end of combat
        if (playerStats.IsDead())
        {
            Debug.Log("Player has been defeated!");
            uiManager.ShowMessage("You have been defeated!");
            // Implement defeat logic (e.g., disable input, show game over screen)
        }

        if (enemyStats.IsDead())
        {
            Debug.Log("Enemy has been defeated!");
            uiManager.ShowMessage("Enemy has been defeated!");
            // Implement victory logic (e.g., loot drops, experience gain)
        }
    }



    private float CalculateDamage(BaseStats attacker, BaseStats defender, WeaponData weapon)
    {
        float damage = weapon.baseDamage;
        damage += attacker.attackDamage;

        if (Random.value <= weapon.criticalChance)
        {
            damage *= weapon.criticalMultiplier;
        }

        damage -= defender.defence;
        return Mathf.Max(0, damage);
    }


    


    // Call this method when the player presses the attack button
    public void PlayerAttack()
    {
        if (playerStats.cooldownTimer <= 0f)
        {
            // Perform accuracy check
            if (IsAttackSuccessful(playerStats.accuracy))
            {
                // Attack hits

                WeaponData weapon = playerStats.GetWeapon();
                if (weapon == null)
                {
                    Debug.LogWarning("No weapon equipped!");
                    return;
                }
                float damage = CalculateDamage(playerStats, enemyStats, weapon);
                enemyStats.TakeDamage(damage);
                uiManager.UpdateEnemyHealth(enemyStats.currentHealth, enemyStats.maxHealth);
                Debug.Log("Player attacked the enemy and hit!");

                // Check if enemy is defeated
                if (enemyStats.IsDead())
                {
                    Debug.Log("Enemy has been defeated!");
                    uiManager.ShowMessage("Enemy has been defeated!");
                    // Implement victory logic
                }
            }
            else
            {
                // Attack misses
                Debug.Log("Player's attack missed!");
                uiManager.ShowMessage("Your attack missed!");
            }

            // Reset cooldown
            playerStats.cooldownTimer = playerStats.attackCooldown;
        }
        else
        {
            Debug.Log("Player attack is on cooldown!");
        }
    }

    void EnemyAttack()
    {
        if (!enemyStats.IsDead())
        {
            // Perform accuracy check
            if (IsAttackSuccessful(enemyStats.accuracy))
            {
                // Attack hits
                playerStats.TakeDamage(enemyStats.attackDamage);
                uiManager.UpdatePlayerHealth(playerStats.currentHealth, playerStats.maxHealth);
                Debug.Log("Enemy attacked the player and hit!");

                // Check if player is defeated
                if (playerStats.IsDead())
                {
                    Debug.Log("Player has been defeated!");
                    uiManager.ShowMessage("You have been defeated!");
                    // Implement defeat logic
                }
            }
            else
            {
                // Attack misses
                Debug.Log("Enemy's attack missed!");
                uiManager.ShowMessage("Enemy's attack missed!");
            }

            // Reset enemy cooldown (already handled in Update())
        }
    }

    // Helper method to determine if an attack is successful based on accuracy
    private bool IsAttackSuccessful(float accuracy)
    {
        float randomValue = Random.value; // Returns a value between 0.0 and 1.0
        return randomValue <= accuracy;
    }

    // Optional: Method to get player's cooldown percentage for UI display
    public float GetPlayerCooldownPercent()
    {
        return Mathf.Clamp01(playerStats.cooldownTimer / playerStats.attackCooldown);
    }
}