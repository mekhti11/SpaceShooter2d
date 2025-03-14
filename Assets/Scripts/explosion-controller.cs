using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float lifetime = 1f;
    public float expandSpeed = 3f;
    public float fadeSpeed = 2f;
    
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        // Get component
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Start small
        transform.localScale = Vector3.zero;
        
        // Destroy after lifetime
        Destroy(gameObject, lifetime);
    }
    
    void Update()
    {
        // Expand
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, expandSpeed * Time.deltaTime);
        
        // Fade out
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(color.a, 0f, fadeSpeed * Time.deltaTime);
            spriteRenderer.color = color;
        }
    }
}
