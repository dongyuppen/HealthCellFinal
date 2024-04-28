using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    // Damage inflicted by the projectile
    public int damage = 15;
    // Movement speed of the projectile
    public Vector2 moveSpeed = new Vector2(7f, 0);
    // Knockback force applied to the target
    public Vector2 knockback = new Vector2(2, 0);

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // If you want the projectile to be effected by gravity by default, make it dynamic mode rigidbody
        // Set the initial velocity of the projectile
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has a Damageable component
        EnemyDamageable damageable = collision.GetComponent<EnemyDamageable>();

        if (damageable != null)
        {
            // If parent is facing left by local scale, flip the knockback x value to face left as well
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            // Hit the target with damage and knockback
            bool gotHit = damageable.Hit(damage, deliveredKnockback);

            // If the target is hit successfully, log the event and destroy the projectile
            if (gotHit)
            {
                Debug.Log(collision.name + " Hit for " + damage);
                Destroy(gameObject);
            }
        }
    }
}
