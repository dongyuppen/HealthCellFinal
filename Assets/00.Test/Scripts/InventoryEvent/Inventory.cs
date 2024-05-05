using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    // Singleton instance for Inventory
    public static Inventory instance;
    private void Awake()
    {
        // Ensuring only one instance of Inventory exists
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    // Delegates for events when slot count changes and when items change
    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    // List to hold items in the inventory
    public List<Item> items = new List<Item>();

    private int slotCnt; // Variable to hold the number of slots in the inventory

    // Property for slot count, invoking the onSlotCountChange event when set
    public int SlotCnt
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }

    // Initializing the inventory with 4 slots when the game starts
    private void Start()
    {
        SlotCnt = 4;
    }

    // Method to add an item to the inventory
    public bool AddItem(Item _item)
    {
        // Checking if there is space in the inventory
        if (items.Count < SlotCnt)
        {
            items.Add(_item); // Adding the item
            // Invoking onChangeItem event if there are subscribers
            if (onChangeItem != null)
            {
                onChangeItem.Invoke();
                return true;
            }
        }
        return false;
    }

    // Method to remove an item from the inventory by index
    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);
        onChangeItem.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checking if the collision is with a GameObject tagged as "FieldItem"
        if (collision.CompareTag("FieldItem"))
        {
            // Getting the FieldItems component from the collided GameObject
            FieldItems fieldItems = collision.GetComponent<FieldItems>();
            // Adding the item to the inventory if there is space
            if (AddItem(fieldItems.GetItem()))
            {
                fieldItems.DestoryItem();
            }
        }
    }
}
