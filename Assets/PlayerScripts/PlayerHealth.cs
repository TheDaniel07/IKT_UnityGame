using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    public GameObject youdiedPanel;

    public HealthBar healthBar;

    public float invincibilityTime = 1f;
    private float lastHitTime = -999f;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        youdiedPanel.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        if (Time.time - lastHitTime < invincibilityTime) return;
        lastHitTime = Time.time;

        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0) Die();
    }

    public void Heal(int amount)
    {
        if (currentHealth <= 90)
        {
            currentHealth += amount;
            healthBar.SetHealth(currentHealth);
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        youdiedPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}