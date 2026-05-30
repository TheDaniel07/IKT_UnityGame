using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private Dictionary<string, InventoryItemData> _items = new();

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitItems();
    }

    private void InitItems()
    {
        var all = new List<InventoryItemData>
        {
            new InventoryItemData { itemId = "coal_ore",          displayName = "Coal Ore",          quantity = 0, category = "Ore" },
            new InventoryItemData { itemId = "coal_smelted",      displayName = "Coal Smelted",      quantity = 0, category = "Smelted" },
            new InventoryItemData { itemId = "copper_ore",        displayName = "Copper Ore",        quantity = 0, category = "Ore" },
            new InventoryItemData { itemId = "copper_smelted",    displayName = "Copper Smelted",    quantity = 0, category = "Smelted" },
            new InventoryItemData { itemId = "iron_ore",          displayName = "Iron Ore",          quantity = 0, category = "Ore" },
            new InventoryItemData { itemId = "iron_smelted",      displayName = "Iron Smelted",      quantity = 0, category = "Smelted" },
            new InventoryItemData { itemId = "diamond_ore",       displayName = "Diamond Ore",       quantity = 0, category = "Ore" },
            new InventoryItemData { itemId = "diamond_smelted",   displayName = "Diamond Smelted",   quantity = 0, category = "Smelted" },
            new InventoryItemData { itemId = "gold_ore",          displayName = "Gold Ore",          quantity = 0, category = "Ore" },
            new InventoryItemData { itemId = "gold_smelted",      displayName = "Gold Smelted",      quantity = 0, category = "Smelted" },
            new InventoryItemData { itemId = "titanium_ore",      displayName = "Titanium Ore",      quantity = 0, category = "Ore" },
            new InventoryItemData { itemId = "titanium_smelted",  displayName = "Titanium Smelted",  quantity = 0, category = "Smelted" },
            new InventoryItemData { itemId = "ruby_ore",          displayName = "Ruby Ore",          quantity = 0, category = "Ore" },
            new InventoryItemData { itemId = "ruby_smelted",      displayName = "Ruby Smelted",      quantity = 0, category = "Smelted" },
            new InventoryItemData { itemId = "uranium_ore",       displayName = "Uranium Ore",       quantity = 0, category = "Ore" },
            new InventoryItemData { itemId = "uranium_smelted",   displayName = "Uranium Smelted",   quantity = 0, category = "Smelted" },
            new InventoryItemData { itemId = "wood_log",          displayName = "Wood Log",          quantity = 0, category = "Wood" },
            new InventoryItemData { itemId = "wood_plank",        displayName = "Wood Plank",        quantity = 0, category = "Wood" },
            new InventoryItemData { itemId = "wood_stick",        displayName = "Wood Stick",        quantity = 0, category = "Wood" },
            new InventoryItemData { itemId = "stone",             displayName = "Stone",             quantity = 0, category = "Material" },
        };
        foreach (var item in all)
            _items[item.itemId] = item;
    }

    public int GetQuantity(string itemId)
        => _items.TryGetValue(itemId, out var d) ? d.quantity : 0;

    public void AddItem(string itemId, int amount)
    {
        if (_items.TryGetValue(itemId, out var d))
            d.quantity += amount;
        else
            Debug.LogWarning($"[InventoryManager] Item not found: {itemId}");
    }

    public void AddItem(Item item, int amount)
    {
        string id = item.itemName.ToLower().Replace(" ", "_");
        if (_items.TryGetValue(id, out var d))
        {
            d.quantity += amount;
            if (d.icon == null)
                d.icon = item.GetSprite();
        }
        else
            Debug.LogWarning($"[InventoryManager] Item not found: {id}");
    }

    public bool RemoveItem(string itemId, int amount)
    {
        if (_items.TryGetValue(itemId, out var d) && d.quantity >= amount)
        {
            d.quantity -= amount;
            return true;
        }
        Debug.LogWarning($"[InventoryManager] Cannot remove {amount}x {itemId}");
        return false;
    }

    public List<InventoryItemData> GetAllItems() => new(_items.Values);
}