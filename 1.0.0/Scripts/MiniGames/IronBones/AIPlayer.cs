using System.Collections.Generic;
using UnityEngine;

public enum AIDifficulty
{
    Easy,
    Medium,
    Hard,
    ExtraHard,
    Godlike
}

public class AIPlayer : Player
{
    private AIDifficulty difficulty;

    public AIPlayer(AIDifficulty difficultyLevel)
    {
        this.difficulty = difficultyLevel;
    }

    public void StartTurn()
    {
        RollsLeft = 3;
    }

    public void RollDice()
    {
        DecideWhichDiceToHold();
        base.RollDice();
        RollsLeft--;
    }

    private void DecideWhichDiceToHold()
    {
        switch (difficulty)
        {
            case AIDifficulty.Easy:
                EasyStrategy();
                break;
            case AIDifficulty.Medium:
                MediumStrategy();
                break;
            case AIDifficulty.Hard:
                HardStrategy();
                break;
            case AIDifficulty.ExtraHard:
                ExtraHardStrategy();
                break;
            case AIDifficulty.Godlike:
                GodlikeStrategy();
                break;
        }
    }

    private void EasyStrategy()
    {
        foreach (Dice dice in DiceList)
        {
            if (dice.Value >= 5)
            {
                dice.IsHeld = true;
            }
            else
            {
                dice.IsHeld = Random.value < 0.2f; // 20% chance to hold lower dice
            }
        }
    }

    private void MediumStrategy()
    {
        // Holds dice with values of 4 or higher
        foreach (Dice dice in DiceList)
        {
            dice.IsHeld = dice.Value >= 4;
        }
    }

    private void HardStrategy()
    {
        // Holds dice with values of 3 or higher
        foreach (Dice dice in DiceList)
        {
            dice.IsHeld = dice.Value >= 3;
        }
    }

    private void ExtraHardStrategy()
    {
        // Uses expected value calculations
        foreach (Dice dice in DiceList)
        {
            float expectedValue = ExpectedValueIfRerolled(dice.Value);
            if (expectedValue > dice.Value)
            {
                dice.IsHeld = false;
            }
            else
            {
                dice.IsHeld = true;
            }
        }
    }

    private void GodlikeStrategy()
    {
        // Uses risk assessment with expected value
        float riskTolerance = 1.0f; // Maximum risk tolerance
        foreach (Dice dice in DiceList)
        {
            float expectedValue = ExpectedValueIfRerolled(dice.Value);
            if ((expectedValue * riskTolerance) > dice.Value)
            {
                dice.IsHeld = false;
            }
            else
            {
                dice.IsHeld = true;
            }
        }
    }

    private float ExpectedValueIfRerolled(int currentValue)
    {
        // Expected value of rerolling the die
        int sides = 6;
        int sum = 0;
        int count = 0;

        for (int i = 1; i <= sides; i++)
        {
            if (i > currentValue)
            {
                sum += i;
                count++;
            }
        }

        if (count == 0)
            return currentValue; // No higher value possible

        float expectedValue = (float)sum / count;
        return expectedValue;
    }
}