using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demo : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void PickUpItem(int id)
    {
        bool result = false;
        if (result)
        {
            Debug.Log("Item Added");
        }
        else
        {
            Debug.Log("Inventory full");
        }
    }

    public void GetSelectedItem()
    {
        Item receiveItem = inventoryManager.GetSelectedItem(false);
        if (receiveItem != null)
        {
            Debug.Log("Receive item: " + receiveItem);
        }
        else
        {
            Debug.Log("No receive item");
        }
    }

    public void UseSelectedItem()
    {
        Item receiveItem = inventoryManager.GetSelectedItem(true);
        if (receiveItem != null)
        {
            Debug.Log("Receive item: " + receiveItem);
        }
        else
        {
            Debug.Log("No receive item");
        }
    }
}
