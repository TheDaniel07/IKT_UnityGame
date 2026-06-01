using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackDamage = 15f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();

        if(enemy != null)
        {
            enemy.TakeDamage(attackDamage);
        }
    }

    public void CheckForBetterSword(string itemId)
    {
        int quantity = InventoryManager.Instance.GetQuantity(itemId);
        if (quantity < 1) return;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        //stone
        if (itemId == "hammer_stone") { attackDamage = 20f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "axe_stone") { attackDamage = 25f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "sword_stone") { attackDamage = 30f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        //copper
        if (itemId == "hammer_copper") { attackDamage = 35f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "axe_copper") { attackDamage = 40f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "sword_copper") { attackDamage = 45f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        //iron
        if (itemId == "hammer_iron") { attackDamage = 50f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "axe_iron") { attackDamage = 55f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "sword_iron") { attackDamage = 60f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        //diamond
        if (itemId == "hammer_diamond") { attackDamage = 65f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "axe_diamond") { attackDamage = 70f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "sword_diamond") { attackDamage = 75f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        //titanium
        if (itemId == "hammer_titanium") { attackDamage = 100f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "axe_titanium") { attackDamage = 100f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
        if (itemId == "sword_titanium") { attackDamage = 100f; if (sr != null) sr.sprite = ItemIconDatabase.Instance.GetIcon(itemId); }
    }

    private void FixedUpdate()
    {
        foreach (var item in InventoryManager.Instance.GetAllItems())
        {
            if (item.quantity >= 1)
                CheckForBetterSword(item.itemId);
        }
    }
}
