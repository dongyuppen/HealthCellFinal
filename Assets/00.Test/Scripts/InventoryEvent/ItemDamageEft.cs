using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Consumable/AttackDamage")]
public class ItemDamageEft : ItemEffect
{
    // Variable to define the amount of attackIncreasePower this item effect provides
    public int attackIncreasePower = 0;

    // Override method from the base class ItemEffect. Executes the role of this item effect.
    public override bool ExecuteRole()
    {
        Damageable playerDamageable = FindObjectOfType<Damageable>();
        if (playerDamageable != null)
        {
            playerDamageable.IncreasePlayerAttackDamage(attackIncreasePower);
            Debug.Log("PlayerAttackDamage increased by: " + attackIncreasePower);
            return true;
        }
        else
        {
            Debug.LogWarning("Player Damageable component not found!");
            return false;
        }
    }
}
