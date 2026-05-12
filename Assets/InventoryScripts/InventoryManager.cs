using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("UI")]
    public GameObject inventoryPanel;

    [Header("Slots")]
    public Slot[] inventorySlots;
    public Slot[] hotbarSlots;

    [Header("Cursor")]
    public Image cursorItemImage;
    public Text cursorStackText;

    [Header("Hotbar")]
    public int selectedHotbarIndex = 0;
    public Image hotbarSelector;

    private Item heldItem = null;
    private Sprite heldSprite = null;
    private int heldStackCount = 0;
    private int heldItemID = -1;
    private string heldItemName = "";
    private int heldMaxStack = 99;
    private bool inventoryOpen = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].slotIndex = i;
            inventorySlots[i].inventoryManager = this;
        }

        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            hotbarSlots[i].slotIndex = i + 1000;
            hotbarSlots[i].inventoryManager = this;
        }

        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);

        SetCursorItemVisible(false);
        UpdateHotbarSelector();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            ToggleInventory();

        if (heldItem != null || heldStackCount > 0)
        {
            if (cursorItemImage != null)
                cursorItemImage.transform.position = Input.mousePosition;
        }

        for (int i = 0; i < Mathf.Min(hotbarSlots.Length, 7); i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectedHotbarIndex = i;
                UpdateHotbarSelector();
            }
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            if (scroll > 0f)
                selectedHotbarIndex--;
            else
                selectedHotbarIndex++;

            if (selectedHotbarIndex < 0) selectedHotbarIndex = hotbarSlots.Length - 1;
            if (selectedHotbarIndex >= hotbarSlots.Length) selectedHotbarIndex = 0;
            UpdateHotbarSelector();
        }
    }

    public void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;
        if (inventoryPanel != null)
            inventoryPanel.SetActive(inventoryOpen);

        if (!inventoryOpen && IsHoldingItem())
            ReturnHeldItem();
    }

    public bool IsInventoryOpen()
    {
        return inventoryOpen;
    }

    public void OnSlotLeftClick(Slot slot)
    {
        if (!IsHoldingItem())
        {
            if (!slot.IsEmpty())
                PickUpItem(slot);
        }
        else
        {
            if (slot.IsEmpty())
                PlaceItem(slot);
            else if (slot.item.itemID == heldItemID)
                StackItems(slot);
            else
                SwapItems(slot);
        }
    }

    public void OnSlotRightClick(Slot slot)
    {
        if (!IsHoldingItem())
        {
            if (!slot.IsEmpty() && slot.item.stackCount > 1)
                PickUpHalfStack(slot);
            else if (!slot.IsEmpty())
                PickUpItem(slot);
        }
        else
        {
            if (slot.IsEmpty())
            {
                PlaceOneItem(slot);
            }
            else if (slot.item.itemID == heldItemID && slot.item.stackCount < slot.item.maxStack)
            {
                slot.item.stackCount++;
                slot.UpdateStackCount();
                heldStackCount--;

                if (heldStackCount <= 0)
                    ClearHeldItem();
                else
                    UpdateCursorDisplay();
            }
        }
    }

    private void PickUpItem(Slot slot)
    {
        heldItem = slot.item;
        heldSprite = slot.GetComponent<Image>().sprite;
        heldStackCount = slot.item.stackCount;
        heldItemID = slot.item.itemID;
        heldItemName = slot.item.itemName;
        heldMaxStack = slot.item.maxStack;

        slot.ClearSlot();
        SetCursorItemVisible(true);
        UpdateCursorDisplay();
    }

    private void PickUpHalfStack(Slot slot)
    {
        int total = slot.item.stackCount;
        int takeAmount = Mathf.CeilToInt(total / 2f);
        int leaveAmount = total - takeAmount;

        heldItem = slot.item;
        heldSprite = slot.GetComponent<Image>().sprite;
        heldItemID = slot.item.itemID;
        heldItemName = slot.item.itemName;
        heldMaxStack = slot.item.maxStack;
        heldStackCount = takeAmount;

        if (leaveAmount > 0)
        {
            slot.item.stackCount = leaveAmount;
            slot.UpdateStackCount();
        }
        else
        {
            slot.ClearSlot();
        }

        SetCursorItemVisible(true);
        UpdateCursorDisplay();
    }

    private void PlaceItem(Slot slot)
    {
        if (heldItem != null)
        {
            heldItem.stackCount = heldStackCount;
            slot.SetItem(heldItem);
            heldItem.transform.SetParent(slot.transform);
            heldItem.transform.localPosition = Vector3.zero;
            heldItem.SetVisible(false);
        }
        ClearHeldItem();
    }

    private void PlaceOneItem(Slot slot)
    {
        if (heldItem != null)
        {
            GameObject newItemObj = Instantiate(heldItem.gameObject, slot.transform);
            newItemObj.transform.localPosition = Vector3.zero;
            Item newItem = newItemObj.GetComponent<Item>();
            newItem.stackCount = 1;
            newItem.SetVisible(false);
            slot.SetItem(newItem);

            heldStackCount--;
            if (heldStackCount <= 0)
                ClearHeldItem();
            else
                UpdateCursorDisplay();
        }
    }

    private void StackItems(Slot slot)
    {
        int spaceLeft = slot.item.maxStack - slot.item.stackCount;

        if (spaceLeft >= heldStackCount)
        {
            slot.item.stackCount += heldStackCount;
            slot.UpdateStackCount();
            ClearHeldItem();
        }
        else if (spaceLeft > 0)
        {
            slot.item.stackCount = slot.item.maxStack;
            slot.UpdateStackCount();
            heldStackCount -= spaceLeft;
            UpdateCursorDisplay();
        }
    }

    private void SwapItems(Slot slot)
    {
        Item tempItem = slot.item;
        Sprite tempSprite = slot.GetComponent<Image>().sprite;
        int tempCount = slot.item.stackCount;
        int tempID = slot.item.itemID;
        string tempName = slot.item.itemName;
        int tempMaxStack = slot.item.maxStack;

        PlaceItem(slot);

        heldItem = tempItem;
        heldSprite = tempSprite;
        heldStackCount = tempCount;
        heldItemID = tempID;
        heldItemName = tempName;
        heldMaxStack = tempMaxStack;

        SetCursorItemVisible(true);
        UpdateCursorDisplay();
    }

    public int AddItem(Item itemPrefab, int count)
    {
        int remaining = count;
        int targetID = itemPrefab.itemID;

        remaining = TryStackOnExisting(hotbarSlots, targetID, remaining);
        if (remaining <= 0) return 0;

        remaining = TryStackOnExisting(inventorySlots, targetID, remaining);
        if (remaining <= 0) return 0;

        remaining = TryPlaceInEmpty(hotbarSlots, itemPrefab, remaining);
        if (remaining <= 0) return 0;

        remaining = TryPlaceInEmpty(inventorySlots, itemPrefab, remaining);
        return remaining;
    }

    public int RemoveItem(int itemID, int count)
    {
        int remaining = count;

        remaining = TryRemoveFrom(inventorySlots, itemID, remaining);
        if (remaining <= 0) return count;

        remaining = TryRemoveFrom(hotbarSlots, itemID, remaining);
        return count - remaining;
    }

    public bool HasItem(int itemID, int count)
    {
        return CountItem(itemID) >= count;
    }

    public int CountItem(int itemID)
    {
        int total = 0;
        foreach (Slot s in hotbarSlots)
        {
            if (!s.IsEmpty() && s.item.itemID == itemID)
                total += s.item.stackCount;
        }
        foreach (Slot s in inventorySlots)
        {
            if (!s.IsEmpty() && s.item.itemID == itemID)
                total += s.item.stackCount;
        }
        return total;
    }

    public Item GetSelectedHotbarItem()
    {
        if (selectedHotbarIndex >= 0 && selectedHotbarIndex < hotbarSlots.Length)
            return hotbarSlots[selectedHotbarIndex].item;
        return null;
    }

    private int TryStackOnExisting(Slot[] slots, int itemID, int remaining)
    {
        foreach (Slot slot in slots)
        {
            if (remaining <= 0) break;
            if (!slot.IsEmpty() && slot.item.itemID == itemID)
            {
                int space = slot.item.maxStack - slot.item.stackCount;
                if (space > 0)
                {
                    int add = Mathf.Min(space, remaining);
                    slot.item.stackCount += add;
                    slot.UpdateStackCount();
                    remaining -= add;
                }
            }
        }
        return remaining;
    }

    private int TryPlaceInEmpty(Slot[] slots, Item itemPrefab, int remaining)
    {
        foreach (Slot slot in slots)
        {
            if (remaining <= 0) break;
            if (slot.IsEmpty())
            {
                int placeCount = Mathf.Min(remaining, itemPrefab.maxStack);

                GameObject newItemObj = Instantiate(itemPrefab.gameObject, slot.transform);
                newItemObj.transform.localPosition = Vector3.zero;
                Item newItem = newItemObj.GetComponent<Item>();
                newItem.stackCount = placeCount;
                newItem.SetVisible(false);

                slot.SetItem(newItem);
                remaining -= placeCount;
            }
        }
        return remaining;
    }

    private int TryRemoveFrom(Slot[] slots, int itemID, int remaining)
    {
        foreach (Slot slot in slots)
        {
            if (remaining <= 0) break;
            if (!slot.IsEmpty() && slot.item.itemID == itemID)
            {
                int remove = Mathf.Min(slot.item.stackCount, remaining);
                slot.item.stackCount -= remove;
                remaining -= remove;

                if (slot.item.stackCount <= 0)
                {
                    Destroy(slot.item.gameObject);
                    slot.ClearSlot();
                }
                else
                {
                    slot.UpdateStackCount();
                }
            }
        }
        return remaining;
    }

    private bool IsHoldingItem()
    {
        return heldStackCount > 0 && heldItem != null;
    }

    private void SetCursorItemVisible(bool visible)
    {
        if (cursorItemImage != null)
        {
            cursorItemImage.enabled = visible;
            cursorItemImage.raycastTarget = false;
        }
        if (cursorStackText != null)
            cursorStackText.enabled = visible;
    }

    private void UpdateCursorDisplay()
    {
        if (cursorItemImage != null && heldSprite != null)
        {
            cursorItemImage.sprite = heldSprite;
            cursorItemImage.enabled = true;
            cursorItemImage.raycastTarget = false;
        }

        if (cursorStackText != null)
        {
            if (heldStackCount > 1)
                cursorStackText.text = heldStackCount.ToString();
            else
                cursorStackText.text = "";
        }
    }

    private void ClearHeldItem()
    {
        heldItem = null;
        heldSprite = null;
        heldStackCount = 0;
        heldItemID = -1;
        heldItemName = "";
        SetCursorItemVisible(false);
    }

    private void ReturnHeldItem()
    {
        if (!IsHoldingItem()) return;

        int remaining = heldStackCount;

        remaining = TryStackOnExistingHeld(hotbarSlots, remaining);
        if (remaining <= 0) { ClearHeldItem(); return; }

        remaining = TryStackOnExistingHeld(inventorySlots, remaining);
        if (remaining <= 0) { ClearHeldItem(); return; }

        foreach (Slot slot in hotbarSlots)
        {
            if (remaining <= 0) break;
            if (slot.IsEmpty() && heldItem != null)
            {
                int place = Mathf.Min(remaining, heldMaxStack);
                heldItem.stackCount = place;
                slot.SetItem(heldItem);
                heldItem.transform.SetParent(slot.transform);
                heldItem.transform.localPosition = Vector3.zero;
                heldItem.SetVisible(false);
                remaining -= place;
                break;
            }
        }

        if (remaining > 0)
        {
            foreach (Slot slot in inventorySlots)
            {
                if (remaining <= 0) break;
                if (slot.IsEmpty() && heldItem != null)
                {
                    heldItem.stackCount = remaining;
                    slot.SetItem(heldItem);
                    heldItem.transform.SetParent(slot.transform);
                    heldItem.transform.localPosition = Vector3.zero;
                    heldItem.SetVisible(false);
                    remaining = 0;
                    break;
                }
            }
        }

        if (remaining > 0 && heldItem != null)
            Destroy(heldItem.gameObject);

        ClearHeldItem();
    }

    private int TryStackOnExistingHeld(Slot[] slots, int remaining)
    {
        foreach (Slot slot in slots)
        {
            if (remaining <= 0) break;
            if (!slot.IsEmpty() && slot.item.itemID == heldItemID)
            {
                int space = slot.item.maxStack - slot.item.stackCount;
                if (space > 0)
                {
                    int add = Mathf.Min(space, remaining);
                    slot.item.stackCount += add;
                    slot.UpdateStackCount();
                    remaining -= add;
                }
            }
        }
        return remaining;
    }

    private void UpdateHotbarSelector()
    {
        if (hotbarSelector == null) return;

        if (selectedHotbarIndex >= 0 && selectedHotbarIndex < hotbarSlots.Length)
        {
            hotbarSelector.transform.position = hotbarSlots[selectedHotbarIndex].transform.position;
            hotbarSelector.enabled = true;
        }
    }
}
