using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string itemName;

    private Image itemImage;

    private void Awake()
    {
        itemImage = GetComponent<Image>();
    }

    // Hide the item visually while it's being dragged
    public void SetVisible(bool visible)
    {
        if (itemImage == null)
            itemImage = GetComponent<Image>();

        Color c = itemImage.color;
        c.a = visible ? 1f : 0f;
        itemImage.color = c;
    }
}
