using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour,IPointerUpHandler
{
    public int slotNum; // Slot number to identify its position in the inventory
    public Item item; // Item held in the slot
    public Image itemIcon; // Reference to the UI image displaying the item icon
    public bool soldOut = false;
    InventoryUI inventoryUI;

    public void Init(InventoryUI Iui)
    {
        inventoryUI = Iui;
    }
   
    // Method to update the slot UI with the item's icon
    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage; // Setting the item's image to the item icon
        itemIcon.gameObject.SetActive(true); // Activating the item icon
        if (soldOut)
        {
            itemIcon.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

    // Method to remove the item from the slot
    public void RemoveSlot()
    {
        item = null; // Clearing the item reference
        soldOut = false;
        itemIcon.gameObject.SetActive(false); // Deactivating the item icon
    }

    // Method called when the pointer is released over the slot
    public void OnPointerUp(PointerEventData eventData)
    {
        if (item != null)
        {
            if (CoinManager.instance.coins >= item.itemCost && !soldOut && Inventory.instance.items.Count < Inventory.instance.SlotCnt)
            {
                CoinManager.instance.coins -= item.itemCost;
                Inventory.instance.AddItem(item);
                soldOut = true;
                inventoryUI.Buy(slotNum);
                UpdateSlotUI();

                CoinManager.instance.UpdateCoinsDisplay(); // Update Coin UI
            }
        }
    }
}