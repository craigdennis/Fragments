using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "ScriptableObjects/Armor")]
public class Weapon : ScriptableObject
{

  public string armorName;
  public float baseArmor;
  public float healthBonus;

  public float requiredLevel;

  public int purchasePrice;
  public int sellValue;

  }