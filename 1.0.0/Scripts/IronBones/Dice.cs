using UnityEngine;

public class Dice : MonoBehaviour
{
    public int Value { get; private set; }
    public bool IsHeld { get; set; }

    public void Roll()
    {
        if (!IsHeld)
        {
            Value = Random.Range(1, 7); // Random value between 1 and 6
        }
    }

    public void Reset()
    {
        IsHeld = false;
        Value = 0;
    }
}