using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textComponent;
    
    [SerializeField]
    private GameObject choiceButtonPrefab;
    
    [SerializeField]
    private Transform choiceButtonContainer;
    
    [SerializeField]
    private StoryNode startingNode;

    private StoryNode currentNode;
    private int currentSegmentIndex = 0;
    private List<GameObject> currentButtons = new List<GameObject>();
    private List<Button> choiceButtons = new List<Button>();

    private void Start()
    {
        if (startingNode != null)
        {
            DisplayNode(startingNode);
        }
        else
        {
            Debug.LogError("No starting node assigned!");
        }
    }

    public void DisplayNode(StoryNode node, int segmentIndex = 0)
    {
        if (node == null)
        {
            Debug.LogError("Attempted to display null node!");
            return;
        }

        Debug.Log($"Displaying node: {node.name}");
        currentNode = node;
        currentSegmentIndex = segmentIndex;
        DisplayCurrentSegment();
    }

    private void DisplayCurrentSegment()
    {
        ClearChoiceButtons();

        if (currentSegmentIndex < currentNode.storySegments.Count)
        {
            var segment = currentNode.storySegments[currentSegmentIndex];
            textComponent.text = segment.text;

            bool isLastSegment = currentSegmentIndex == currentNode.storySegments.Count - 1;
            bool hasEmptyContinueText = string.IsNullOrEmpty(segment.continueButtonText);

            if (isLastSegment && hasEmptyContinueText && currentNode.choices.Count > 0)
            {
                foreach (var choice in currentNode.choices)
                {
                    if (!currentNode.IsChoiceAvailable(choice))
                    {
                        Debug.Log($"Choice '{choice.buttonText}' is not available due to missing decisions.");
                        continue;
                    }

                    GameObject buttonObj = Instantiate(choiceButtonPrefab, choiceButtonContainer);
                    currentButtons.Add(buttonObj);

                    TextMeshProUGUI buttonTextComponent = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
                    if (buttonTextComponent != null)
                    {
                        buttonTextComponent.text = choice.buttonText;
                    }

                    Button button = buttonObj.GetComponent<Button>();
                    button.onClick.AddListener(() => {
                        Destroy(buttonObj);
                        currentButtons.Remove(buttonObj);
                        
                        if (!string.IsNullOrEmpty(choice.decisionKey))
                        {
                            Debug.Log($"About to record decision from choice: {choice.decisionKey}");
                            currentNode.RecordDecision(choice.decisionKey);
                        }
                        else
                        {
                            Debug.Log($"No decision key for choice: {choice.buttonText}");
                        }
                        
                        if (!string.IsNullOrEmpty(choice.resultText))
                        {
                            textComponent.text = choice.resultText;
                        }
                        
                        if (choice.nextNode != null)
                        {
                            Debug.Log($"Moving to next node for choice: {choice.buttonText}");
                            DisplayNode(choice.nextNode);
                        }
                    });
                }
            }
            else
            {
                CreateButton(segment.continueButtonText, () => {
                    currentSegmentIndex++;
                    DisplayCurrentSegment();
                });
            }
        }
    }

    private GameObject CreateButton(string buttonText, UnityEngine.Events.UnityAction onClick)
    {
        GameObject buttonObj = Instantiate(choiceButtonPrefab, choiceButtonContainer);
        currentButtons.Add(buttonObj);

        TextMeshProUGUI buttonTextComponent = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonTextComponent != null)
        {
            buttonTextComponent.text = buttonText;
        }

        Button button = buttonObj.GetComponent<Button>();
        button.onClick.AddListener(() => {
            Debug.Log($"Button clicked: {buttonText}");
            if (onClick != null)
            {
                onClick.Invoke();
            }
            else
            {
                Debug.LogWarning("onClick action is null!");
            }
        });

        return buttonObj;
    }

    private void ClearChoiceButtons()
    {
        foreach (var button in currentButtons)
        {
            Destroy(button);
        }
        currentButtons.Clear();
    }

    public void DisplayChoices(StoryNode node)
    {
        // Clear existing buttons
        ClearChoiceButtons();
        
        // Only display available (unselected) choices
        var availableChoices = node.GetAvailableChoices();
        foreach (var (choice, originalIndex) in availableChoices)
        {
            CreateChoiceButton(choice, () => {
                // When choice is selected:
                node.SelectChoice(originalIndex);
                UpdateStoryText(node.currentText);
                DisplayChoices(node); // Refresh choices, excluding the selected one
            });
        }
    }

    private void CreateChoiceButton(StoryNode.Choice choice, UnityEngine.Events.UnityAction onClick)
    {
        GameObject buttonObj = Instantiate(choiceButtonPrefab, choiceButtonContainer);
        choiceButtons.Add(buttonObj.GetComponent<Button>());

        TextMeshProUGUI buttonTextComponent = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonTextComponent != null)
        {
            buttonTextComponent.text = choice.buttonText;
        }

        Button button = buttonObj.GetComponent<Button>();
        button.onClick.AddListener(() => {
            Debug.Log($"Button clicked: {choice.buttonText}");
            if (onClick != null)
            {
                onClick.Invoke();
            }
            else
            {
                Debug.LogWarning("onClick action is null!");
            }
        });
    }

    private void UpdateStoryText(string newText)
    {
        textComponent.text = newText;
    }

#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
    // Test helper methods
    public StoryNode GetCurrentNode() => currentNode;
    public int GetCurrentSegmentIndex() => currentSegmentIndex;
    public List<GameObject> GetCurrentButtons() => currentButtons;
#endif
}
