using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class ObjectInventoryManager : MonoBehaviour, Iinventory
{

    public GameObject objMenus;
    private ObjectManager objectManager;
    UnityEvent myevent;
    UnityEvent<Item, InventorySlot> addItem;
    private bool updated, cleaned;
    public GameObject inventoryItemPrefab;
    public InventorySlot[] inventorySlots;
    public GameObject InventoryGroup;

    // Start is called before the first frame update
    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        addItem = new UnityEvent<Item, InventorySlot>();
        addItem.AddListener(SpawnItem);
        InventoryGroup = objMenus.transform.Find("Cofre").gameObject;

    }

    public void SpawnItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item, objectManager.cantidades[slot]);
    }

    // Update is called once per frame
    void Update()
    {
        if (objectManager.InMenu)
        {
            InventoryGroup = objMenus.transform.Find(objectManager.menu).gameObject;
            InventoryGroup.SetActive(true);
            if (addItem == null)
            {
                addItem = new UnityEvent<Item, InventorySlot>();
                addItem.AddListener(SpawnItem);
            }
            if(!updated)
            {
                OpenInventory();
            }

        }
        else
        {
            updated = false;
            if (myevent != null)
            {
                myevent.Invoke();
            }
        }
        if(InventoryGroup.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InventoryGroup.SetActive(false);
            }
        }
        else
        {
            if (myevent == null && !cleaned)
            {
                myevent = new UnityEvent();
                myevent.AddListener(ClearInventory);
            }
        }
    }

    public bool AddItem(Item item, InventorySlot slot)
    {
        if(slot.transform.childCount == 0)
        {
            SpawnItem(item, slot);
            return true;
        }
        else
        {
            InventoryItem item_in_slot = slot.GetComponentInChildren<InventoryItem>();
            if(item_in_slot.id == item.id)
            {
                item_in_slot.count++;
                item_in_slot.RefreshCount();
            }
        }
        return false;
    }

    private void OpenInventory()
    {
        if (addItem != null)
        {
            if (objectManager.ItemsInObject.Count > 0)
            {
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (objectManager.ItemsInObject.ContainsKey(inventorySlots[i]))
                    {
                        if (inventorySlots[i].transform.childCount == 0)
                        {
                            addItem.Invoke(objectManager.ItemsInObject[inventorySlots[i]], inventorySlots[i]);
                        }
                    }
                }
            }
        }
        cleaned = false;
        updated = true;
    }

    void ClearInventory()
    {
        if(objectManager.obj == null)
        {
            return;
        }
        for (int i = 0; i < inventorySlots.Length - 1; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem item_in_slot = slot.GetComponentInChildren<InventoryItem>();
            if (item_in_slot != null)
            {
                Destroy(item_in_slot.gameObject);
            }
        }
        cleaned = true;
        myevent = null;
    }
}
