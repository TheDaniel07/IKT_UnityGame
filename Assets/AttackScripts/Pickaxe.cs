using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage { get; set; } = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Mineable mineable = collision.GetComponent<Mineable>();

        if(mineable != null)
        {
            mineable.TakeDamage(damage);
        }
    }

    public void CheckForBetterPickaxe(string itemId)
    {
        int quantity = InventoryManager.Instance.GetQuantity(itemId);
        if (quantity < 1) return;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (itemId == "pickaxe_stone") { damage = 3; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "pickaxe_copper") { damage = 5; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "pickaxe_iron") { damage = 8; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "pickaxe_diamond") { damage = 10; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "pickaxe_titanium") { damage = 20; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
    }

    private void FixedUpdate()
    {
        foreach (var item in InventoryManager.Instance.GetAllItems())
        {
            if (item.quantity >= 1)
                CheckForBetterPickaxe(item.itemId);
        }
    }
}
