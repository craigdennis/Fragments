using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{

     public TextMeshProUGUI displayText;

     [SerializeField] private TMP_Text[] optionButtonTexts; // Array of your 3 button texts

     public TextMeshProUGUI lastActionText;

    
    [HideInInspector] public RoomNavigation roomNavigation;
    [HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string>();
    List<string> actionLog = new List<string>();


    void Awake()
    {
        roomNavigation = GetComponent<RoomNavigation>();
    }

    void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
    }

    public void DisplayLoggedText() 
{
    // Instead of showing all logged text, just show the most recent entry
    if (actionLog.Count > 0)
    {
        displayText.text = actionLog[actionLog.Count - 1];
    }
}

    public void DisplayRoomText() 
    {
        ClearCollectionsForNewRoom();
        UnpackRoom();

        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());

       string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;


    // Get the minimum between number of exits and number of buttons
    int numButtons = Mathf.Min(roomNavigation.currentRoom.exits.Length, optionButtonTexts.Length);
    
    // Only loop through available buttons/exits
    for (int i = 0; i < numButtons; i++)
    {
        string buttonText = roomNavigation.currentRoom.exits[i].buttonChoiceText;
        optionButtonTexts[i].text = buttonText;
    }

    // Optional: Hide unused buttons
    for (int i = numButtons; i < optionButtonTexts.Length; i++)
    {
            optionButtonTexts[i].transform.parent.gameObject.SetActive(false);
        }
        
       
        LogStringWithReturn(combinedText);
    }

    void UnpackRoom() 
    {
        roomNavigation.UnpackExitsInRoom();

    }

    void ClearCollectionsForNewRoom() 
    {
    interactionDescriptionsInRoom.Clear();
       roomNavigation.ClearExitsInRoom();
    }


    public void LogStringWithReturn(string stringToAdd) 
{
    // Clear the previous log entries and only keep the current one
    actionLog.Clear();
    actionLog.Add(stringToAdd + "\n");
}
// public void buttonChoiceClicked(int choiceIndex) 
// {
//     roomNavigation.AttemptToChangeRooms(choiceIndex);
//     lastActionText.text = "";
//     DisplayRoomText();
//     DisplayLoggedText();
//     lastActionText.text = displayText.text;
// }

public void buttonChoiceClicked(int choiceIndex) 
{
    // Store the current room description before changing rooms
    lastActionText.text = displayText.text;
    
    // Change rooms and update display
    roomNavigation.AttemptToChangeRooms(choiceIndex);
    DisplayRoomText();
    DisplayLoggedText();
}
    
}



