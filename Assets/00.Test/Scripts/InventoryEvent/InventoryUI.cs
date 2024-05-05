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
    public ShopSlot[] shopSlots;
    public Transform shopHolder;

    private void Start()
    {
        // Getting the Inventory instance
        inven = Inventory.instance;
        // Getting references to all slots
        slots = slotHolder.GetComponentsInChildren<Slot>();
        shopSlots = shopHolder.GetComponentsInChildren<ShopSlot>();
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

        if (Input.GetMouseButtonUp(0))
        {
            RayShop();
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

    public GameObject shop;
    public Button closeShop;
    public bool isStoreActive;

    public ShopData shopData;
    public void RayShop()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -10;
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1)) // Mobile = 0
        {
            RaycastHit2D hit2D = Physics2D.Raycast(mousePos, transform.forward, 30);
            if (hit2D.collider != null)
            {
                if (hit2D.collider.CompareTag("Store"))
                {
                    if (!isStoreActive)
                    {
                        ActiveShop(true);
                        shopData = hit2D.collider.GetComponent<ShopData>();
                        for (int i = 0; i < shopData.stocks.Count; i++)
                        {
                            shopSlots[i].item = shopData.stocks[i];
                            shopSlots[i].UpdateSlotUI();
                        }
                    }
                }
            }
        }
    }

    public void Buy(int num)
    {
        shopData.soldOuts[num] = true;
    }

    public void ActiveShop(bool isOpen)
    {
        if (!activeInventory)
        {
            isStoreActive = isOpen;
            shop.SetActive(isOpen);
            inventoryPanel.SetActive(isOpen);
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].isShopMode = isOpen;
            }
        }
    }
    public void DeActiveShop()
    {
        ActiveShop(false);
        shopData = null;
        for (int i = 0; i < shopSlots.Length; i++)
        {
            shopSlots[i].RemoveSlot();
        }
    }

    public void SellBtn()
    {
        for (int i = slots.Length; i > 0; i--)
        {
            slots[i - 1].SellItem();
        }
    }
}
