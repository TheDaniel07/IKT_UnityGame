using UnityEngine;

public class EnemyContact : MonoBehaviour
{
    public int damage = 10;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }
}