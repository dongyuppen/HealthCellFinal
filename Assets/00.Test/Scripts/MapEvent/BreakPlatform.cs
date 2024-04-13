using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakPlatform : MonoBehaviour
{
    // Platform broken or not.
    private bool isBroken = false;

    // Platform regeneration time.
    private float respawnTime = 5f;

    // OnCollisionEnter2D Verify that the player has stepped on the Platform in response to the event.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Make sure you step on the top of the platform.
        if (collision.contacts[0].normal.y < 0.5f)
        {
            // If you step on it, set isBroken to true.
            isBroken = true;

            // It will be broken in one second.
            Invoke("Breakplatform", 1f);
        }
    }

    // The Breakplatform function breaks platform.
    private void Breakplatform()
    {
        // Off platform.
        gameObject.SetActive(false);

        // Respawn the platform.
        Invoke("Respawnplatform", respawnTime);
    }

    // Respawn the platform.
    private void Respawnplatform()
    {
        gameObject.SetActive(true);

        isBroken = false;
    }
}
