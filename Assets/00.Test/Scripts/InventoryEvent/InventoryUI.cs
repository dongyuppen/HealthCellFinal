using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inven; // Reference to the Inventory script

    public GameObject player;

    public GameObject inventoryPanel; // Reference to the GameObject representing the inventory panel
    bool activeInventory = false; // Flag to track if the inventory panel is active or not

    public Slot[] slots; // Array to hold references to all slots in the inventory UI
    public Transform slotHolder; // Reference to the parent transform holding all slots
    public ShopSlot[] shopSlots; // Array to hold references to all shop slots in the shop UI
    public Transform shopHolder; // Reference to the parent transform holding all shop slots

    public GameObject shop; // Reference to the shop UI GameObject
    public Button closeShop; // Reference to the close button for the shop
    public bool isStoreActive; // Flag to track if the shop is currently active

    public ShopData shopData; // Reference to the data for the current shop

    public float shopOpenDistance = 1f; // Maximum distance to open the shop

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Getting the Inventory instance
        inven = Inventory.instance;
        // Getting references to all slots
        slots = slotHolder.GetComponentsInChildren<Slot>();
        // Getting references to all shop slots
        shopSlots = shopHolder.GetComponentsInChildren<ShopSlot>();

        // Initializing shop slots
        for (int i = 0; i < shopSlots.Length; i++)
        {
            shopSlots[i].Init(this);
            shopSlots[i].slotNum = i;
        }

        // Subscribing to events in the Inventory script
        inven.onSlotCountChange += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
        RedrawSlotUI();
        // Setting the initial state of the inventory panel
        inventoryPanel.SetActive(activeInventory);
        // Adding a listener to the close shop button
        closeShop.onClick.AddListener(DeActiveShop);
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
        if (Input.GetKeyDown(KeyCode.I) && !isStoreActive)
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }

        // Opening the shop when 'P' key is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            TryOpenShop();
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
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        // Adding items to the slots and updating their UI
        for (int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i]; // Assigning the item to the slot
            slots[i].UpdateSlotUI(); // Updating the slot UI
        }
    }

    // Method to attempt opening the shop when near
    void TryOpenShop()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(player.transform.position, shopOpenDistance);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Store"))
            {
                if (!isStoreActive)
                {
                    ActiveShop(true); // Opening the shop UI
                    shopData = hitCollider.GetComponent<ShopData>(); // Getting shop data
                    // Updating shop slots with available stocks
                    for (int i = 0; i < shopData.stocks.Count; i++)
                    {
                        shopSlots[i].item = shopData.stocks[i];
                        shopSlots[i].UpdateSlotUI();
                    }
                    break;
                }
            }
        }
    }

    // Method to handle buying an item from the shop
    public void Buy(int num)
    {
        shopData.soldOuts[num] = true; // Marking the item as sold out in the shop data
    }

    // Method to activate or deactivate the shop UI
    public void ActiveShop(bool isOpen)
    {
        if (!activeInventory)
        {
            isStoreActive = isOpen; // Updating the shop active flag
            shop.SetActive(isOpen); // Activating or deactivating the shop UI
            inventoryPanel.SetActive(isOpen); // Activating or deactivating the inventory panel
            // Setting the shop mode for all slots
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].isShopMode = isOpen;
            }
        }
    }

    // Method to deactivate the shop UI
    public void DeActiveShop()
    {
        ActiveShop(false); // Deactivating the shop
        shopData = null; // Clearing shop data
        // Removing all items from shop slots
        for (int i = 0; i < shopSlots.Length; i++)
        {
            shopSlots[i].RemoveSlot();
        }
    }

    // Method to handle selling all items in the inventory
    public void SellBtn()
    {
        for (int i = slots.Length; i > 0; i--)
        {
            slots[i - 1].SellItem(); // Selling each item in the inventory
        }
    }
}
