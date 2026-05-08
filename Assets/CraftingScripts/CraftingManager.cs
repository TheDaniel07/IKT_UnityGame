using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class CraftingRecipe
{
    public string recipeName;
    public int[] ingredientIDs;
    public Item resultItem;
}

public class CraftingManager : MonoBehaviour
{
    private Item currentItem;
    //public Image customCursor;
    public Slot[] craftingSlots;
    public Slot resultSlot;
    public float snapDistance = 100f;
    public List<CraftingRecipe> recipes = new List<CraftingRecipe>();

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (currentItem != null)
            {
                TryPlaceItem();
            }
        }
    }

    public void OnMouseDownItem(Item item)
    {
        if (currentItem == null)
        {
            currentItem = item;
            currentItem.SetVisible(false);
            //customCursor.gameObject.SetActive(true);
            //customCursor.sprite = currentItem.GetComponent<Image>().sprite;
        }
    }

    private void TryPlaceItem()
    {
        Slot nearestSlot = null;
        float shortestDistance = float.MaxValue;

        foreach (Slot slot in craftingSlots)
        {
            float dist = Vector2.Distance(Input.mousePosition, slot.transform.position);
            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                nearestSlot = slot;
            }
        }

        if (nearestSlot != null && shortestDistance <= snapDistance)
        {
            if (!nearestSlot.IsEmpty())
            {
                nearestSlot.item.SetVisible(true);
            }
            nearestSlot.SetItem(currentItem);
            DropItem();
            CheckCraftingResult();
        }
        else
        {
            currentItem.SetVisible(true);
            DropItem();
        }
    }

    private void DropItem()
    {
        currentItem = null;
        //customCursor.gameObject.SetActive(false);
    }

    private void CheckCraftingResult()
    {
        if (resultSlot == null) return;

        int[] currentIDs = new int[craftingSlots.Length];

        for (int i = 0; i < craftingSlots.Length; i++)
        {
            if (craftingSlots[i].IsEmpty())
            {
                resultSlot.ClearSlot();
                return;
            }
            currentIDs[i] = craftingSlots[i].item.itemID;
        }

        foreach (CraftingRecipe recipe in recipes)
        {
            if (recipe.ingredientIDs.Length != craftingSlots.Length)
                continue;

            int[] sortedRecipe = new int[recipe.ingredientIDs.Length];
            int[] sortedCurrent = new int[currentIDs.Length];

            System.Array.Copy(recipe.ingredientIDs, sortedRecipe, sortedRecipe.Length);
            System.Array.Copy(currentIDs, sortedCurrent, sortedCurrent.Length);

            System.Array.Sort(sortedRecipe);
            System.Array.Sort(sortedCurrent);

            bool matched = true;
            for (int i = 0; i < sortedRecipe.Length; i++)
            {
                if (sortedRecipe[i] != sortedCurrent[i])
                {
                    matched = false;
                    break;
                }
            }

            if (matched)
            {
                resultSlot.SetItem(recipe.resultItem);
                return;
            }
        }

        resultSlot.ClearSlot();
    }
}