using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public float attackSpeed; // Time in seconds between attacks
    public int weaponDamage;
    public AudioClip weaponSound;
}
