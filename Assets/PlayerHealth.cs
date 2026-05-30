using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthBar;

    public float invincibilityTime = 1f;
    private float lastHitTime = -999f;
    [SerializeField] private ParticleSystem damageParticles;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (Time.time - lastHitTime < invincibilityTime) return;
        lastHitTime = Time.time;

        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);
        SpawnDamageParticles();

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        Debug.Log("Player died!");
    }

    void SpawnDamageParticles()
    {
        ParticleSystem instance = Instantiate(damageParticles, transform.position, Quaternion.identity);
        instance.Play();
        Destroy(instance.gameObject, instance.main.duration);
    }
}