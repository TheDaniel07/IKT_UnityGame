using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string itemName;
    public int itemID;
    public int stackCount = 1;
    public int maxStack = 99;
    private Image itemImage;

    private void Awake()
    {
        itemImage = GetComponent<Image>();
    }

    public void SetVisible(bool visible)
    {
        if (itemImage == null)
            itemImage = GetComponent<Image>();

        Color c = itemImage.color;
        c.a = visible ? 1f : 0f;
        itemImage.color = c;
    }

    public Sprite GetSprite()
    {
        if (itemImage == null)
            itemImage = GetComponent<Image>();
        return itemImage.sprite;
    }

    public int AddToStack(int amount)
    {
        int total = stackCount + amount;
        if (total <= maxStack)
        {
            stackCount = total;
            return 0;
        }
        stackCount = maxStack;
        return total - maxStack;
    }

    public int RemoveFromStack(int amount)
    {
        int removed = Mathf.Min(stackCount, amount);
        stackCount -= removed;
        return removed;
    }
}
