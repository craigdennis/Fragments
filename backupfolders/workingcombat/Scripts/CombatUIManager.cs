using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemyHealthText;
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private TextMeshProUGUI combatResultText;
    [SerializeField] private Button attackButton;
    
    private CombatInstance player;
    private CombatInstance enemy;
    private CombatController combatController;

    private void Awake()
    {
        combatController = GetComponentInParent<CombatController>();
    }

    private void OnEnable()
    {
        GameEvents.OnPlayerHealthChanged += UpdatePlayerHealth;
        GameEvents.OnEnemyHealthChanged += UpdateEnemyHealth;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerHealthChanged -= UpdatePlayerHealth;
        GameEvents.OnEnemyHealthChanged -= UpdateEnemyHealth;
    }

    public void Initialize(CombatInstance player, CombatInstance enemy)
    {
        this.player = player;
        this.enemy = enemy;
        
        attackButton.onClick.RemoveAllListeners();
        attackButton.onClick.AddListener(OnAttackButtonClicked);
        
        UpdatePlayerHealth(player.currentHealth, player.maxHealth);
        UpdateEnemyHealth(enemy.currentHealth, enemy.maxHealth);
        
        // Start with attack button disabled
        SetAttackButtonState(false);
        
        if (combatResultText != null)
        {
            combatResultText.gameObject.SetActive(false);
        }
    }

    public void SetAttackButtonState(bool isInteractable)
    {
        if (attackButton != null)
        {
            attackButton.interactable = isInteractable;
        }
    }

    public void UpdatePlayerHealth(float current, float max)
    {
        if (playerHealthText != null)
        {
            playerHealthText.text = $"Player: {Mathf.Max(0, current)}/{max}";
        }
    }

    public void UpdateEnemyHealth(float current, float max)
    {
        if (enemyHealthText != null && enemy != null)
        {
            enemyHealthText.text = $"{enemy.entityName}: {Mathf.Max(0, current)}/{max}";
        }
    }

    public void ShowCombatResult(CombatResult result)
    {
        if (combatResultText != null)
        {
            combatResultText.gameObject.SetActive(true);
            combatResultText.text = result == CombatResult.Victory ? "Victory!" : "Defeat!";
            SetAttackButtonState(false);
        }
    }

    private void OnAttackButtonClicked()
    {
        if (combatController != null)
        {
            combatController.OnPlayerAttack();
        }
    }

    private void OnDestroy()
    {
        if (attackButton != null)
        {
            attackButton.onClick.RemoveAllListeners();
        }
    }
}