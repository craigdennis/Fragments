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
    private int currentParagraphIndex = 0;

    public int CurrentParagraphIndex => currentParagraphIndex;

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
        currentParagraphIndex = 0;  // Reset paragraph index
        
        if (room.HasDetailedDescription)
        {
            // Show first paragraph
            displayText.text = room.detailedDescription.paragraphs[0].paragraphText;
            
            // If there are more paragraphs, show "Next" button
            if (room.detailedDescription.paragraphs.Length > 1)
            {
                ShowNextParagraphButton(room.detailedDescription.paragraphs[0].nextButtonText);
                return;  // Don't show exits yet
            }
        }
        else
        {
            // Use regular description if no detailed paragraphs
            displayText.text = room.description;
        }

        // Show exit buttons (only for regular description or last paragraph)
        UpdateExitButtons(exits);
    }

    private void ShowNextParagraphButton(string buttonText)
    {
        // Deactivate all buttons first
        for (int i = 0; i < buttonContainers.Length; i++)
        {
            buttonContainers[i].SetActive(false);
        }

        // Show only the "Next" button
        buttonContainers[0].SetActive(true);
        optionButtonTexts[0].text = buttonText;
        choiceButtons[0].interactable = true;
    }

    private void UpdateExitButtons(Exit[] exits)
    {
        // Existing exit button logic
        int numButtons = Mathf.Min(exits.Length, optionButtonTexts.Length);
        
        for (int i = 0; i < buttonContainers.Length; i++)
        {
            buttonContainers[i].SetActive(false);
        }

        for (int i = 0; i < numButtons; i++)
        {
            buttonContainers[i].SetActive(true);
            optionButtonTexts[i].text = exits[i].buttonChoiceText;
            choiceButtons[i].interactable = true;
        }
    }

    public void ShowNextParagraph(Room room)
    {
        currentParagraphIndex++;
        
        // Get the current paragraph
        var paragraph = room.detailedDescription.paragraphs[currentParagraphIndex];
        displayText.text = paragraph.paragraphText;
        
        // If there's another paragraph after this one, show the next button
        if (currentParagraphIndex < room.detailedDescription.paragraphs.Length - 1)
        {
            ShowNextParagraphButton(paragraph.nextButtonText);
        }
        else
        {
            // On last paragraph, show room exits
            UpdateExitButtons(room.exits);
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