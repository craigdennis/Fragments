using UnityEngine;

[System.Serializable]
public class Exit
{
    public string keyString;
    [TextArea] public string exitDescription;
    public string buttonChoiceText;
    public Room valueRoom;
    public Item itemToPickup;
    public string pickupButtonText = "Pick up";
    [Tooltip("Use {0} as placeholder for item name")]
    public string pickupDescription = "You picked up {0}";

    public bool IsValid => valueRoom != null && !string.IsNullOrEmpty(buttonChoiceText);
}