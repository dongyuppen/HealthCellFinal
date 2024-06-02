using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRectangleDamage : MonoBehaviour
{
    public GameObject player;
    public GameObject square; // Red square sprite object
    public float activationTime = 5f; // Time taken to activate Square
    public float squareDuration = 5f; // Duration for which Square remains active
    public int damage = 30; // Amount of damage to inflict on the player
    Vector2 knockback = Vector2.zero;

    private bool playerInRange = false;
    private bool timerRunning = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (!timerRunning)
            {
                StartCoroutine(ActivateSquareTimer());
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    IEnumerator ActivateSquareTimer()
    {
        while (true)
        {
            timerRunning = true;
            float timer = activationTime;
            while (timer > 0)
            {
                yield return new WaitForSeconds(1f);
                if (playerInRange)
                {
                    timer -= 1f;
                }
            }
            square.SetActive(true);
            yield return new WaitForSeconds(squareDuration);
            Explosion();
        }
    }

    void Explosion()
    {
        // Inflict damage on the player
        if (playerInRange)
        {
            ApplyDamage();
        }

        // Explode Square
        square.SetActive(false);
        // Additional explosion animation or effects can be played here.
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
