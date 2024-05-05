using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;

    // Method to set the item's properties
    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;
        item.itemCost = _item.itemCost;
        item.efts = _item.efts;

        image.sprite = item.itemImage;
    }

    // Method to retrieve the item
    public Item GetItem()
    {
        return item;
    }
    
    // Method to destroy the item GameObject
    public void DestoryItem()
    {
        Destroy(gameObject);
    }
}
