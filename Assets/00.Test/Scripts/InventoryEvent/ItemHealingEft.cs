using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ItemEft/Consumable/Health")]
public class ItemHealingEft : ItemEffect
{
    public int healingPoint = 0;
    public override bool ExecuteRole()
    {
        Damageable damageable = FindObjectOfType<Damageable>();
        if (damageable != null)
        {
            damageable.IncreaseMaxHealth(healingPoint);
        }
        Debug.Log("PlayerMaxHp Add: " + healingPoint);
        return true;

    }

    
}
