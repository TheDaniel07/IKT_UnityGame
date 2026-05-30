using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Mineable mineable = collision.GetComponent<Mineable>();

        if(mineable != null)
        {
            mineable.TakeDamage(damage);
        }
    }
}
