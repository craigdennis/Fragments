using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<Room> OnRoomChanged;
    public static event Action<string> OnLogMessage;
    public static event Action<CombatResult> OnCombatEnded;
    public static event Action<float, float> OnPlayerHealthChanged;
    public static event Action<float, float> OnEnemyHealthChanged;

    public static void RaiseRoomChanged(Room room) => OnRoomChanged?.Invoke(room);
    public static void RaiseLogMessage(string message) => OnLogMessage?.Invoke(message);
    public static void RaiseCombatEnded(CombatResult result) => OnCombatEnded?.Invoke(result);
    public static void RaisePlayerHealthChanged(float current, float max) => OnPlayerHealthChanged?.Invoke(current, max);
    public static void RaiseEnemyHealthChanged(float current, float max) => OnEnemyHealthChanged?.Invoke(current, max);
}