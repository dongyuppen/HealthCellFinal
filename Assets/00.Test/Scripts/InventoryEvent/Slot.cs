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

    public bool isShopMode; // Flag to indicate whether the slot is in shop mode
    public bool isSell = false; // Flag to indicate whether the item is marked for selling
    public GameObject chkSell; // Reference to the UI GameObject indicating the item is marked for selling

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
        if (item != null)
        {
            if (!isShopMode)
            {
                // Using the item when the slot is clicked, if successful, remove the item from the inventory
                bool isUse = item.Use(); // Using the item and storing the result
                if (isUse)
                {
                    Inventory.instance.RemoveItem(slotNum); // Removing the item from the inventory
                }
            }
            else
            {
                // Shop
                isSell = true; // Marking the item for selling
                chkSell.SetActive(isSell); // Activating the UI indicator for selling
            }
        }
    }

    // Method to sell the item
    public void SellItem()
    {
        if(isSell)
        {
            // Adding coins for selling the item
            CoinManager.instance.coins += item.itemCost;
            // Removing the item from the inventory
            Inventory.instance.RemoveItem(slotNum);
            isSell = false; // Resetting the selling flag
            chkSell.SetActive(isSell); // Deactivating the UI indicator for selling
            CoinManager.instance.UpdateCoinsDisplay(); // Update Coin UI
        }
    }

    // Method called when the GameObject is disabled
    private void OnDisable()
    {
        isSell = false; // Resetting the selling flag
        chkSell.SetActive(isSell); // Deactivating the UI indicator for selling
    }
}
