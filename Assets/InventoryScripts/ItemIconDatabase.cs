using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemIconEntry
{
    public string itemId;
    public Sprite icon;
}

public class ItemIconDatabase : MonoBehaviour
{
    public static ItemIconDatabase Instance { get; private set; }

    [SerializeField] private List<ItemIconEntry> entries = new();

    private Dictionary<string, Sprite> _lookup = new();

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        foreach (var entry in entries)
            _lookup[entry.itemId] = entry.icon;
    }

    private void Start()
    {
        ApplyToInventory();
    }

    private void ApplyToInventory()
    {
        foreach (var item in InventoryManager.Instance.GetAllItems())
        {
            if (_lookup.TryGetValue(item.itemId, out var sprite))
                item.icon = sprite;
        }
    }

    public Sprite GetIcon(string itemId)
        => _lookup.TryGetValue(itemId, out var s) ? s : null;
}