using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Player humanPlayer;
    public AIPlayer aiPlayer;
    public Text messagePanel;
    public Text humanPlayerScoreText;
    public Text aiPlayerScoreText;
    public Button rollButton;
    public Button endTurnButton;
    public DiceUI[] humanDiceUI;
    public DiceUI[] aiDiceUI;
    public Dropdown difficultyDropdown; // UI element to select difficulty
    private bool isHumanTurn = true;
    private bool gameStarted = false;

    void Start()
    {
        messagePanel.text = "Select Difficulty and Press Start";
    }

    public void StartGame()
    {
        humanPlayer = new Player();
        AIDifficulty selectedDifficulty = (AIDifficulty)difficultyDropdown.value;
        aiPlayer = new AIPlayer(selectedDifficulty);
        gameStarted = true;
        isHumanTurn = true;
        UpdateUI();
        messagePanel.text = "Your Turn";
        rollButton.interactable = true;
        endTurnButton.interactable = true;
    }

    public void RollDice()
    {
        if (!gameStarted) return;

        if (isHumanTurn)
        {
            if (humanPlayer.RollsLeft > 0)
            {
                humanPlayer.RollDice();
                humanPlayer.RollsLeft--;
                UpdateUI();
            }
            else
            {
                messagePanel.text = "No rolls left! End your turn.";
            }
        }
    }

    public void EndTurn()
    {
        if (!gameStarted) return;

        if (isHumanTurn)
        {
            isHumanTurn = false;
            messagePanel.text = "AI's Turn";
            rollButton.interactable = false;
            endTurnButton.interactable = false;
            StartCoroutine(AITurn());
        }
    }

    private IEnumerator AITurn()
    {
        yield return new WaitForSeconds(1f); // Wait before AI starts

        aiPlayer.StartTurn();

        while (aiPlayer.RollsLeft > 0)
        {
            aiPlayer.RollDice();
            UpdateUI();
            yield return new WaitForSeconds(1f); // Wait between rolls
        }

        DetermineRoundWinner();
        isHumanTurn = true;
        ResetPlayers();
        UpdateUI();
        messagePanel.text = "Your Turn";
        rollButton.interactable = true;
        endTurnButton.interactable = true;
    }

    void DetermineRoundWinner()
    {
        if (humanPlayer.TotalScore < aiPlayer.TotalScore)
        {
            humanPlayer.LoseDie();
            messagePanel.text = "You lose a die!";
        }
        else if (aiPlayer.TotalScore < humanPlayer.TotalScore)
        {
            aiPlayer.LoseDie();
            messagePanel.text = "AI loses a die!";
        }
        else
        {
            messagePanel.text = "It's a tie!";
        }
        CheckGameOver();
    }

    void ResetPlayers()
    {
        humanPlayer.ResetDice();
        aiPlayer.ResetDice();
    }

    void CheckGameOver()
    {
        if (humanPlayer.DiceCount == 0)
        {
            messagePanel.text = "AI Wins!";
            rollButton.interactable = false;
            endTurnButton.interactable = false;
            gameStarted = false;
        }
        else if (aiPlayer.DiceCount == 0)
        {
            messagePanel.text = "You Win!";
            rollButton.interactable = false;
            endTurnButton.interactable = false;
            gameStarted = false;
        }
    }

    void UpdateUI()
    {
        humanPlayerScoreText.text = $"Your Score: {humanPlayer.TotalScore} | Dice: {humanPlayer.DiceCount}";
        aiPlayerScoreText.text = $"AI Score: {aiPlayer.TotalScore} | Dice: {aiPlayer.DiceCount}";
        UpdateDiceUI();
    }

    void UpdateDiceUI()
    {
        // Update Human Player Dice UI
        for (int i = 0; i < humanDiceUI.Length; i++)
        {
            if (i < humanPlayer.DiceList.Count)
            {
                humanDiceUI[i].UpdateDice(humanPlayer.DiceList[i]);
            }
            else
            {
                humanDiceUI[i].HideDice();
            }
        }

        // Update AI Dice UI
        for (int i = 0; i < aiDiceUI.Length; i++)
        {
            if (i < aiPlayer.DiceList.Count)
            {
                aiDiceUI[i].UpdateDice(aiPlayer.DiceList[i]);
            }
            else
            {
                aiDiceUI[i].HideDice();
            }
        }
    }
}