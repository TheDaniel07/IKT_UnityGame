using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public int slotIndex;

    private Image slotImage;
    private Sprite emptySprite; // null = no sprite

    private void Awake()
    {
        slotImage = GetComponent<Image>();
        // Save the default empty appearance
        emptySprite = slotImage.sprite;
    }

    // Place an item into this slot and update its visual
    public void SetItem(Item newItem)
    {
        item = newItem;
        if (newItem != null)
        {
            slotImage.sprite = newItem.GetComponent<Image>().sprite;
            slotImage.color = Color.white;
        }
        else
        {
            ClearSlot();
        }
    }

    // Clear the slot back to its empty state
    public void ClearSlot()
    {
        item = null;
        slotImage.sprite = emptySprite;
        // Keep the slot GameObject active — just make it semi-transparent to show it's empty
        Color c = slotImage.color;
        c.a = 0.3f;
        slotImage.color = c;
    }

    public bool IsEmpty()
    {
        return item == null;
    }
}
