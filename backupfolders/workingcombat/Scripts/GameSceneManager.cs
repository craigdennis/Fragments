using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameSceneManager : MonoBehaviour
{
   void Start()
{
    if (PlayerPrefs.HasKey("LastRoomGUID"))
    {
        string roomGUID = PlayerPrefs.GetString("LastRoomGUID");
        string roomPath = AssetDatabase.GUIDToAssetPath(roomGUID);
        Room lastRoom = AssetDatabase.LoadAssetAtPath<Room>(roomPath);

        Debug.Log($"Restoring last room: {lastRoom?.name}");
        if (lastRoom != null)
        {
            GetComponent<RoomNavigation>().SetRoom(lastRoom);
        }

        PlayerPrefs.DeleteKey("LastRoomGUID");
    }
}

}