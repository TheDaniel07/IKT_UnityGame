using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuRoot;
    [SerializeField] private KeyCode toggleKey = KeyCode.I;
    [SerializeField] private bool exportOnOpen = false;
    [SerializeField] private bool exportOnClose = true;

    private bool _isOpen = false;

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
        List<InventoryItemData> items = InventoryManager.Instance.GetAllItems();
        InventoryExporter.Export(items);
    }
}