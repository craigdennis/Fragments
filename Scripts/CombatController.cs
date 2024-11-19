using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CombatController : MonoBehaviour
{
    [SerializeField] private CombatEntity playerData;
    [SerializeField] private CombatEntity guardData;
    [SerializeField] private Button attackButton;
    [SerializeField] private TextMeshProUGUI enemyHealthText;
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private TextMeshProUGUI combatResultText;

    private CombatInstance player;
    private CombatInstance enemy;
    private bool isCombatOver = false;
    private float playerAttackTimer = 0f;
    private float enemyAttackTimer = 0f;

    void Start()
    {
        Debug.Log("Combat Controller Starting...");

        // Check if ScriptableObjects are assigned
        if (playerData == null)
        {
            Debug.LogError("PlayerData ScriptableObject is not assigned!");
            return;
        }

        if (guardData == null)
        {
            Debug.LogError("GuardData ScriptableObject is not assigned!");
            return;
        }

        // Create player instance
        player = new CombatInstance(playerData);
        player.Initialize();
        Debug.Log($"Player initialized with health: {player.currentHealth}/{player.maxHealth}");

        // Get enemy type from PlayerPrefs
        CombatEntityType enemyType = (CombatEntityType)PlayerPrefs.GetInt("EnemyType", 0);
        Debug.Log($"Loading enemy type: {enemyType}");

        // Create enemy instance
        CombatEntity enemyData = GetEnemyData(enemyType);
        if (enemyData == null)
        {
            Debug.LogError($"No enemy data found for type: {enemyType}");
            return;
        }

        enemy = new CombatInstance(enemyData);
        enemy.Initialize();
        Debug.Log($"Enemy initialized with health: {enemy.currentHealth}/{enemy.maxHealth}");

        // Set up UI
        if (attackButton == null)
        {
            Debug.LogError("Attack Button not assigned in inspector!");
            return;
        }

        // Clear and add listener
        attackButton.onClick.RemoveAllListeners();
        attackButton.onClick.AddListener(OnAttackButtonPressed);
        Debug.Log("Attack button listener added");

        UpdateHealthDisplay();
    }

    private CombatEntity GetEnemyData(CombatEntityType type)
    {
        switch(type)
        {
            case CombatEntityType.Guard:
                return guardData;
            // Add other cases as needed
            default:
                Debug.LogError($"Unknown enemy type: {type}");
                return guardData; // Default to guard if unknown
        }
    }

    void Update()
    {
        if (isCombatOver) return;

        // Update attack timers
        if (playerAttackTimer > 0)
        {
            playerAttackTimer -= Time.deltaTime;
            attackButton.interactable = false;  // Disable button during cooldown
        }
        else
        {
            attackButton.interactable = true;   // Enable button when ready
        }
        
        if (enemyAttackTimer > 0)
        {
            enemyAttackTimer -= Time.deltaTime;
        }
        else if (!isCombatOver)
        {
            EnemyAttack();
        }
    }

    public void OnAttackButtonPressed()
    {
        if (isCombatOver || playerAttackTimer > 0)
        {
            Debug.Log($"Can't attack: {(isCombatOver ? "Combat over" : $"Cooldown: {playerAttackTimer:F1}s")}");
            return;
        }

        if (player == null || enemy == null)
        {
            Debug.LogError($"Player is null: {player == null}, Enemy is null: {enemy == null}");
            return;
        }

        // Perform attack and set cooldown
        bool isDefeated = enemy.TakeDamage(player.equippedWeapon.weaponDamage);
        playerAttackTimer = player.attackCooldown;
        Debug.Log($"{player.entityName} attacked {enemy.entityName} for {player.equippedWeapon.weaponDamage} damage!");
        
        UpdateHealthDisplay();

        if (isDefeated)
        {
            EndCombat($"{enemy.entityName} has been defeated!");
        }
    }

    private void EnemyAttack()
    {
        if (isCombatOver) return;

        bool isDefeated = player.TakeDamage(enemy.damage);
        enemyAttackTimer = enemy.attackCooldown;  // Reset enemy cooldown
        Debug.Log($"{enemy.entityName} attacked {player.entityName} for {enemy.damage} damage!");
        
        UpdateHealthDisplay();

        if (isDefeated)
        {
            EndCombat($"{player.entityName} has been defeated!");
        }
    }

    void UpdateHealthDisplay()
    {
        if (enemyHealthText != null && enemy != null)
        {
            string cooldownInfo = playerAttackTimer > 0 ? $" (Attack ready in: {playerAttackTimer:F1}s)" : " (Ready!)";
            enemyHealthText.text = $"{enemy.entityName} Health: {Mathf.Max(0, enemy.currentHealth)}/{enemy.maxHealth}{cooldownInfo}";
        }
        
        if (playerHealthText != null && player != null)
        {
            string cooldownInfo = enemyAttackTimer > 0 ? $" (Enemy attacks in: {enemyAttackTimer:F1}s)" : " (Attacking!)";
            playerHealthText.text = $"Player Health: {Mathf.Max(0, player.currentHealth)}/{player.maxHealth}{cooldownInfo}";
        }
    }

    private void EndCombat(string result)
    {
        isCombatOver = true;
        attackButton.interactable = false;
        
        if (combatResultText != null)
        {
            combatResultText.text = result;
            combatResultText.gameObject.SetActive(true);
        }
        
        StartCoroutine(ProceedToNextRoom());
    }

    private IEnumerator ProceedToNextRoom()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainGameScene");
    }

    private IEnumerator HandlePlayerDefeat()
    {
        yield return new WaitForSeconds(2f);
        // You can customize what happens on defeat
        SceneManager.LoadScene("MainGameScene");  // For now, just return to main game
    }
}
