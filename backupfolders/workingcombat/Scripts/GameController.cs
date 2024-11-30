using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private RoomNavigation roomNavigation;
    private PlayerInventory playerInventory;
    
    private List<string> actionLog = new List<string>();

    private void Awake()
    {
        if (uiManager == null) uiManager = GetComponent<UIManager>();
        if (roomNavigation == null) roomNavigation = GetComponent<RoomNavigation>();
        
        // Get PlayerInventory from SceneService
        if (SceneService.Instance != null)
        {
            playerInventory = SceneService.Instance.PlayerInventory;
        }
        else
        {
            Debug.LogError("SceneService not found! Make sure it exists in the scene.");
            return;
        }
        
        // Subscribe to events
        GameEvents.OnLogMessage += AddToActionLog;
        GameEvents.OnRoomChanged += HandleRoomChanged;
    }

    private void Start()
    {
        if (SceneService.Instance == null)
        {
            Debug.LogError("SceneService not found in scene! Please add SceneService GameObject.");
            return;
        }

        var transitionData = SceneService.Instance.GetTransitionData();
        if (transitionData != null)
        {
            roomNavigation.SetRoom(transitionData.NextRoom);
            SceneService.Instance.ClearTransitionData();
        }
        else
        {
            roomNavigation.SetRoom(roomNavigation.StartingRoom);
        }
        
        DisplayCurrentRoom();
    }

    private void OnDestroy()
    {
        GameEvents.OnLogMessage -= AddToActionLog;
        GameEvents.OnRoomChanged -= HandleRoomChanged;
    }

    private void AddToActionLog(string message)
    {
        actionLog.Add(message + "\n");
        uiManager.UpdateLogDisplay(actionLog);
    }

    private void HandleRoomChanged(Room newRoom)
    {
        DisplayCurrentRoom();
    }

    private void DisplayCurrentRoom()
    {
        if (SceneService.Instance.IsTransitionPending) return;

        roomNavigation.PrepareRoom();
        string roomDescription = roomNavigation.CurrentRoom.description;
        GameEvents.RaiseLogMessage(roomDescription);
        
        uiManager.UpdateRoomDisplay(
            roomNavigation.CurrentRoom,
            roomNavigation.GetInteractionDescriptions(),
            roomNavigation.CurrentRoom.exits
        );
    }

    public void OnChoiceSelected(int choiceIndex)
    {
        var currentRoom = roomNavigation.CurrentRoom;
        
        // Add null check for currentRoom
        if (currentRoom == null)
        {
            Debug.LogError("Current room is null in OnChoiceSelected!");
            return;
        }
        
        // Check if there's an item to pick up
        if (currentRoom.HasPickableItem)
        {
            Debug.Log($"Attempting to pick up item: {currentRoom.itemInRoom?.itemName ?? "null"}");
            HandleItemPickup(currentRoom);
            return;
        }
        
        // Check if we're in a multi-paragraph room and not on the last paragraph
        if (currentRoom.HasDetailedDescription)
        {
            var paragraphs = currentRoom.detailedDescription.paragraphs;
            int currentIndex = uiManager.CurrentParagraphIndex;
            
            if (currentIndex < paragraphs.Length - 1)
            {
                // Show next paragraph
                uiManager.ShowNextParagraph(currentRoom);
                return;
            }
        }

        // If we're here, it's a regular exit choice
        roomNavigation.ProcessChoice(choiceIndex);
    }

    private void HandleItemPickup(Room room)
    {
        // Check if room is null
        if (room == null)
        {
            Debug.LogError("Room is null in HandleItemPickup!");
            return;
        }

        // Check if playerInventory is null
        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory is null! Make sure it's assigned in the Inspector.");
            return;
        }

        // Check if item is null
        if (room.itemInRoom == null)
        {
            Debug.LogError("Item in room is null!");
            return;
        }

        // Try to add the item
        if (playerInventory.AddItem(room.itemInRoom))
        {
            try
            {
                // Log the pickup with null checks
                string itemName = room.itemInRoom.itemName ?? "Unknown Item";
                string pickupMessage = string.Format(
                    room.pickupDescription ?? "Picked up {0}", 
                    itemName
                );
                GameEvents.RaiseLogMessage(pickupMessage);
                
                // Clear the item from the room
                room.itemInRoom = null;
                
                // Update the display
                DisplayCurrentRoom();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error during item pickup: {e.Message}");
            }
        }
        else
        {
            GameEvents.RaiseLogMessage("Cannot pick up item - inventory is full!");
        }
    }
}