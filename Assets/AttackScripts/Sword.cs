using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackDamage = 20f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();

        if(enemy != null)
        {
            enemy.TakeDamage(attackDamage);
        }
    }

    public void CheckForBettersword(string itemId)
    {
        int quantity = InventoryManager.Instance.GetQuantity(itemId);
        if (quantity < 1) return;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (itemId == "sword_stone") { attackDamage = 40f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "sword_copper") { attackDamage = 60f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "sword_iron") { attackDamage = 70f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "sword_diamond") { attackDamage = 80f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "sword_titanium") { attackDamage = 100f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
    }

    private void FixedUpdate()
    {
        foreach (var item in InventoryManager.Instance.GetAllItems())
        {
            if (item.quantity >= 1)
                CheckForBettersword(item.itemId);
        }
    }
}
