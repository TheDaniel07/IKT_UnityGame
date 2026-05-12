using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class SmeltingRecipe
{
    public int inputID;
    public Item result;
}

public class SmeltingManager : MonoBehaviour
{
    private Item currentItem;
    public Slot inputSlot;
    public Slot fuelSlot;
    public Slot resultSlot;
    public List<SmeltingRecipe> recipes;
    public int[] validFuelIDs;
    public InventoryManager inventoryManager;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (currentItem != null)
            {
                float distInput = Vector2.Distance(Input.mousePosition, inputSlot.transform.position);
                float distFuel = Vector2.Distance(Input.mousePosition, fuelSlot.transform.position);

                Slot nearestSlot = distInput < distFuel ? inputSlot : fuelSlot;

                nearestSlot.SetItem(currentItem);
                currentItem = null;

                CheckForCompletedRecipes();
            }
        }
    }

    public void OnMouseDownItem(Item item)
    {
        if (currentItem == null)
        {
            currentItem = item;
        }
    }

    public void OnClickSlot(Slot slot)
    {
        slot.ClearSlot();
        CheckForCompletedRecipes();
    }

    public void OnResultTaken()
    {
        if (resultSlot.item == null) return;

        if (inventoryManager != null)
        {
            int remaining = inventoryManager.AddItem(resultSlot.item, resultSlot.item.stackCount);
            if (remaining > 0) return;
        }

        inputSlot.ClearSlot();
        fuelSlot.ClearSlot();
        resultSlot.ClearSlot();
    }

    private bool IsValidFuel(int id)
    {
        foreach (int fuelID in validFuelIDs)
        {
            if (fuelID == id) return true;
        }
        return false;
    }

    private void CheckForCompletedRecipes()
    {
        resultSlot.ClearSlot();

        if (inputSlot.item == null || fuelSlot.item == null) return;

        if (!IsValidFuel(fuelSlot.item.itemID)) return;

        int inputID = inputSlot.item.itemID;

        foreach (SmeltingRecipe recipe in recipes)
        {
            if (recipe.inputID == inputID)
            {
                resultSlot.SetItem(recipe.result);
                return;
            }
        }
    }
}