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
}
