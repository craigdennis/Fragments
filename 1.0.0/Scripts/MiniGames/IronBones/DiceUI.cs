using UnityEngine;
using UnityEngine.UI;

public class DiceUI : MonoBehaviour
{
    public Image diceImage;
    public Toggle holdToggle;
    public Sprite[] diceSprites; // Sprites for dice faces (1-6)
    public Sprite backFaceSprite; // Sprite for hidden dice
    private Dice dice;
    private bool isAI;

    public void Initialize(Dice dice, bool isAI = false)
    {
        this.dice = dice;
        this.isAI = isAI;
        if (!isAI)
        {
            holdToggle.isOn = false;
            holdToggle.onValueChanged.AddListener(OnHoldToggleChanged);
        }
        else
        {
            holdToggle.gameObject.SetActive(false);
        }
    }

    public void UpdateDice(Dice dice)
    {
        this.dice = dice;
        if (dice.Value > 0)
        {
            if (isAI)
            {
                // Optionally hide AI's dice values
                diceImage.sprite = backFaceSprite;
            }
            else
            {
                diceImage.sprite = diceSprites[dice.Value - 1];
                holdToggle.isOn = dice.IsHeld;
            }
            diceImage.enabled = true;
        }
        else
        {
            diceImage.enabled = false;
        }
    }

    public void HideDice()
    {
        diceImage.enabled = false;
        holdToggle.gameObject.SetActive(false);
    }

    private void OnHoldToggleChanged(bool isOn)
    {
        if (dice != null)
        {
            dice.IsHeld = isOn;
        }
    }
}