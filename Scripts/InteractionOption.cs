using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class InteractionOption
{
    public string description;
    public string buttonText;
    public string resultText; // Text to display after interaction
    public bool isOneTimeUse; // If true, remove after use
} 