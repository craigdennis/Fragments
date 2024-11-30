using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string itemName;
    public string description;
    public string flavorText;
    public Sprite icon;
    public bool isEquipped;
    public int goldValue;
    public bool isConsumable;
    public float weight;
    public bool isStackable;
    public int maxStackSize = 1;
    public int currentStackSize = 1;
}