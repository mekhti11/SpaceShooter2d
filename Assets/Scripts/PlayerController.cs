using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float boundaryPadding = 0.5f; // Keeps player from moving off-screen
    
    [Header("Shooting Settings")]
    public GameObject laserPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;
    
    private Camera mainCamera;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        // Get camera reference and calculate screen boundaries
        mainCamera = Camera.main;
        CalculateScreenBoundaries();
    }

    void Update()
    {
        // Handle movement
        HandleMovement();
        
        // Handle shooting
        HandleShooting();
    }

    void CalculateScreenBoundaries()
    {
        // Convert screen boundaries to world coordinates
        Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        
        // Set boundaries with padding
        minX = bottomLeft.x + boundaryPadding;
        maxX = topRight.x - boundaryPadding;
        minY = bottomLeft.y + boundaryPadding;
        maxY = topRight.y - boundaryPadding;
    }

    void HandleMovement()
    {
        // Get input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        // Calculate movement
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.deltaTime;
        
        // Apply movement
        Vector2 newPosition = (Vector2)transform.position + movement;
        
        // Clamp position to screen boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        
        // Update position
        transform.position = newPosition;
    }

    void HandleShooting()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            // Set next fire time
            nextFireTime = Time.time + fireRate;
            
            // Create laser
            Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
        }
    }
}