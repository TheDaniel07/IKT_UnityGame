using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public int slotIndex;
    public Text stackCountText;
    private Image slotImage;
    private Sprite emptySprite;

    [HideInInspector]
    public InventoryManager inventoryManager;

    private void Awake()
    {
        slotImage = GetComponent<Image>();
        emptySprite = slotImage.sprite;

        if (stackCountText == null)
            stackCountText = GetComponentInChildren<Text>();
    }

    public void SetItem(Item newItem)
    {
        item = newItem;
        if (newItem != null)
        {
            slotImage.sprite = newItem.GetComponent<Image>().sprite;
            slotImage.color = Color.white;
            UpdateStackCount();
        }
        else
        {
            ClearSlot();
        }
    }

    public void SetItemData(Item newItem, Sprite sprite, int count)
    {
        item = newItem;
        if (newItem != null)
        {
            slotImage.sprite = sprite;
            slotImage.color = Color.white;
            item.stackCount = count;
            UpdateStackCount();
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
        slotImage.color = new Color(1, 1, 1, 0);
        if (stackCountText != null) stackCountText.text = "";
    }

    public bool IsEmpty()
    {
        return item == null;
    }

    public void UpdateStackCount()
    {
        if (stackCountText == null) return;
        if (item != null && item.stackCount > 1)
            stackCountText.text = item.stackCount.ToString();
        else
            stackCountText.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventoryManager == null) return;

        if (eventData.button == PointerEventData.InputButton.Left)
            inventoryManager.OnSlotLeftClick(this);
        else if (eventData.button == PointerEventData.InputButton.Right)
            inventoryManager.OnSlotRightClick(this);
    }
}