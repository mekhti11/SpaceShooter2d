using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public int scoreValue = 10;
    
    private GameManager gameManager;
    
    void Start()
    {
        // Find the game manager
        gameManager = FindObjectOfType<GameManager>();
        
        // Randomize the starting X position slightly
        float randomOffset = Random.Range(-2f, 2f);
        transform.position = new Vector2(transform.position.x + randomOffset, transform.position.y);
    }
    
    void Update()
    {
        // Move downward
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        
        // If we're below the bottom of the screen, destroy this enemy
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
    
    void OnDestroy()
    {
        // This gets called when the object is destroyed
        // Make sure this isn't called during a scene change
        if (gameManager != null && gameObject.scene.isLoaded && GetComponent<EnemyHealth>().IsDead)
        {
            gameManager.AddScore(scoreValue);
        }
    }
}
