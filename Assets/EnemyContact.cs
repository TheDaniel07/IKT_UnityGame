using UnityEngine;

public class EnemyContact : MonoBehaviour
{
    public int damage = 10;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PauseController.IsGamePaused)
            {
                return;
            }
            other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }
}