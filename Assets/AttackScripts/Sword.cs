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

    public void CheckForBetterAxe(string itemId)
    {
        int quantity = InventoryManager.Instance.GetQuantity(itemId);
        if (quantity < 1) return;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (itemId == "axe_stone") { attackDamage = 3; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "axe_copper") { attackDamage = 5; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "axe_iron") { attackDamage = 8; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "axe_diamond") { attackDamage = 10; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "axe_titanium") { attackDamage = 20; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
    }

    private void FixedUpdate()
    {
        foreach (var item in InventoryManager.Instance.GetAllItems())
        {
            if (item.quantity >= 1)
                CheckForBetterAxe(item.itemId);
        }
    }
}
