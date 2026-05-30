using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuRoot;
    [SerializeField] private KeyCode toggleKey = KeyCode.I;
    [SerializeField] private Transform listParent;
    [SerializeField] private GameObject itemRowPrefab;
    [SerializeField] private bool exportOnOpen = false;
    [SerializeField] private bool exportOnClose = true;

    private bool _isOpen = false;
    private List<GameObject> _rows = new();

    private void Start() => SetMenuVisible(false);

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey)) Toggle();
    }

    public void Toggle()
    {
        if (_isOpen) CloseMenu();
        else OpenMenu();
    }

    public void OpenMenu()
    {
        if (_isOpen) return;
        SetMenuVisible(true);
        _isOpen = true;
        RefreshList();
        if (exportOnOpen) DoExport();
        Debug.Log("[InventoryMenuManager] Menu opened.");
    }

    public void CloseMenu()
    {
        if (!_isOpen) return;
        if (exportOnClose) DoExport();
        SetMenuVisible(false);
        _isOpen = false;
        Debug.Log("[InventoryMenuManager] Menu closed.");
    }

    public void ExportNow() => DoExport();

    private void SetMenuVisible(bool visible)
    {
        if (menuRoot != null) menuRoot.SetActive(visible);
    }

    private void DoExport()
    {
        InventoryExporter.Export(InventoryManager.Instance.GetAllItems());
    }

    private void RefreshList()
    {
        foreach (var row in _rows) Destroy(row);
        _rows.Clear();

        var allItems = InventoryManager.Instance.GetAllItems()
            .Where(i => i.quantity > 0)
            .OrderBy(g => g.quantity);


        int i = 0;
        foreach (var item in allItems)
        {
            var row = Instantiate(itemRowPrefab, listParent);
            _rows.Add(row);

            var label = row.GetComponentInChildren<TextMeshProUGUI>();
            if (label != null) label.text = $"{item.displayName} x{item.quantity}";

            var iconImage = row.transform.Find("Icon")?.GetComponent<Image>();
            if (iconImage != null && item?.icon != null)
            {
                iconImage.sprite = item.icon;
                iconImage.enabled = true;
            }
            i++;
        }
    }
}