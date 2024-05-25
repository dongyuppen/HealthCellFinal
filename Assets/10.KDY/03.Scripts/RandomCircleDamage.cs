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
    public float checkInterval = 0.5f;

    private GameObject spawnedCircle;
    private Vector3 randomPosition;

    private void Start()
    {
        InvokeRepeating("SpawnRandomCircle", 0, checkInterval);
    }

    public void SpawnRandomCircle()
    {
        // Create Random Coordinate
        randomPosition = player.transform.position + (Vector3)(Random.insideUnitCircle * spawnRadius);

        // Create Circle
        if (spawnedCircle != null)
        {
            Destroy(spawnedCircle);
        }
        spawnedCircle = Instantiate(circlePrefab, randomPosition, Quaternion.identity);
    }

    private void Update()
    {
        if (spawnedCircle != null)
        {
            CheckPlayerInCircle();
        }
    }

    public void CheckPlayerInCircle()
    {
        // Calculate the distance between the player and the circle
        float distance = Vector3.Distance(player.transform.position, spawnedCircle.transform.position);

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
