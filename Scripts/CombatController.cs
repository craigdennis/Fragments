using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class CombatController : MonoBehaviour
{
    [SerializeField] private CombatEntity playerData;    // Reference to the ScriptableObject
    [SerializeField] private CombatEntity guardData; 
    [SerializeField] private Button attackButton; 
    [SerializeField] private TextMeshProUGUI guardHealthText; 
    [SerializeField] private TextMeshProUGUI playerHealthText; 
    [SerializeField] private TextMeshProUGUI combatResultText;  // Add this for showing the result
    
    private CombatInstance player;    // Runtime instance
    private CombatInstance guard;     // Runtime instance
    private bool isCombatOver = false;

    void Start()
    {
        // Create runtime instances from the ScriptableObject data
        player = new CombatInstance(playerData);
        guard = new CombatInstance(guardData);
        
        player.Initialize();
        guard.Initialize();

        // Add button listener
        attackButton.onClick.AddListener(OnAttackButtonPressed);
        UpdateHealthDisplay();  // Initial display
    }

    // New method for button click
    public void OnAttackButtonPressed()
    {
    

        if (player.attackTimer <= 0f)
        {
            AttackGuard();
            player.attackTimer = player.equippedWeapon.attackSpeed;
        }
    
    }

    void Update()
    {
        // Update timers
        if (player.attackTimer > 0f)
            player.attackTimer -= Time.deltaTime;
            
        HandleGuardAttack();
    }

    void HandleGuardAttack()
    {
        if (guard.attackTimer <= 0f)
        {
            AttackPlayer();
            guard.attackTimer = guard.attackCooldown;
        }

        if (guard.attackTimer > 0f)
            guard.attackTimer -= Time.deltaTime;
    }

    void AttackGuard()
    {
        if (isCombatOver) return;  // Don't allow attacks if combat is over
        
        if (player == null)
        {
            Debug.LogError("Player is null!");
            return;
        }
        
        if (player.equippedWeapon == null)
        {
            Debug.LogError("Player weapon is null!");
            return;
        }
        
        bool isDefeated = guard.TakeDamage(player.equippedWeapon.weaponDamage);
        Debug.Log($"{player.entityName} attacked {guard.entityName} for {player.equippedWeapon.weaponDamage} damage!");
        
        UpdateHealthDisplay();

        if (isDefeated)
        {
            EndCombat($"{guard.entityName} has been defeated!");
        }
    }

    void AttackPlayer()
    {
        if (isCombatOver) return;  // Don't allow attacks if combat is over
        
        bool isDefeated = player.TakeDamage(guard.damage);
        Debug.Log($"{guard.entityName} attacked {player.entityName} for {guard.damage} damage.");
        
        UpdateHealthDisplay();

        if (isDefeated)
        {
            EndCombat($"{player.entityName} has been defeated!");
        }
    }

    void EndCombat(string result)
    {
        isCombatOver = true;
        attackButton.interactable = false;  // Disable attack button
        
        if (combatResultText != null)
        {
            combatResultText.text = result;
            combatResultText.gameObject.SetActive(true);
        }
        
        Debug.Log("Combat is over: " + result);
        
        // You might want to add these depending on your game:
        // StartCoroutine(ReturnToGameAfterDelay());
        // ShowVictoryOrDefeatScreen();
        // etc.
    }

    void UpdateHealthDisplay()
    {
        if (guardHealthText != null && guard != null)
        {
            guardHealthText.text = $"Guard Health: {Mathf.Max(0, guard.currentHealth)}/{guard.maxHealth}";
        }
        
        if (playerHealthText != null && player != null)
        {
            playerHealthText.text = $"Player Health: {Mathf.Max(0, player.currentHealth)}/{player.maxHealth}";
        }
    }
}
