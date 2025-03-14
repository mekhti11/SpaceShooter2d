using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public GameObject explosionPrefab;
    
    private int currentHealth;
    public bool IsDead { get; private set; } = false;
    
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0 && !IsDead)
        {
            Die();
        }
    }
    
    void Die()
    {
        IsDead = true;
        
        // Create explosion effect
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        
        // Destroy the enemy
        Destroy(gameObject);
    }
}