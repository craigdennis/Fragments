using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RoomNavigation : MonoBehaviour
{

    public Room currentRoom;
    private Room nextRoom;
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


        }
    }
public void AttemptToChangeRooms(int exitIndex)
{
    if (exitIndex < currentRoom.exits.Length)
    {
        nextRoom = currentRoom.exits[exitIndex].valueRoom;
        
        Debug.Log($"Attempting to change to: {nextRoom.name}. HasCombat: {nextRoom.hasCombat}");
        
        if (nextRoom.hasCombat)
        {
            Debug.Log("Starting combat transition...");
            StartCombat(nextRoom.enemyType);
            return; // Prevent further execution
        }

        CompleteRoomChange();

        GameController controller = GetComponent<GameController>();
        if (controller != null)
        {
            controller.DisplayRoomText();
        }
    }
}



private void StartCombat(CombatEntityType enemyType)
{
    Debug.Log($"Initiating combat with enemy type: {enemyType}");
    PlayerPrefs.SetString("NextRoomGUID", AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(nextRoom)));
    PlayerPrefs.SetInt("EnemyType", (int)enemyType);
    SceneManager.LoadScene("CombatScene");
}


    public void CompleteRoomChange()
    {
        if (nextRoom != null)
        {
            currentRoom = nextRoom;
            nextRoom = null;
        }
    }

    public void ClearExitsInRoom() 
    {
        exitDictionary.Clear();
    }

}
