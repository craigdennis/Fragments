using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public class StoryNodeValidator : EditorWindow
{
    [MenuItem("Tools/Story/Validate Story Nodes")]
    public static void ValidateAllNodes()
    {
        string[] guids = AssetDatabase.FindAssets("t:StoryNode");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            StoryNode node = AssetDatabase.LoadAssetAtPath<StoryNode>(path);
            
            Debug.Log($"Checking node: {node.name}");
            
            foreach (var choice in node.choices)
            {
                if (choice.nextNode == null)
                {
                    Debug.LogError($"Node '{node.name}' has choice '{choice.buttonText}' with no next node assigned!");
                }
                else
                {
                    Debug.Log($"Node '{node.name}' -> Choice '{choice.buttonText}' -> Next Node: '{choice.nextNode.name}'");
                }
            }
        }
    }
}
#endif 