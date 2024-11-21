using UnityEngine;
using System.Collections.Generic;

public enum RoomOptionType
{
    Exit,
    Interaction
}

[System.Serializable]
public class RoomOption
{
    public string keyString;
    [TextArea] public string optionDescription;
    public string buttonChoiceText;
    public Room valueRoom; // Only used if it's an exit
    public RoomOptionType optionType;

    public bool IsValid => optionType == RoomOptionType.Exit ? valueRoom != null && !string.IsNullOrEmpty(buttonChoiceText) : !string.IsNullOrEmpty(buttonChoiceText);
}