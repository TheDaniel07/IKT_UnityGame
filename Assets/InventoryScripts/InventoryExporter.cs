using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class InventoryItemData
{
    public string itemId;
    public string displayName;
    public int quantity;
    public string category;
    public Sprite icon;
}

[Serializable]
public class InventorySnapshot
{
    public string exportedAt;
    public int totalUniqueItems;
    public List<InventoryItemData> items = new();
}

public static class InventoryExporter
{
    private const string DEFAULT_FILENAME = "inventory_snapshot.json";

    public static string Export(IEnumerable<InventoryItemData> items, string fileName = DEFAULT_FILENAME)
    {
        try
        {
            var snapshot = new InventorySnapshot
            {
                exportedAt = DateTime.UtcNow.ToString("o"),
                items = new List<InventoryItemData>(items),
                totalUniqueItems = 0
            };
            snapshot.totalUniqueItems = snapshot.items.Count;

            string json = JsonUtility.ToJson(snapshot, prettyPrint: true);
            string filePath = Path.Combine(Application.persistentDataPath, fileName);

            File.WriteAllText(filePath, json);
            Debug.Log($"[InventoryExporter] Saved successfully -> {filePath}");
            return filePath;
        }
        catch (Exception ex)
        {
            Debug.LogError($"[InventoryExporter] Save error: {ex.Message}");
            return null;
        }
    }

    public static InventorySnapshot Load(string fileName = DEFAULT_FILENAME)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        if (!File.Exists(filePath))
        {
            Debug.LogWarning($"[InventoryExporter] No save file found: {filePath}");
            return null;
        }

        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<InventorySnapshot>(json);
    }
}