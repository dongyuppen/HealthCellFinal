using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    // Reference to the point from where the projectile will be launched
    public Transform launchPoint;
    // Reference to the projectile prefab to be instantiated
    public GameObject projectilePrefab;

    // Method to fire the projectile
    public void FireProjectile()
    {
        // Instantiate a new projectile at the launch point
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);
        // Get the original scale of the projectile
        Vector3 origScale = projectile.transform.localScale;

        // Flip the projectile's facing direction and movement based on the direction the character is facing at the time of launch
        projectile.transform.localScale = new Vector3(
            origScale.x * transform.localScale.x > 0 ? 1 : -1, // Flip along x-axis if the character's scale is positive
            origScale.y, // Retain original scale along y-axis
            origScale.z  // Retain original scale along z-axis
            );
    }
}
