using UnityEngine;


[System.Serializable]
public class SceneTransitionData
{
    public string RoomGUID { get; set; }
    public CombatEntityType EnemyType { get; set; }
    public Room NextRoom { get; set; }
}