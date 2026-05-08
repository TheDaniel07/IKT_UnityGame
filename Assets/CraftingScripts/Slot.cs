using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public int slotIndex;
    private Image slotImage;
    private Sprite emptySprite;

    private void Awake()
    {
        slotImage = GetComponent<Image>();
        emptySprite = slotImage.sprite;
    }

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

    public void ClearSlot()
    {
        item = null;
        slotImage.sprite = emptySprite;
        slotImage.color = Color.white;
    }

    public bool IsEmpty()
    {
        return item == null;
    }
}