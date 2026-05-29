using UnityEngine;

public class Mineable : MonoBehaviour
{
    public float setMaxHealth;
    float health, maxHealth;
    void Start()
    {
        maxHealth = setMaxHealth;
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
