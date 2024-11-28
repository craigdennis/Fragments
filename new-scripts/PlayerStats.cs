using UnityEngine;

public class PlayerStats : Stats
{
    public override void Initialize()
    {
        base.Initialize();
        // Initialize player-specific attributes if any

        // For example, set the player's accuracy to a desired value
        accuracy = 0.9f; // 90% chance to hit
    }
}