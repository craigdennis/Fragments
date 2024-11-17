using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{

     public TextMeshProUGUI displayText;
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
        string logAsText = string.Join("\n", actionLog.ToArray());
        displayText.text = logAsText;
    }

    public void DisplayRoomText() 
    {
        ClearCollectionsForNewRoom();
        UnpackRoom();

        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());

        string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;

        // string buttonChoiceText = buttonChoiceText;



        LogStringWithReturn(combinedText);
    }

    void UnpackRoom() 
    {
        roomNavigation.UnpackExitsInRoom();

    }

    void ClearCollectionsForNewRoom() 
    {
        interactionDescriptionsInRoom.Clear();
       roomNavigation.ClearExits();
    }


    public void LogStringWithReturn(string stringToAdd) 
    {
        actionLog.Add(stringToAdd + "\n");
    }

    // public void setUpButtonChoice() 
    // {
    //     buttonChoice.DisplayButtonOptions();
    // }

    // public void buttonChoiceClicked(int choiceIndex) 
    // {
    //     roomNavigation.AttemptToChangeRooms(choiceIndex);
    //     DisplayRoomText();
    //     DisplayLoggedText();
    // }
    
}
