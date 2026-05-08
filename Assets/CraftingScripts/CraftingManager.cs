using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    // The item currently being dragged
    private Item currentItem;

    // The image that follows the mouse as a drag cursor
    public Image customCursor;

    // The 4 ingredient crafting slots (assign in Inspector)
    public Slot[] craftingSlots;

    // The result slot (assign in Inspector)
    public Slot resultSlot;

    // How close (in pixels) the mouse must be to snap into a slot
    public float snapDistance = 100f;

    private void Update()
    {
        // On left mouse button RELEASE (not hold), attempt to place the item
        if (Input.GetMouseButtonUp(0))
        {
            if (currentItem != null)
            {
                TryPlaceItem();
            }
        }
    }

    // Called by the EventTrigger (PointerDown) on each item
    public void OnMouseDownItem(Item item)
    {
        if (currentItem == null)
        {
            currentItem = item;

            // Hide the item at its original position while dragging
            currentItem.SetVisible(false);

            // Show and update the drag cursor
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.GetComponent<Image>().sprite;
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

        // Only place if close enough to a slot
        if (nearestSlot != null && shortestDistance <= snapDistance)
        {
            // If the slot already has an item, restore that item's visibility
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
            // Not close enough — return item to its original position
            currentItem.SetVisible(true);
            DropItem();
        }
    }

    private void DropItem()
    {
        currentItem = null;
        customCursor.gameObject.SetActive(false);
    }

    // ---------------------------------------------------------------
    // CRAFTING RECIPES
    // Define your recipes here. craftingSlots[0..3] are the 4 slots.
    // resultSlot will display the crafted item's sprite.
    // ---------------------------------------------------------------
    private void CheckCraftingResult()
    {
        if (resultSlot == null) return;

        // Example recipe: all 4 slots filled with Wood => Planks
        // Replace or extend this with your actual recipes.
        bool allFilled = true;
        bool allWood = true;

        foreach (Slot slot in craftingSlots)
        {
            if (slot.IsEmpty())
            {
                allFilled = false;
                break;
            }
            if (slot.item.itemName != "Wood")
            {
                allWood = false;
            }
        }

        if (allFilled && allWood)
        {
            // Show result — assign your result item in the resultSlot manually
            // or swap the sprite like below if you have a result Item reference.
            Debug.Log("Recipe matched: 4x Wood => Planks!");
            // resultSlot.SetItem(yourResultItem); // wire this up as needed
        }
        else
        {
            resultSlot.ClearSlot();
        }
    }
}
