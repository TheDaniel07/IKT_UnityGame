using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class CraftingRecipe
{
    public int slot0;
    public int slot1;
    public int slot2;
    public int slot3;
    public Item result;
}

public class CraftingManager : MonoBehaviour
{
    private Item currentItem;
    public Slot[] craftingSlots;
    public List<Item> itemList;
    public List<CraftingRecipe> recipes;
    public Slot resultSlot;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (currentItem != null)
            {
                Slot nearestSlot = null;
                float closestDistance = float.MaxValue;

                foreach (Slot slot in craftingSlots)
                {
                    float distance = Vector2.Distance(Input.mousePosition, slot.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        nearestSlot = slot;
                    }
                }

                nearestSlot.SetItem(currentItem);
                itemList[nearestSlot.slotIndex] = currentItem;
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
        itemList[slot.slotIndex] = null;
        CheckForCompletedRecipes();
    }

    private void CheckForCompletedRecipes()
    {
        resultSlot.ClearSlot();

        int id0 = itemList[0] != null ? itemList[0].itemID : -1;
        int id1 = itemList[1] != null ? itemList[1].itemID : -1;
        int id2 = itemList[2] != null ? itemList[2].itemID : -1;
        int id3 = itemList[3] != null ? itemList[3].itemID : -1;

        foreach (CraftingRecipe recipe in recipes)
        {
            if (recipe.slot0 == id0 && recipe.slot1 == id1 && recipe.slot2 == id2 && recipe.slot3 == id3)
            {
                resultSlot.SetItem(recipe.result);
                return;
            }
        }
    }
}