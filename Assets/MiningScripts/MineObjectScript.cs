using System;
using Unity.VisualScripting;
using UnityEngine;

public class Mineable : MonoBehaviour
{
    public float setMaxHealth;
    float health, maxHealth;
    [SerializeField] Item item;
    
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
            InventoryManager.Instance.AddItem(item.itemId, 1);
            Destroy(gameObject);
            Debug.Log($"[Mineable] Destroyed and added {item.itemId} to inventory.");
        }
    }
}
