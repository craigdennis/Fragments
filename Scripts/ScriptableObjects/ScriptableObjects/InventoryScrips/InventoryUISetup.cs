using UnityEngine;
using UnityEngine.UI;

public class InventoryUISetup : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private RectTransform itemDetailsPanel;
    [SerializeField] private RectTransform tabsPanel;
    [SerializeField] private RectTransform categoryPanel;
    
    [Header("Tab Buttons")]
    [SerializeField] private Button[] tabButtons;

private void Start()
{
    SetupLayout();
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(InventoryUISetup))]
public class InventoryUISetupEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        if (GUILayout.Button("Setup Layout"))
        {
            ((InventoryUISetup)target).SetupLayout();
        }
    }
}
#endif

    
    private void SetupLayout()
    {
        // Details Panel (Top 30%)
        SetupRectTransform(itemDetailsPanel, new Vector2(0, 0.7f), Vector2.one, Vector2.zero);
        
        // Tabs Panel (Middle 10%)
        SetupRectTransform(tabsPanel, new Vector2(0, 0.6f), new Vector2(1, 0.7f), Vector2.zero);
        
        // Category Panel (Bottom 60%)
        SetupRectTransform(categoryPanel, Vector2.zero, new Vector2(1, 0.6f), Vector2.zero);
        
        // Setup tab buttons
        float tabWidth = 1f / tabButtons.Length;
        for (int i = 0; i < tabButtons.Length; i++)
        {
            RectTransform tabRect = tabButtons[i].GetComponent<RectTransform>();
            float startX = i * tabWidth;
            SetupRectTransform(tabRect, 
                new Vector2(startX, 0), 
                new Vector2(startX + tabWidth, 1), 
                new Vector2(0.5f, 0.5f));
        }
    }
    
    private void SetupRectTransform(RectTransform rect, Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot)
    {
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.pivot = pivot;
        rect.anchoredPosition = Vector2.zero;
        rect.sizeDelta = Vector2.zero;
    }
}