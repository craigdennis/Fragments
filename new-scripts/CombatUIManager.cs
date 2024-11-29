using UnityEngine;
using UnityEngine.UI;

public class CombatUIManager : MonoBehaviour
{
    public Text playerHealthText;
    public Text enemyHealthText;
    public Text combatMessageText; // Text element for displaying combat messages

    public slider playeHealthSlider;
    public slider enemyHealthSlider;

    // Reference to the CombatManager if needed
    public CombatManager combatManager;

    // Optional: Reference to the player's attack button
    public Button attackButton;

    void Start()
    {

        if (playerHealthSlider == null || enemyHealthSlider == null)
        {
            Debug.LogError("Health Slider UI elements are not assigned in the inspector.");
        }
        
        // Ensure that the texts are initialized
        if (playerHealthText == null || enemyHealthText == null || combatMessageText == null)
        {
            Debug.LogError("Health Text or Message Text UI elements are not assigned in the inspector.");
        }

        // Ensure the attack button is assigned if used
        if (attackButton == null)
        {
            Debug.LogWarning("Attack Button is not assigned in the inspector.");
        }
    }

    void Update()
    {
        // Update the attack button interactability based on cooldown
        UpdateAttackButton();
    }

    public void UpdatePlayerHealth(float currentHealth, float maxHealth)
    {
        playerHealthText.text = "Player Health: " + currentHealth + " / " + maxHealth;
    }

    public void UpdateEnemyHealth(float currentHealth, float maxHealth)
    {
        enemyHealthText.text = "Enemy Health: " + currentHealth + " / " + maxHealth;
    }

    public void ShowMessage(string message)
    {
        combatMessageText.text = message;
        // Optionally, you can add logic to fade out the message after a few seconds
    }

    // Update the attack button interactability based on the player's cooldown
    public void UpdateAttackButton()
    {
        if (combatManager != null && attackButton != null)
        {
            bool isInteractable = combatManager.playerStats.cooldownTimer <= 0f;
            attackButton.interactable = isInteractable;
        }
    }
}