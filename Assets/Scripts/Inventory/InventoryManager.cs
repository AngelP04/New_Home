using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using static UnityEditor.Progress;

public interface Iinventory
{
    void SpawnItem(Item item, InventorySlot slot);
}

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItem = 4;
    public InventorySlot[] inventorySlots;
    public List<ItemData> itemDatas;
    public GameObject InventoryGroup;
    private GameObject toolbar;
    public bool updated, full;
    public Item pico, azada, cofre, madera, semilla;
    public GameObject inventoryItemPrefab;
    private Transform tools;

    public int selectedSlot = -1;

    public static InventoryManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        tools = GameObject.FindGameObjectWithTag("Player").transform.Find("Tools").transform;
        toolbar = GameObject.Find("Toolbar");
        AddItem(pico);
        AddItem(cofre);
        AddItem(madera);
        /* 
        */
        ChangeSelectedSlot(15);
    }

    public void LoadItemsData()
    {
        foreach(ItemData data in  itemDatas)
        {
            Item newItem = Resources.Load(data.itemPath) as Item;
            SpawNewItem(newItem, inventorySlots[data.slot], data.count);
        }
    }

    public void SaveItemsData()
    {
        itemDatas.Clear();
        for(int i = 0; i < inventorySlots.Length - 6; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot != null)
            {
                ItemData data = itemInSlot.SaveData();
                data.slot = i;
                itemDatas.Add(data);
            }
        }
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            return;
        }
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if(isNumber && number > 0 && number < 6)
            {
                ChangeSelectedSlot(number + 14);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && selectedSlot > 15)
        {
            ChangeSelectedSlot(selectedSlot - 1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && selectedSlot < 19)
        {
            ChangeSelectedSlot(selectedSlot + 1);
        }


        if ((GetSelectedItem(false) == null))
        {
            tools.gameObject.SetActive(false);
        }
        else
        {
            tools.gameObject.SetActive(true);
            if (GetSelectedItem(false).type == ItemType.Tool)
            {
                tools.transform.Find("Objetos").gameObject.SetActive(false);
                tools.transform.Find("Armas").gameObject.SetActive(true);
                tools.transform.Find("Armas").GetComponent<SpriteRenderer>().sprite = GetSelectedItem(false).image;
            }
            else
            {
                tools.transform.Find("Armas").gameObject.SetActive(false);
                tools.transform.Find("Objetos").gameObject.SetActive(true);
                tools.transform.Find("Objetos").GetComponent<SpriteRenderer>().sprite = GetSelectedItem(false).image;
            }
        }
        for(int i=0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].transform.childCount > 0)
            {
                if (inventorySlots[i].transform.GetChild(0).GetComponent<InventoryItem>().count == 0)
                {
                    Destroy(inventorySlots[i].transform.GetChild(0).gameObject);
                    updated = false;

                }
            }
        }

        if(!updated)
        {
            Update_toolbar();
        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public InventoryItem SearchItem(Item item, int cantidad)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem item_in_slot = slot.GetComponentInChildren<InventoryItem>();
            if (item_in_slot != null)
            {             
                if (item_in_slot.id == item.id)
                {
                    if(item_in_slot.count >= cantidad)
                    {
                        return item_in_slot;
                    }
                }
            }
        }
        return null;
    }

    public InventoryItem SearchItemWithoutCount(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem item_in_slot = slot.GetComponentInChildren<InventoryItem>();
            if(item_in_slot != null)
            {
                if (item_in_slot.id == item.id)
                {
                    return item_in_slot;
                }
            }
        }
        return null;
    }

    public bool AddItem(Item item)
    {
        if (full) return false;
        updated = false;
        //Revisar si cualquier slot tiene el mismo item con el contador mas bajo que el maximo
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem item_in_slot = slot.GetComponentInChildren<InventoryItem>();
            if (item_in_slot != null && item_in_slot.item == item && item_in_slot.count < maxStackedItem && item_in_slot.item.stackable == true)
            {
                item_in_slot.count++;
                item_in_slot.RefreshCount();
                full = false;
                return true;
            }
        }
        //Encontrar un slot vacìo
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem item_in_slot = slot.GetComponentInChildren<InventoryItem>();
            if (item_in_slot == null) 
            {
                full = false;
                SpawNewItem(item, slot);
                return true;
            }
        }
        full = true;
        return false;
    }

    public void RemoveItems(Item[] items, int[] catidades)
    {
        StartCoroutine(deleteItems(items, catidades));
    }

    private IEnumerator deleteItems(Item[] items, int[] catidades)
    {
        for (int i = 0; i < items.Length; i++)
        {
            InventoryItem item = SearchItem(items[i], catidades[i]);
            if (item != null)
            {
                item.count -= catidades[i];
                if (item.count <= 0)
                {
                    Destroy(item.gameObject);
                    Debug.Log(true);
                }
                else
                {
                    item.RefreshCount();
                }
            }
        }
        Update_toolbar();
        yield return null;
    }

    public void SpawNewItem(Item item, InventorySlot slot, int count = 1)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        if(count == 0)
        {
            count = 1;
        }
        inventoryItem.InitializeItem(item, count);
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot - 15];
        InventoryItem item_in_slot = slot.GetComponentInChildren<InventoryItem>();
        if(item_in_slot != null)
        {
            Item item = item_in_slot.item;
            if(use)
            {
                if(!item.hasUses)
                {
                    item_in_slot.count--;
                    if (item_in_slot.count <= 0)
                    {
                        Destroy(item_in_slot.gameObject);
                    }
                    else
                    {
                        item_in_slot.RefreshCount();
                    }
                    updated = false;
                }
                GameManager.instance.playerHealthCurrent -= 1;
            }
            return item;
        }
        return null;
    }

    public void DeleteItem(InventorySlot slot)
    {
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if(itemInSlot != null)
        {
            Destroy(itemInSlot.gameObject);
        }
    }


    private void Update_toolbar()
    {
        for (int i = 0; i < toolbar.transform.childCount; i++)
        {
            if (InventoryGroup.transform.Find("Inventory").GetChild(i).childCount > 0)
            {
                if(toolbar.transform.GetChild(i).transform.childCount == 0)
                {
                    Transform slot = InventoryGroup.transform.Find("Inventory").GetChild(i);
                    GameObject item = slot.GetChild(0).gameObject;
                    var clone = Instantiate(item);
                    clone.transform.SetParent(toolbar.transform.GetChild(i));
                }

                if(toolbar.transform.GetChild(i).transform.GetComponentInChildren<InventoryItem>().id == InventoryGroup.transform.Find("Inventory").GetChild(i).transform.GetComponentInChildren<InventoryItem>().id)
                {
                    toolbar.transform.GetChild(i).transform.GetComponentInChildren<InventoryItem>().count = InventoryGroup.transform.Find("Inventory").GetChild(i).transform.GetComponentInChildren<InventoryItem>().count;
                    toolbar.transform.GetChild(i).transform.GetComponentInChildren<InventoryItem>().RefreshCount();

                }
                
            }
            else
            {
                if(toolbar.transform.GetChild(i).childCount > 0)
                {
                    Destroy(toolbar.transform.GetChild(i).GetChild(0).gameObject);
                }

            }
        }
        updated = true;
    }
}
