using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyType
    {
        public GameObject enemyPrefab;
        public float weight = 1f; // Higher weight = more common
    }
    
    public EnemyType[] enemyTypes;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 3f;
    public float spawnHeight = 6f; // How far above the screen to spawn
    public float minX = -4f;
    public float maxX = 4f;
    
    private bool isSpawning = true;
    
    void Start()
    {
        // Start spawning enemies
        StartCoroutine(SpawnEnemies());
    }
    
    IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            // Wait for random time between min and max spawn time
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            
            // Spawn an enemy
            SpawnEnemy();
        }
    }
    
    void SpawnEnemy()
    {
        // Calculate total weight
        float totalWeight = 0f;
        foreach (EnemyType enemy in enemyTypes)
        {
            totalWeight += enemy.weight;
        }
        
        // Select random enemy based on weight
        float randomValue = Random.Range(0f, totalWeight);
        float weightSum = 0f;
        EnemyType selectedEnemy = enemyTypes[0]; // Default to first enemy
        
        foreach (EnemyType enemy in enemyTypes)
        {
            weightSum += enemy.weight;
            if (randomValue <= weightSum)
            {
                selectedEnemy = enemy;
                break;
            }
        }
        
        // Calculate random position
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPosition = new Vector2(randomX, spawnHeight);
        
        // Spawn the enemy
        Instantiate(selectedEnemy.enemyPrefab, spawnPosition, Quaternion.identity);
    }
    
    public void StopSpawning()
    {
        isSpawning = false;
    }
}
