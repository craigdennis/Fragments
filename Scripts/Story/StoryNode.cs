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
        return choices.Select((choice, index) => 
            (choice, index))
            .Where(pair => !selectedChoiceIndices.Contains(pair.index))
            .ToList();
    }

    // Add this property
    public string currentText { get; private set; }
} 