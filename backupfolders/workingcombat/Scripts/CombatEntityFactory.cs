using System;
using UnityEngine;

public class CombatEntityFactory : MonoBehaviour
{
    [SerializeField] private CombatEntity playerData;
    [SerializeField] private CombatEntity guardData;
    [SerializeField] private CombatEntity prisonerData;
    [SerializeField] private CombatEntity banditData;

    public CombatInstance CreatePlayer()
    {
        var instance = new CombatInstance(playerData, isPlayer: true);
        instance.Initialize();
        return instance;
    }

    public CombatInstance CreateEnemy(CombatEntityType type)
    {
        var data = GetEnemyData(type);
        var instance = new CombatInstance(data, isPlayer: false);
        instance.Initialize();
        return instance;
    }

    private CombatEntity GetEnemyData(CombatEntityType type)
    {
        return type switch
        {
            CombatEntityType.Guard => guardData,
            CombatEntityType.Prisoner => prisonerData,
            CombatEntityType.Bandit => banditData,
            _ => throw new ArgumentException($"Unknown enemy type: {type}")
        };
    }
}