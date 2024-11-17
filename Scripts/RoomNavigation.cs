using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RoomNavigation : MonoBehaviour
{

    public Room currentRoom;
    public Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();

    private GameController controller;


    void Awake()
    {
        controller = GetComponent<GameController>();
    }

    public void UnpackExitsInRoom()
    {
        for (int i = 0; i < currentRoom.exits.Length; i++)
        {
            exitDictionary.Add(currentRoom.exits[i].keyString, currentRoom.exits[i].valueRoom);
            controller.interactionDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);
            controller.buttonChoiceDescriptionsInRoom.Add(currentRoom.exits[i].buttonChoiceText);
        }
    }

    public void AttemptToChangeRooms(int exitIndex)
    {
        if (exitIndex < currentRoom.exits.Length)
        {
            currentRoom = currentRoom.exits[exitIndex].valueRoom;
        }
    }

    public void ClearExitsInRoom() 
    {
        exitDictionary.Clear();
    }

}
