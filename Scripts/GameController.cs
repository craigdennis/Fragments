using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private RoomNavigation roomNavigation;
    
    private List<string> actionLog = new List<string>();

    private void Awake()
    {
        if (uiManager == null) uiManager = GetComponent<UIManager>();
        if (roomNavigation == null) roomNavigation = GetComponent<RoomNavigation>();
        
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
}