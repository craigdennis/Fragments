using UnityEngine;
using TMPro;
using System.Collections.Generic;  // This is required for List<>
using System.Linq;         

public class InventoryUIManager : MonoBehaviour
{
    [Header("Content Reference")]
    [SerializeField] private Transform contentArea;

    [Header("UI References")]
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemStatsText;
    [SerializeField] private TMP_Text itemDescriptionText;
    [SerializeField] private GameObject itemDetailsPanel;
    [SerializeField] private GameObject itemUIPrefab;

    // Add these field declarations
    [Header("References")]
    [SerializeField] private PlayerInventory playerInventory;  // Add this line
    private Item selectedItem;

 private void Start()
    {
        if (playerInventory == null)
        {
            playerInventory = GetComponent<PlayerInventory>();
      
        }
        
        playerInventory.OnInventoryChanged += RefreshUI;

        
        ShowCategory("weapons"); // Show default category
    }

   public void ShowCategory(string category)
{
    if (string.IsNullOrEmpty(category))
    {

        return;
    }

    ClearList();
    
    switch (category.ToLower())
    {
        case "weapons":
            PopulateList(playerInventory.GetWeapons());
            break;
        case "armor":
            PopulateList(playerInventory.GetArmor());
            break;
        case "potions":
            PopulateList(playerInventory.GetPotions());
            break;
        case "food":
            PopulateList(playerInventory.GetFood());
            break;
        case "keyitems":
            PopulateList(playerInventory.GetKeyItems());
            break;
        default:
            break;
    }
}

  private void PopulateList<T>(List<T> items) where T : Item
    {
        foreach (var item in items)
        {
            var itemUIObject = Instantiate(itemUIPrefab, contentArea);
            var itemUI = itemUIObject.GetComponent<InventoryItemUI>();
            itemUI.SetItem(item);
            itemUI.OnItemClicked += HandleItemClick;
        }
    }

 private void ClearList()
    {
        foreach (Transform child in contentArea)
        {
            Destroy(child.gameObject);
        }
    }


 private void HandleItemClick(Item item)
    {
        selectedItem = item;
        DisplayItemDetails(item);

        if (item is Weapon weapon)
        {
            if (weapon.isEquipped)
            {
           
                playerInventory.UnequipWeapon();
            }
            else
            {
                
                playerInventory.EquipWeapon(weapon);
            }
        }
        else if (item is Armor armor)
        {
            if (armor.isEquipped)
            {
                
                playerInventory.UnequipArmor(armor.slot);
            }
            else
            {
        
                playerInventory.EquipArmor(armor);
            }
        }
    }


      private void DisplayItemDetails(Item item)
    {

        itemDetailsPanel.SetActive(true);
        itemNameText.text = item.itemName;
        itemDescriptionText.text = item.description;
        itemStatsText.text = GetItemStats(item);
    }

    private string GetItemStats(Item item)
    {
        switch (item)
        {
            case Weapon weapon:
                return $"Damage: {weapon.weaponDamage}\n" +
                       $"Attack Speed: {weapon.attackSpeed}\n" +
                       $"Condition: {weapon.conditionType}\n" +
                       GetConditionStats(weapon);

            case Armor armor:
                return $"Defense: {armor.baseDefense}\n" +
                       $"Slot: {armor.slot}\n" +
                       GetResistanceStats(armor);

            case Potion potion:
                return GetPotionStats(potion);

            default:
                return "";
        }
    }

    private string GetConditionStats(Weapon weapon)
    {
        if (weapon.conditionType == ConditionType.None)
            return "";

        return weapon.conditionType switch
        {
            ConditionType.Cold => $"Cold Effect: +{weapon.cooldownIncrease}s cooldown",
            ConditionType.Shock => $"Shock Effect: {weapon.freezeDuration}s freeze",
            ConditionType.Poison => $"Poison Damage: {weapon.poisonDamage} per attack",
            ConditionType.Blindness => $"Accuracy Reduction: {weapon.accuracyReduction}%",
            _ => ""
        };
    }

    private string GetResistanceStats(Armor armor)
    {
        string stats = "";
        foreach (ResistanceType type in System.Enum.GetValues(typeof(ResistanceType)))
        {
            float resistance = armor.GetResistance(type);
            if (resistance > 0)
                stats += $"{type} Resistance: {resistance}%\n";
        }
        return stats;
    }

    private string GetPotionStats(Potion potion)
    {
        string stats = "";
        foreach (var effect in potion.effects)
        {
            stats += $"{effect.type}: {effect.value}";
            if (effect.duration > 0)
                stats += $" for {effect.duration}s";
            stats += "\n";
        }
        return stats;
    }
    private void OnDestroy()
    {
        if (playerInventory != null)
        {
            playerInventory.OnInventoryChanged -= RefreshUI;
    
        }
    }

     private void RefreshUI()
    {
    
        string currentCategory = "weapons"; // You might want to track the current category
        ShowCategory(currentCategory);
    }
}