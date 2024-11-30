using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CombatUIManager : MonoBehaviour
{
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI enemyHealthText;
    public TextMeshProUGUI combatMessageText; // Text element for displaying combat messages

    public Slider playerHealthSlider;
    public Slider enemyHealthSlider;

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

        // Initialize the sliders with proper max values
        if (playerHealthSlider != null)
        {
            playerHealthSlider.minValue = 0f;
            playerHealthSlider.maxValue = 1f;
        }
        
        if (enemyHealthSlider != null)
        {
            enemyHealthSlider.minValue = 0f;
            enemyHealthSlider.maxValue = 1f;
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
        // Update the slider value (0 to 1 range)
        playerHealthSlider.value = currentHealth / maxHealth;
    }

    public void UpdateEnemyHealth(float currentHealth, float maxHealth)
    {
        enemyHealthText.text = "Enemy Health: " + currentHealth + " / " + maxHealth;
        // Update the slider value (0 to 1 range)
        enemyHealthSlider.value = currentHealth / maxHealth;
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