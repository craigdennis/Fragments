using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    [SerializeField] private Room startingRoom;
    public Room CurrentRoom { get; private set; }
    private Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();
    private List<string> interactionDescriptions = new List<string>();

    public Room StartingRoom => startingRoom;

    private void Start()
    {
        if (CurrentRoom == null)
        {
            SetRoom(startingRoom);
        }
    }

    public void SetRoom(Room room)
    {
        CurrentRoom = room;
        GameEvents.RaiseRoomChanged(room);
    }

    public void PrepareRoom()
    {
        ClearCurrentRoomData();
        UnpackExits();
    }

    public void ProcessChoice(int exitIndex)
    {
        if (exitIndex >= CurrentRoom.exits.Length) 
        {
            Debug.LogError($"Invalid exit index: {exitIndex}");
            return;
        }

        var targetRoom = CurrentRoom.exits[exitIndex].valueRoom;
        
        if (targetRoom == null)
        {
            Debug.LogError("Target room is null!");
            return;
        }

        if (targetRoom.hasCombat)
        {
            Debug.Log($"Transitioning to combat with enemy type: {targetRoom.enemyType}");
            SceneService.Instance.TransitionToCombat(new SceneTransitionData
            {
                NextRoom = targetRoom,
                EnemyType = targetRoom.enemyType,
                RoomGUID = targetRoom.name
            });
        }
        else
        {
            SetRoom(targetRoom);
        }
    }

    public string GetCurrentRoomDescription()
    {
        return CurrentRoom.description;
    }

    public List<string> GetInteractionDescriptions()
    {
        return interactionDescriptions;
    }

    public Exit[] GetExitChoices()
    {
        return CurrentRoom.exits;
    }

    private void UnpackExits()
    {
        foreach (var exit in CurrentRoom.exits)
        {
            exitDictionary[exit.keyString] = exit.valueRoom;
            interactionDescriptions.Add(exit.exitDescription);
        }
    }

    private void ClearCurrentRoomData()
    {
        exitDictionary.Clear();
        interactionDescriptions.Clear();
    }
}