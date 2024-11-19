using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private TextMeshProUGUI[] optionButtonTexts;
    [SerializeField] private TextMeshProUGUI lastActionText;
    [SerializeField] private GameObject[] buttonContainers;
    [SerializeField] private Button[] choiceButtons;

    private void Awake()
    {
        // Set up button click handlers
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int index = i; // Capture the index for the lambda
            choiceButtons[i].onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    private void OnButtonClicked(int index)
    {
        GetComponent<GameController>().OnChoiceSelected(index);
    }

    public void UpdateRoomDisplay(Room room, List<string> interactions, Exit[] exits)
    {
        // Update display text with room description
        displayText.text = room.description;

        // Safety check for array lengths
        if (optionButtonTexts.Length != buttonContainers.Length || 
            optionButtonTexts.Length != choiceButtons.Length)
        {
            Debug.LogError("Button arrays must be the same length!");
            return;
        }

        // Update button texts
        int numButtons = Mathf.Min(exits.Length, optionButtonTexts.Length);
        
        // First deactivate all buttons
        for (int i = 0; i < buttonContainers.Length; i++)
        {
            buttonContainers[i].SetActive(false);
        }

        // Then activate and set up only the needed buttons
        for (int i = 0; i < numButtons; i++)
        {
            buttonContainers[i].SetActive(true);
            optionButtonTexts[i].text = exits[i].buttonChoiceText;
            choiceButtons[i].interactable = true;
        }
    }

    public void UpdateLogDisplay(List<string> actionLog)
    {
        displayText.text = string.Join("\n", actionLog);
    }

    public void UpdateLastAction(string action)
    {
        lastActionText.text = action;
    }
}