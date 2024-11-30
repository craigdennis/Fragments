using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class Potion : Item
{
    [Header("Potion Effects")]
    public List<PotionEffect> effects = new();
    public float useTime;
    public float cooldown;
    
    public bool UsePotion()
    {
        foreach (var effect in effects)
        {
            ApplyEffect(effect);
        }
        return true;
    }
    
    
    private void ApplyEffect(PotionEffect effect)
    {
        // Implementation for applying specific effects
        switch (effect.type)
        {
            case PotionEffectType.Health:
                // Heal player
                break;
            case PotionEffectType.AttackBoost:
                // Boost attack
                break;
            case PotionEffectType.ManaCure:
                // Restore mana
                break;
            case PotionEffectType.PoisonCure:
                // Cure poison
                break;
        }
    }
} 