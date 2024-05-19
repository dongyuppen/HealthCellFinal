using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Consumable/AtkSpeed")]
public class ItemAtkSpeedEft : ItemEffect
{
    // Variable to define the amount of atkSpeed points this item effect provides
    public float atkSpeedPoint = 0;

    // Override method from the base class ItemEffect. Executes the role of this item effect.
    public override bool ExecuteRole()
    {
        // Finding an object of type AttackSpeed in the scene
        AttackSpeed attackSpeed = FindObjectOfType<AttackSpeed>();

        // Checking if a AttackSpeed component is found
        if (attackSpeed != null)
        {
            // If found, increasing the atkSpeed by the atkSpeed point value
            attackSpeed.IncreaseAttackSpeed(atkSpeedPoint);
        }

        // Logging the action performed by this item effect
        Debug.Log("PlayerAttackSpeed Add: " + atkSpeedPoint);

        // Returning true indicating the successful execution of the item effect
        return true;
    }
}
