using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class CraftingRecipe
{
    public int rows;
    public int cols;
    public int[] shape;
    public Item result;

    public int[,] GetShape()
    {
        int[,] s = new int[rows, cols];
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                s[r, c] = shape[r * cols + c];
        return s;
    }
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

    private int[,] GetNormalizedGrid()
    {
        int[,] grid = new int[3, 3];

        for (int i = 0; i < 9; i++)
        {
            grid[i / 3, i % 3] = itemList[i] != null ? itemList[i].itemID : -1;
        }

        int minRow = 3, maxRow = -1, minCol = 3, maxCol = -1;

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (grid[r, c] != -1)
                {
                    if (r < minRow) minRow = r;
                    if (r > maxRow) maxRow = r;
                    if (c < minCol) minCol = c;
                    if (c > maxCol) maxCol = c;
                }
            }
        }

        if (maxRow == -1) return null;

        int rows = maxRow - minRow + 1;
        int cols = maxCol - minCol + 1;
        int[,] normalized = new int[rows, cols];

        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                normalized[r, c] = grid[minRow + r, minCol + c];

        return normalized;
    }

    private void CheckForCompletedRecipes()
    {
        resultSlot.ClearSlot();

        int[,] current = GetNormalizedGrid();
        if (current == null) return;

        foreach (CraftingRecipe recipe in recipes)
        {
            int[,] recipeShape = recipe.GetShape();

            if (recipeShape.GetLength(0) != current.GetLength(0)) continue;
            if (recipeShape.GetLength(1) != current.GetLength(1)) continue;

            bool matched = true;
            for (int r = 0; r < recipeShape.GetLength(0); r++)
            {
                for (int c = 0; c < recipeShape.GetLength(1); c++)
                {
                    if (recipeShape[r, c] != current[r, c])
                    {
                        matched = false;
                        break;
                    }
                }
                if (!matched) break;
            }

            if (matched)
            {
                resultSlot.SetItem(recipe.result);
                return;
            }
        }
    }
}