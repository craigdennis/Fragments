using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Room))]
public class RoomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        Room room = (Room)target;

        // Draw default fields
        EditorGUILayout.PropertyField(serializedObject.FindProperty("description"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("roomName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("exits"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("detailedDescription"));
        
        // Draw the item pickup fields
        EditorGUILayout.PropertyField(serializedObject.FindProperty("itemInRoom"));
        
        if (room.itemInRoom != null)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("pickupButtonText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("pickupDescription"));
        }
        
        // Draw combat fields
        EditorGUILayout.PropertyField(serializedObject.FindProperty("hasCombat"));
        if (room.hasCombat)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("enemyType"));
        }

        serializedObject.ApplyModifiedProperties();
    }
} 