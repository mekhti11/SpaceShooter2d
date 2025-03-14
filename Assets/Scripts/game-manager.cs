using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public Text scoreText;
    public Text highScoreText;
    public Text livesText;
    public GameObject gameOverPanel;
    
    [Header("Player Settings")]
    public int startingLives = 3;
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;
    
    private int score = 0;
    private int highScore = 0;
    private int lives;
    private bool isGameOver = false;
    
    void Start()
    {
        // Hide game over panel
        gameOverPanel.SetActive(false);
        
        // Load high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        
        // Reset score
        score = 0;
        
        // Set starting lives
        lives = startingLives;
        
        // Update UI
        UpdateUI();
        
        // Spawn player
        SpawnPlayer();
    }
    
    void UpdateUI()
    {
        // Update score text
        scoreText.text = "Score: " + score;
        
        // Update high score text
        highScoreText.text = "High Score: " + highScore;
        
        // Update lives text
        livesText.text = "Lives: " + lives;
    }
    
    public void AddScore(int points)
    {
        // Add to score
        score += points;
        
        // Check for high score
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        
        // Update UI
        UpdateUI();
    }
    
    public void PlayerDied()
    {
        // Reduce lives
        lives--;
        
        // Update UI
        UpdateUI();
        
        // Check if game over
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            // Respawn player after delay
            Invoke("SpawnPlayer", 2f);
        }
    }
    
    void SpawnPlayer()
    {
        if (!isGameOver)
        {
            // Spawn player at spawn point
            Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        }
    }
    
    void GameOver()
    {
        isGameOver = true;
        
        // Stop enemy spawning
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.StopSpawning();
        }
        
        // Show game over panel
        gameOverPanel.SetActive(true);
    }
    
    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void QuitGame()
    {
        // In editor, this does nothing. In build, it quits the application
        Application.Quit();
        
        // This line below helps in the Unity Editor to know the button is working
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
