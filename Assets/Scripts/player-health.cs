using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public float invincibilityTime = 2f;
    public GameObject explosionPrefab;
    public SpriteRenderer spriteRenderer;
    
    private int currentHealth;
    private bool isInvincible = false;
    private GameManager gameManager;
    private float blinkTime = 0.1f;
    
    void Start()
    {
        // Set starting health
        currentHealth = maxHealth;
        
        // Find the game manager
        gameManager = FindObjectOfType<GameManager>();
        
        // If SpriteRenderer wasn't set in inspector, try to get it
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if we hit an enemy or enemy laser
        if ((other.CompareTag("Enemy")) && !isInvincible)
        {
            TakeDamage(1);
        }
    }
    
    public void TakeDamage(int damage)
    {
        if (isInvincible) return;
        
        // Reduce health
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Become invincible for a short time
            StartInvincibility();
        }
    }
    
    void StartInvincibility()
    {
        isInvincible = true;
        
        // Start blinking
        InvokeRepeating("ToggleVisibility", 0f, blinkTime);
        
        // End invincibility after set time
        Invoke("EndInvincibility", invincibilityTime);
    }
    
    void EndInvincibility()
    {
        isInvincible = false;
        
        // Stop blinking
        CancelInvoke("ToggleVisibility");
        
        // Make sure player is visible
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }
    }
    
    void ToggleVisibility()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }
    }
    
    void Die()
    {
        // Create explosion effect
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        
        // Notify game manager
        if (gameManager != null)
        {
            gameManager.PlayerDied();
        }
        
        // Destroy the player
        Destroy(gameObject);
    }
}
