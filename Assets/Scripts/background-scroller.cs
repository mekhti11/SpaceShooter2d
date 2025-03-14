using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    private Material material;
    private Vector2 offset;

    void Start()
    {
        // Get the material from the renderer
        material = GetComponent<Renderer>().material;
        offset = material.mainTextureOffset;
    }

    void Update()
    {
        // Update offset based on time and speed
        offset.y += scrollSpeed * Time.deltaTime;
        
        // Reset offset if it gets too large to prevent floating point issues
        if (offset.y > 1)
        {
            offset.y -= 1;
        }
        
        // Apply the offset to the material
        material.mainTextureOffset = offset;
    }
}