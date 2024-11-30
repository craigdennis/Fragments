using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "ScriptableObjects/Armor")]
public class ArmorData : ScriptableObject
{
    public string armorName;
    public float baseArmor;
    public float healthBonus;
    public float defence;

    public float requiredLevel;

    public int purchasePrice;
    public int sellValue;
}
