using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int level;

    // Reference to the ScriptableObject containing monster's data
    public SOMonster monsterData;

    // Damage inflicted by the attack
    public int attackDamage = 10;
    // Knockback force applied to the target
    public Vector2 knockback = Vector2.zero;

    private void Awake()
    {
        InitializeLevelFromMonsterData();
        InitializeAttackDamageFromMonsterData();
    }

    private void InitializeLevelFromMonsterData()
    {
        if (monsterData != null)
        {
            level = monsterData.level;
        }
        else
        {
            Debug.LogWarning("Monster data is not assigned to EnemyAttack!");
        }
    }
    private void InitializeAttackDamageFromMonsterData()
    {
        if (monsterData != null)
        {
            attackDamage = monsterData.attackDamage;
        }
        else
        {
            Debug.LogWarning("Monster data is not assigned to EnemyAttack!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object can be damaged
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            // If parent is facing the left by localscale, our knockback x flips its value to face the left as well
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            // Inflict damage and apply knockback to the target
            bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);

            // Log the hit if successful
            if (gotHit)
            {
                Debug.Log(collision.name + " Hit for " + attackDamage);
            }
        }
    }
}
