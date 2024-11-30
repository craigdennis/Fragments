using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour

{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI equippedText;
    [SerializeField] private Button itemButton;
    
    private Item item;
    
    public event System.Action<Item> OnItemClicked;
    
    private void Awake()
    {
        itemButton.onClick.AddListener(HandleClick);
    }
    
    public void SetItem(Item item)
    {
        this.item = item;
        itemNameText.text = item.itemName;
        UpdateEquippedStatus();
    }
    
    private void UpdateEquippedStatus()
    {
        equippedText.gameObject.SetActive(item.isEquipped);
        equippedText.text = "[E]";
    }
    
    private void HandleClick()
    {
        OnItemClicked?.Invoke(item);
    }
    
    private void OnDestroy()
    {
        itemButton.onClick.RemoveListener(HandleClick);
    }
}