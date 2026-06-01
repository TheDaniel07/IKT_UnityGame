using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 50f;
    public int knockbackStrength = -100;
    private float currentHealth;
    [SerializeField] private ParticleSystem damageParticles;
    private ParticleSystem damageParticlesInstance;
    [SerializeField] private AIChase AIChase;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. HP: {currentHealth}/{maxHealth}");

        SpawnDamageParticles();
        KnockBack();

        if (currentHealth <= 0f)
            Die();
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        Destroy(gameObject);
    }

    private void SpawnDamageParticles()
    {
        ParticleSystem instance = Instantiate(damageParticles, transform.position, Quaternion.identity);
        instance.Play();
        Destroy(instance.gameObject, instance.main.duration);
    }

    private void KnockBack()
    {
        AIChase.speed = knockbackStrength;
    }
}