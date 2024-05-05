using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour,IPointerUpHandler
{
    public int slotNum; // Slot number to identify its position in the inventory
    public Item item; // Item held in the slot
    public Image itemIcon; // Reference to the UI image displaying the item icon

    // Method to update the slot UI with the item's icon
    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage; // Setting the item's image to the item icon
        itemIcon.gameObject.SetActive(true); // Activating the item icon
    }

    // Method to remove the item from the slot
    public void RemoveSlot()
    {
        item = null; // Clearing the item reference
        itemIcon.gameObject.SetActive(false); // Deactivating the item icon
    }

    // Method called when the pointer is released over the slot
    public void OnPointerUp(PointerEventData eventData)
    {
        // Using the item when the slot is clicked, if successful, remove the item from the inventory
        bool isUse = item.Use(); // Using the item and storing the result
        if (isUse)
        {
            Inventory.instance.RemoveItem(slotNum); // Removing the item from the inventory
        }
    }
}
