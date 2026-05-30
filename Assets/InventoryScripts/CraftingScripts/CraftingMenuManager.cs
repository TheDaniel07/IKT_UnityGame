using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CraftingMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuRoot;
    [SerializeField] private KeyCode toggleKey = KeyCode.C;
    [SerializeField] private Transform recipeListParent;
    [SerializeField] private GameObject recipeRowPrefab;
    [SerializeField] private TextMeshProUGUI selectedRecipeInfoText;

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
        Debug.Log("[CraftingMenuManager] Menu opened.");
    }

    public void CloseMenu()
    {
        if (!_isOpen) return;
        SetMenuVisible(false);
        _isOpen = false;
        if (selectedRecipeInfoText != null) selectedRecipeInfoText.text = "";
        Debug.Log("[CraftingMenuManager] Menu closed.");
    }

    private void SetMenuVisible(bool visible)
    {
        if (menuRoot != null) menuRoot.SetActive(visible);
    }

    private void RefreshList()
    {
        foreach (var row in _rows) Destroy(row);
        _rows.Clear();

        foreach (var recipe in RecipeDatabase.CraftingRecipes)
        {
            bool canCraft = RecipeDatabase.CanCraft(recipe);
            var row = Instantiate(recipeRowPrefab, recipeListParent);
            _rows.Add(row);

            var allItems = InventoryManager.Instance.GetAllItems();
            var outputItem = allItems.Find(i => i.itemId == recipe.outputItemId);
            string outputName = outputItem?.displayName ?? recipe.outputItemId;

            var label = row.GetComponentInChildren<TextMeshProUGUI>();
            if (label != null)
                label.text = canCraft
                    ? $"{outputName} x{recipe.outputAmount}"
                    : $"<color=#888888>{outputName} x{recipe.outputAmount}</color>";

            var iconImage = row.transform.Find("Icon")?.GetComponent<Image>();
            if (iconImage != null && outputItem?.icon != null)
            {
                iconImage.sprite = outputItem.icon;
                iconImage.enabled = true;
            }

            var btn = row.GetComponentInChildren<Button>();
            if (btn != null)
            {
                btn.interactable = canCraft;
                var r = recipe;
                btn.onClick.AddListener(() => OnRecipeSelected(r));
            }
        }
    }

    private void OnRecipeSelected(Recipe recipe)
    {
        if (selectedRecipeInfoText != null)
        {
            var allItems = InventoryManager.Instance.GetAllItems();
            string outputName = allItems.Find(i => i.itemId == recipe.outputItemId)?.displayName ?? recipe.outputItemId;

            var lines = new System.Text.StringBuilder();
            lines.AppendLine($"Craft: {outputName} x{recipe.outputAmount}");
            lines.AppendLine("Required:");
            foreach (var ing in recipe.ingredients)
            {
                string ingName = allItems.Find(i => i.itemId == ing.itemId)?.displayName ?? ing.itemId;
                int have = InventoryManager.Instance.GetQuantity(ing.itemId);
                lines.AppendLine($"  {ingName}: {have}/{ing.amount}");
            }
            selectedRecipeInfoText.text = lines.ToString();
        }

        RecipeDatabase.ExecuteRecipe(recipe);
        RefreshList();
        InventoryExporter.Export(InventoryManager.Instance.GetAllItems());
    }
}