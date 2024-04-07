using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    // Amount of health restored upon pickup
    public int healthRestore = 20;
    // Rotation speed of the pickup object
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has a Damageable component
        Damageable damageable = collision.GetComponent<Damageable>();

        // If the collided object has a Damageable component and its health is not at maximum
        if (damageable && damageable.Health < damageable.MaxHealth)
        {
            // Attempt to heal the damageable object
            bool wasHealed = damageable.Heal(healthRestore);

            // If the healing was successful, destroy the pickup object
            if (wasHealed)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        // Rotate the pickup object based on spinRotationSpeed
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
