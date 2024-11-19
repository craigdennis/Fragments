using UnityEngine;

[System.Serializable]
public class Exit
{
    public string keyString;
    [TextArea] public string exitDescription;
    public string buttonChoiceText;
    public Room valueRoom;

    public bool IsValid => valueRoom != null && !string.IsNullOrEmpty(buttonChoiceText);
}