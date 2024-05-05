using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inven; // Reference to the Inventory script

    public GameObject inventoryPanel; // Reference to the GameObject representing the inventory panel
    bool activeInventory = false; // Flag to track if the inventory panel is active or not

    public Slot[] slots; // Array to hold references to all slots in the inventory UI
    public Transform slotHolder; // Reference to the parent transform holding all slots

    private void Start()
    {
        // Getting the Inventory instance
        inven = Inventory.instance;
        // Getting references to all slots
        slots = slotHolder.GetComponentsInChildren<Slot>();
        // Subscribing to events in the Inventory script
        inven.onSlotCountChange += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
        // Setting the initial state of the inventory panel
        inventoryPanel.SetActive(activeInventory);
    }

    // Method to handle slot count change event
    private void SlotChange(int val)
    {
        // Looping through all slots
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNum = i; // Updating slot number

            // Enabling or disabling the slot based on the slot count
            if (i < inven.SlotCnt)
            {
                slots[i].GetComponent<Button>().interactable = true; // Making the slot interactable
            }
            else
            {
                slots[i].GetComponent<Button>().interactable = false; // Disabling the slot
            }
        }
    }

    private void Update()
    {
        // Toggling the inventory panel visibility when 'I' key is pressed
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }

    // Method to add a slot to the inventory
    public void AddSlot()
    {
        inven.SlotCnt++; // Increasing the slot count in the inventory
    }

    // Method to redraw the UI when items change in the inventory
    void RedrawSlotUI()
    {
        // Removing all slots from the UI
        for (int i = 0;i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        // Adding items to the slots and updating their UI
        for (int i = 0; i<inven.items.Count; i++)
        {
            slots[i].item = inven.items[i]; // Assigning the item to the slot
            slots[i].UpdateSlotUI(); // Updating the slot UI
        }
    }
}
