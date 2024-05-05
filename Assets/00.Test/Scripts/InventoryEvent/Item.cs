using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enumeration to define different types of items
public enum ItemType
{
    Equipmnent,
    Consumables,
    Etc
}

// Serializable class representing an item
[System.Serializable]
public class Item
{
    public ItemType itemType; // Type of the item
    public string itemName; // Name of the item
    public int itemCost; // Cost of the item
    public Sprite itemImage; // Image of the item
    public List<ItemEffect> efts; // List of effects associated with the item

    // Method to use the item
    public bool Use()
    {
        bool isUsed = false; // Flag to track if the item is used

        // Looping through each effect associated with the item
        foreach (ItemEffect eft in efts)
        {
            // Executing the effect and updating the isUsed flag
            isUsed = eft.ExecuteRole();
        }

        // Returning whether the item is used or not
        return isUsed;
    }
}
