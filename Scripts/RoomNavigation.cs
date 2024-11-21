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

        var selectedExit = CurrentRoom.exits[exitIndex];
        
        // Check if this exit has an item to pick up
        if (selectedExit.itemToPickup != null)
        {
            HandleExitItemPickup(selectedExit);
            return;
        }

        // Normal room transition logic
        var targetRoom = selectedExit.valueRoom;
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

    private void HandleExitItemPickup(Exit exit)
    {
        var playerInventory = SceneService.Instance?.PlayerInventory;
        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory not found!");
            return;
        }

        if (playerInventory.AddItem(exit.itemToPickup))
        {
            // Log the pickup
            string pickupMessage = string.Format(
                exit.pickupDescription,
                exit.itemToPickup.itemName
            );
            GameEvents.RaiseLogMessage(pickupMessage);
            
            // Clear the item from the exit
            exit.itemToPickup = null;
            
            // Refresh the room display
            PrepareRoom();
            GameEvents.RaiseRoomChanged(CurrentRoom);
        }
        else
        {
            GameEvents.RaiseLogMessage("Cannot pick up item - inventory is full!");
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
        // Guard against null exits
        if (CurrentRoom == null || CurrentRoom.exits == null) return;
        
        foreach (var exit in CurrentRoom.exits)
        {
            // Skip invalid exits
            if (string.IsNullOrEmpty(exit.keyString) || 
                (exit.valueRoom == null && exit.itemToPickup == null)) continue;
            
            exitDictionary[exit.keyString] = exit.valueRoom;
            
            // Use pickup text if there's an item, otherwise use normal exit description
            string description = exit.itemToPickup != null ? 
                exit.pickupButtonText : 
                exit.exitDescription;
                
            interactionDescriptions.Add(description);
        }
    }

    private void ClearCurrentRoomData()
    {
        exitDictionary.Clear();
        interactionDescriptions.Clear();
    }
}