using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Enemy prefab
    public Transform player; // Variable to reference the player's position
    public float spawnInterval = 10f; // Spawn interval in seconds
    private bool playerInBossMap = false; // Whether the player is inside the BossMap

    void Start()
    {
        // Start the coroutine
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(spawnInterval);

            // Spawn enemy only if the player is inside the BossMap
            if (playerInBossMap)
            {
                Vector2 spawnPosition = GetSpawnPositionNearPlayer();
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    Vector2 GetSpawnPositionNearPlayer()
    {
        // Logic to determine the spawn position near the player
        float spawnDistance = 3f; // Distance from the player to spawn
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2 spawnPosition = (Vector2)player.position + randomDirection * spawnDistance;

        return spawnPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInBossMap = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInBossMap = false;
        }
    }
}
