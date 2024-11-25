using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyHitbox : MonoBehaviour
{
    // Damage inflicted by the attack
    public int damage = 10;
    // Knockback force applied to the target
    public Vector2 knockback = Vector2.zero;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object can be damaged
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            // If parent is facing the left by localscale, our knockback x flips its value to face the left as well
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            // Inflict damage and apply knockback to the target
            bool gotHit = damageable.Hit(damage, deliveredKnockback);

            // Log the hit if successful
            if (gotHit)
            {
                Debug.Log(collision.name + " Hit for " + damage);
            }
        }
    }
}
