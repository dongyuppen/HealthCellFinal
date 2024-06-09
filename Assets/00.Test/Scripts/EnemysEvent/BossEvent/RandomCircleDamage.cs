using Com.LuisPedroFonseca.ProCamera2D.TopDownShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCircleDamage : MonoBehaviour
{
    public GameObject player;
    public GameObject circlePrefab;
    public float circleRadius = 1.0f;
    public int damage = 10;
    Vector2 knockback = Vector2.zero;
    public float spawnRadius = 5.0f;
    public float bombLifetime = 3.0f; // Time until the bomb explodes

    private Coroutine bombCoroutine;
    private List<GameObject> activeBombs = new List<GameObject>();


    // Start BombSpawning Coroutine
    public void StartBombSpawning()
    {
        if (bombCoroutine == null)
        {
            bombCoroutine = StartCoroutine(SpawnRandomBomb());
        }
    }

    // Stop BombSpawning Coroutine
    public void StopBombSpawning()
    {
        if (bombCoroutine != null)
        {
            StopCoroutine(bombCoroutine);
            bombCoroutine = null;
        }
        ClearAllBombs();
    }

    // Destroy All Bombs
    private void ClearAllBombs()
    {
        foreach (GameObject bomb in activeBombs)
        {
            if (bomb != null)
            {
                Destroy(bomb);
            }
        }
        activeBombs.Clear();
    }

    IEnumerator SpawnRandomBomb()
    {
        while (true)
        {
            // Generate random coordinates around player location
            Vector3 randomPosition = player.transform.position + (Vector3)(Random.insideUnitCircle * spawnRadius);

            // Create a circle (bomb)
            GameObject spawnedCircle = Instantiate(circlePrefab, randomPosition, Quaternion.identity);
            activeBombs.Add(spawnedCircle);

            // Wait until the bomb explodes
            yield return new WaitForSeconds(bombLifetime);
            // Bomb explosion and damage applied
            CheckPlayerInCircle(spawnedCircle);

            if (spawnedCircle != null)
            {
                Destroy(spawnedCircle);
                activeBombs.Remove(spawnedCircle);
            }
        }
    }

    public void CheckPlayerInCircle(GameObject circle)
    {
        // Calculate the distance between the player and the circle
        float distance = Vector3.Distance(player.transform.position, circle.transform.position);

        // Check if player is inside circle
        if (distance <= circleRadius)
        {
            ApplyDamage();
        }
    }

    public void ApplyDamage()
    {
        Damageable damageable = player.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.Hit(damage, knockback);
        }
    }
}
