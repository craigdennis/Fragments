using UnityEngine;

[System.Serializable]
public class ParagraphData
{
    [TextArea(3, 10)]
    [Tooltip("The text content for this paragraph")]
    public string paragraphText;

    [Tooltip("The text that will appear on the button to proceed")]
    public string buttonText = "Continue...";
}