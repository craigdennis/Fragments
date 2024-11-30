using UnityEngine;
using System.Collections;

public class CombatController : MonoBehaviour
{
    [SerializeField] private CombatUIManager uiManager;
    [SerializeField] private CombatEntityFactory entityFactory;
    
    private CombatInstance player;
    private CombatInstance enemy;
    private CombatState currentState = CombatState.Active;
    
    private readonly Timer playerAttackTimer = new Timer();
    private readonly Timer enemyAttackTimer = new Timer();

    private void Start()
    {
        if (!ValidateReferences()) return;
        InitializeCombat();
    }

    private bool ValidateReferences()
    {
        if (uiManager == null)
        {
            Debug.LogError("CombatUIManager reference is missing on CombatController!");
            return false;
        }

        if (entityFactory == null)
        {
            Debug.LogError("CombatEntityFactory reference is missing on CombatController!");
            return false;
        }

        return true;
    }

    private void InitializeCombat()
    {
        if (SceneService.Instance == null)
        {
            Debug.LogError("SceneService.Instance is null!");
            return;
        }

        var transitionData = SceneService.Instance.GetTransitionData();
        if (transitionData == null)
        {
            Debug.LogError("No transition data found!");
            return;
        }

        player = entityFactory.CreatePlayer();
        enemy = entityFactory.CreateEnemy(transitionData.EnemyType);

        if (player == null || enemy == null)
        {
            Debug.LogError("Failed to create combat instances!");
            return;
        }

        uiManager.Initialize(player, enemy);
    }

    public void OnPlayerAttack()
    {
        if (currentState != CombatState.Active || player == null || enemy == null) return;
        if (!playerAttackTimer.IsReady) return;

        Debug.Log($"Player attacking for {player.damage} damage");
        bool enemyDied = enemy.TakeDamage(player.damage);
        playerAttackTimer.Start(player.attackCooldown);
        
        // Disable attack button when attack starts
        uiManager.SetAttackButtonState(false);

        if (enemyDied)
        {
            Debug.Log("Enemy died!");
            EndCombat(CombatResult.Victory);
        }
    }

    private void Update()
    {
        if (currentState != CombatState.Active || player == null || enemy == null) return;

        // Update timers
        playerAttackTimer.Update(Time.deltaTime);
        enemyAttackTimer.Update(Time.deltaTime);

        // Update attack button state
        uiManager.SetAttackButtonState(playerAttackTimer.IsReady);

        if (enemyAttackTimer.IsReady)
        {
            PerformEnemyAttack();
        }
    }

    private void PerformEnemyAttack()
    {
        if (enemy == null || player == null) return;

        Debug.Log($"Enemy attacking for {enemy.damage} damage");
        bool playerDied = player.TakeDamage(enemy.damage);
        enemyAttackTimer.Start(enemy.attackCooldown);
        
        if (playerDied)
        {
            Debug.Log("Player died!");
            EndCombat(CombatResult.Defeat);
        }
    }

    private void OnDestroy()
    {
        // Remove this as we're handling it in OnDisable
        // GameEvents.OnHealthChanged -= uiManager.UpdateHealthDisplay;
    }

    private void EndCombat(CombatResult result)
    {
        currentState = CombatState.Ended;
        uiManager.ShowCombatResult(result);
        StartCoroutine(TransitionAfterDelay(GameConstants.COMBAT_END_DELAY));
    }

    private IEnumerator TransitionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneService.Instance.ReturnToExploration(SceneService.Instance.GetTransitionData().NextRoom);
    }
}