using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ItemEft/Consumable/Health")]
public class ItemHealingEft : ItemEffect
{
    // Variable to define the amount of healing points this item effect provides
    public int healingPoint = 0;

    // Override method from the base class ItemEffect. Executes the role of this item effect.
    public override bool ExecuteRole()
    {
        // Finding an object of type Damageable in the scene
        Damageable damageable = FindObjectOfType<Damageable>();

        // Checking if a Damageable component is found
        if (damageable != null)
        {
            // If found, increasing the maximum health by the healing point value
            damageable.IncreaseMaxHealth(healingPoint);
        }

        // Logging the action performed by this item effect
        Debug.Log("PlayerMaxHp Add: " + healingPoint);

        // Returning true indicating the successful execution of the item effect
        return true;
    }
}
