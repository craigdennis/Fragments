using UnityEngine;

public class PlayerStats : BaseStats
{
    // Add any player-specific attributes or methods here

    public override void Initialize()
    {
        base.Initialize();

        accuracy = 0.8f;
        // Initialize player-specific attributes if any
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        // Additional logic for player when taking damage
    }
}
