using UnityEngine;

[System.Serializable]
public class RoomParagraph
{
    [TextArea] 
    public string paragraphText;
    public string nextButtonText;  // Optional, null/empty for last paragraph
}

[System.Serializable]
public class RoomParagraphs
{
    public RoomParagraph[] paragraphs;
}