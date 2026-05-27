using UnityEngine;

public class Mineable : MonoBehaviour
{
    float health, maxHealth = 3f;
    void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health < 1)
        {
            Destroy(gameObject);
            Debug.Log("Destroyed");
        }
    }
}
