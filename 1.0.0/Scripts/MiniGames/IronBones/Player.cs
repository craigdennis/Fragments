using System.Collections.Generic;

public class Player
{
    public int DiceCount { get; set; } = 5;
    public List<Dice> DiceList { get; private set; } = new List<Dice>();
    public int TotalScore { get; private set; }
    public int RollsLeft { get; set; } = 3;

    public Player()
    {
        for (int i = 0; i < DiceCount; i++)
        {
            DiceList.Add(new Dice());
        }
    }

    public void RollDice()
    {
        foreach (Dice dice in DiceList)
        {
            dice.Roll();
        }
        CalculateTotalScore();
    }

    public void CalculateTotalScore()
    {
        TotalScore = 0;
        foreach (Dice dice in DiceList)
        {
            TotalScore += dice.Value;
        }
    }

    public void LoseDie()
    {
        if (DiceList.Count > 0)
        {
            DiceList.RemoveAt(DiceList.Count - 1);
            DiceCount--;
        }
    }

    public void ResetDice()
    {
        foreach (Dice dice in DiceList)
        {
            dice.Reset();
        }
        RollsLeft = 3;
    }
}