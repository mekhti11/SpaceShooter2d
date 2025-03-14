using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public float lifetime = 2f; // Automatically destroy after this time
    
    void Start()
    {
        // Destroy the laser after lifetime seconds
        Destroy(gameObject, lifetime);
    }
    
    void Update()
    {
        // Move the laser upward
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if we hit an enemy
        if (other.CompareTag("Enemy"))
        {
            // Try to get the enemy's health component
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            
            if (enemyHealth != null)
            {
                // Damage the enemy
                enemyHealth.TakeDamage(damage);
            }
            
            // Destroy the laser
            Destroy(gameObject);
        }
    }
}
