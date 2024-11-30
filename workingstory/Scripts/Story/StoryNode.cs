using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "New Story Node", menuName = "Story/Story Node")]
public class StoryNode : ScriptableObject
{
    [System.Serializable]
    public class StorySegment
    {
        [TextArea(3, 10)]
        public string text;
        [TextArea(1, 2)]
        public string continueButtonText;
    }

    public List<StorySegment> storySegments = new List<StorySegment>();
    
    [System.Serializable]
    public class Choice
    {
        [TextArea(1, 2)]
        public string buttonText;
        [TextArea(3, 10)]
        public string resultText;
        public StoryNode nextNode;
        public string decisionKey;
        public List<string> requiredDecisions;
    }

    public List<Choice> choices = new List<Choice>();

    // Add a property to track selected choices
    private List<int> selectedChoiceIndices = new List<int>();
    
    // Method to handle choice selection
    public void SelectChoice(int choiceIndex)
    {
        selectedChoiceIndices.Add(choiceIndex);
        if (string.IsNullOrEmpty(currentText))
        {
            currentText = storySegments[0].text;
        }
        currentText = choices[choiceIndex].resultText + "\n\n" + currentText;
    }

    // Method to get available choices (excluding selected ones)
    public List<(Choice choice, int originalIndex)> GetAvailableChoices()
    {
        return choices.Select((choice, index) => (choice, index))
            .Where(pair => IsChoiceAvailable(pair.choice))
            .ToList();
    }

    public bool IsChoiceAvailable(Choice choice)
    {
        Debug.Log($"Checking availability for choice: {choice.buttonText}");
        
        if (choice.requiredDecisions == null || choice.requiredDecisions.Count == 0)
        {
            Debug.Log($"Choice '{choice.buttonText}' has no required decisions");
            return true;
        }
        
        Debug.Log($"Required decisions for '{choice.buttonText}':");
        foreach (var decision in choice.requiredDecisions)
        {
            bool hasDecision = PlayerDecisionManager.Instance.HasMadeDecision(decision);
            Debug.Log($"- Required decision '{decision}': {(hasDecision ? "YES" : "NO")}");
        }
        
        bool isAvailable = choice.requiredDecisions.All(decision => 
            PlayerDecisionManager.Instance.HasMadeDecision(decision));
        
        Debug.Log($"Final availability for '{choice.buttonText}': {isAvailable}");
        return isAvailable;
    }

    // Add this property
    public string currentText { get; private set; }

    // Update the RecordDecision method
    public void RecordDecision(string decisionKey)
    {
        Debug.Log($"Attempting to record decision: {decisionKey}");
        if (!string.IsNullOrEmpty(decisionKey))
        {
            PlayerDecisionManager.Instance.RecordDecision(decisionKey);
            Debug.Log($"Successfully recorded decision: {decisionKey}");
        }
    }

    // Update the HasMadeDecision method
    public static bool HasMadeDecision(string decisionKey)
    {
        return PlayerDecisionManager.Instance.HasMadeDecision(decisionKey);
    }
} 