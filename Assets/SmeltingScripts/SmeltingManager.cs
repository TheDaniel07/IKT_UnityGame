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
            Debug.Log("Picked up " + item.itemName);
        }
    }

    public void OnClickSlot(Slot slot)
    {
        slot.ClearSlot();
        CheckForCompletedRecipes();
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