using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int level;

    // Reference to the ScriptableObject containing player's data
    public SOPlayer playerData;

    // Damage inflicted by the attack
    public int attackDamage = 10;

    
    // Knockback force applied to the target
    public Vector2 knockback = Vector2.zero;


    private void Awake()
    {
        InitializeLevelFromPlayerData();
        InitializeAttackDamageFromPlayerData();
        //InitializeAttackdamageFromPlayerData();
    }

    private void InitializeLevelFromPlayerData()
    {
        if (playerData != null)
        {
            level = playerData.level;
        }
        else
        {
            Debug.LogWarning("Player data is not assigned to Attack!");
        }
    }

    // Individual attackDamage Setting
    private void InitializeAttackDamageFromPlayerData()
    {
        if (playerData != null && gameObject.name == "SwordAttack1")
        {
            attackDamage = playerData.attackDamage1;
        }
        else if((playerData != null && gameObject.name == "SwordAttack2"))
        {
            playerData.attackDamage2 = playerData.attackDamage1 + 5;
            attackDamage = playerData.attackDamage2;
        }
        else if ((playerData != null && gameObject.name == "SwordAttack3"))
        {
            playerData.attackDamage3 = playerData.attackDamage1 + 10;
            attackDamage = playerData.attackDamage3;
        }
        else if ((playerData != null && gameObject.name == "AirAttack1"))
        {
            playerData.airAttackDamage1 = playerData.attackDamage1 + 3;
            attackDamage = playerData.airAttackDamage1;
        }
        else if ((playerData != null && gameObject.name == "AirAttack2"))
        {
            playerData.airAttackDamage2 = playerData.attackDamage1 + 5;
            attackDamage = playerData.airAttackDamage2;
        }
        
        else
        {
            Debug.LogWarning("PlayerData is not assigned to Attack!");
        }
    }

    // Integrated attackDamage Setting
    private void InitializeAttackdamageFromPlayerData()
    {
        if (playerData != null && gameObject.name == "SwordAttack1")
        {
            attackDamage = playerData.attackDamage1;
        }
        else if ((playerData != null && gameObject.name == "SwordAttack2"))
        {
            attackDamage = playerData.attackDamage1 + 5;
        }
        else if ((playerData != null && gameObject.name == "SwordAttack3"))
        {
            attackDamage = playerData.attackDamage1 + 10;
        }
        else if ((playerData != null && gameObject.name == "AirAttack1"))
        {
            attackDamage = playerData.attackDamage1 + 3;
        }
        else if ((playerData != null && gameObject.name == "AirAttack2"))
        {
            attackDamage = playerData.attackDamage1 + 5;
        }

        else
        {
            Debug.LogWarning("PlayerData is not assigned to Attack!");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object can be damaged
        EnemyDamageable damageable = collision.GetComponent<EnemyDamageable>();

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

    public void IncreaseAttackDamage(int amount)
    {
        attackDamage += amount;

        GameObject.Find("SwordAttack1").GetComponent<Attack>().attackDamage = attackDamage;
        GameObject.Find("SwordAttack2").GetComponent<Attack>().attackDamage = attackDamage + 5;
        GameObject.Find("SwordAttack3").GetComponent<Attack>().attackDamage = attackDamage + 10;
        GameObject.Find("AirAttack1").GetComponent<Attack>().attackDamage = attackDamage + 3;
        GameObject.Find("AirAttack2").GetComponent<Attack>().attackDamage = attackDamage + 5;
    }
}
