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
            // Ores
            new InventoryItemData { itemId = "coal_ore",          displayName = "Coal Ore",          quantity = 0, category = "Ore" },
            new InventoryItemData { itemId = "copper_ore",        displayName = "Copper Ore",        quantity = 0, category = "Ore" },
            new InventoryItemData { itemId = "iron_ore",          displayName = "Iron Ore",          quantity = 0, category = "Ore" },
            new InventoryItemData { itemId = "diamond_ore",       displayName = "Diamond Ore",       quantity = 0, category = "Ore" },
            new InventoryItemData { itemId = "gold_ore",          displayName = "Gold Ore",          quantity = 0, category = "Ore" },
            new InventoryItemData { itemId = "titanium_ore",      displayName = "Titanium Ore",      quantity = 0, category = "Ore" },
            new InventoryItemData { itemId = "ruby_ore",          displayName = "Ruby Ore",          quantity = 0, category = "Ore" },
            new InventoryItemData { itemId = "uranium_ore",       displayName = "Uranium Ore",       quantity = 0, category = "Ore" },
            // Smelted
            new InventoryItemData { itemId = "coal_smelted",      displayName = "Coal Smelted",      quantity = 0, category = "Smelted" },
            new InventoryItemData { itemId = "copper_smelted",    displayName = "Copper Smelted",    quantity = 0, category = "Smelted" },
            new InventoryItemData { itemId = "iron_smelted",      displayName = "Iron Smelted",      quantity = 0, category = "Smelted" },
            new InventoryItemData { itemId = "diamond_smelted",   displayName = "Diamond Smelted",   quantity = 0, category = "Smelted" },
            new InventoryItemData { itemId = "gold_smelted",      displayName = "Gold Smelted",      quantity = 0, category = "Smelted" },
            new InventoryItemData { itemId = "titanium_smelted",  displayName = "Titanium Smelted",  quantity = 0, category = "Smelted" },
            new InventoryItemData { itemId = "ruby_smelted",      displayName = "Ruby Smelted",      quantity = 0, category = "Smelted" },
            new InventoryItemData { itemId = "uranium_smelted",   displayName = "Uranium Smelted",   quantity = 0, category = "Smelted" },
            // Wood
            new InventoryItemData { itemId = "wood_log",          displayName = "Wood Log",          quantity = 100, category = "Wood" },
            new InventoryItemData { itemId = "wood_plank",        displayName = "Wood Plank",        quantity = 0, category = "Wood" },
            new InventoryItemData { itemId = "wood_stick",        displayName = "Wood Stick",        quantity = 0, category = "Wood" },
            // Material
            new InventoryItemData { itemId = "stone",             displayName = "Stone",             quantity = 0, category = "Material" },
            new InventoryItemData { itemId = "torch",             displayName = "Torch",             quantity = 0, category = "Material" },
            new InventoryItemData { itemId = "health_potion",     displayName = "Health Potion",     quantity = 3, category = "Material" },
            // Pickaxes
            new InventoryItemData { itemId = "pickaxe_wooden",    displayName = "Wooden Pickaxe",    quantity = 0, category = "Pickaxe" },
            new InventoryItemData { itemId = "pickaxe_stone",     displayName = "Stone Pickaxe",     quantity = 0, category = "Pickaxe" },
            new InventoryItemData { itemId = "pickaxe_copper",    displayName = "Copper Pickaxe",    quantity = 0, category = "Pickaxe" },
            new InventoryItemData { itemId = "pickaxe_iron",      displayName = "Iron Pickaxe",      quantity = 0, category = "Pickaxe" },
            new InventoryItemData { itemId = "pickaxe_diamond",   displayName = "Diamond Pickaxe",   quantity = 0, category = "Pickaxe" },
            new InventoryItemData { itemId = "pickaxe_titanium",  displayName = "Titanium Pickaxe",  quantity = 0, category = "Pickaxe" },
            // Swords
            new InventoryItemData { itemId = "sword_wooden",      displayName = "Wooden Sword",      quantity = 0, category = "Sword" },
            new InventoryItemData { itemId = "sword_stone",       displayName = "Stone Sword",       quantity = 0, category = "Sword" },
            new InventoryItemData { itemId = "sword_copper",      displayName = "Copper Sword",      quantity = 0, category = "Sword" },
            new InventoryItemData { itemId = "sword_iron",        displayName = "Iron Sword",        quantity = 0, category = "Sword" },
            new InventoryItemData { itemId = "sword_diamond",     displayName = "Diamond Sword",     quantity = 0, category = "Sword" },
            new InventoryItemData { itemId = "sword_titanium",    displayName = "Titanium Sword",    quantity = 0, category = "Sword" },
            // Axes
            new InventoryItemData { itemId = "axe_wooden",        displayName = "Wooden Axe",        quantity = 0, category = "Axe" },
            new InventoryItemData { itemId = "axe_stone",         displayName = "Stone Axe",         quantity = 0, category = "Axe" },
            new InventoryItemData { itemId = "axe_copper",        displayName = "Copper Axe",        quantity = 0, category = "Axe" },
            new InventoryItemData { itemId = "axe_iron",          displayName = "Iron Axe",          quantity = 0, category = "Axe" },
            new InventoryItemData { itemId = "axe_diamond",       displayName = "Diamond Axe",       quantity = 0, category = "Axe" },
            new InventoryItemData { itemId = "axe_titanium",      displayName = "Titanium Axe",      quantity = 0, category = "Axe" },
            // Hammers
            new InventoryItemData { itemId = "hammer_wooden",     displayName = "Wooden Hammer",     quantity = 0, category = "Hammer" },
            new InventoryItemData { itemId = "hammer_stone",      displayName = "Stone Hammer",      quantity = 0, category = "Hammer" },
            new InventoryItemData { itemId = "hammer_copper",     displayName = "Copper Hammer",     quantity = 0, category = "Hammer" },
            new InventoryItemData { itemId = "hammer_iron",       displayName = "Iron Hammer",       quantity = 0, category = "Hammer" },
            new InventoryItemData { itemId = "hammer_diamond",    displayName = "Diamond Hammer",    quantity = 0, category = "Hammer" },
            new InventoryItemData { itemId = "hammer_titanium",   displayName = "Titanium Hammer",   quantity = 0, category = "Hammer" },
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