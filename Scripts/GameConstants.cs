using UnityEngine;

public static class GameConstants
{
    // Scene Names
    public const string MAIN_SCENE = "MainGameScene";
    public const string COMBAT_SCENE = "CombatScene";

    // Timing
    public const float COMBAT_END_DELAY = 2f;
    public const float DEFAULT_ATTACK_COOLDOWN = 1.5f;
    
    // PlayerPrefs Keys
    public const string LAST_ROOM_KEY = "LastRoomGUID";
    public const string ENEMY_TYPE_KEY = "EnemyType";
}