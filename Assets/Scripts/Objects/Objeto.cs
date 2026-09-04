using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static UnityEditor.Progress;

public class Objeto : PersistenceObject
{

    public Dictionary<InventorySlot, Item> items = new Dictionary<InventorySlot, Item>();
    private Dictionary<InventorySlot, InventoryItem> inventoryItems = new();
    public bool IsOpen;
    public string menu;
    public bool haveInventory;
    public InventorySlot[] slots;
    private GameObject menusObjs;
    public ObjectInventoryManager inventoryManager;
    public List<ItemData> datas = new();
    public Dictionary<InventorySlot, int> cantidades = new();
    private Animator animator;


    private void Awake()
    {
        inventoryManager = FindObjectOfType<ObjectInventoryManager>();
        menusObjs = GameObject.Find("ObjetosMenu");
        slots = menusObjs.transform.Find(menu).transform.Find("Inventory").transform.GetComponentsInChildren<InventorySlot>();
        animator = GetComponent<Animator>();
    }

    public override void FromData(ObjectData data)
    {
        id = data.id;
        haveInventory = data.haveInventory;
        transform.position = new Vector2(data.position[0], data.position[1]);
        foreach(ItemData itemData in data.itemDatas)
        {
            items[slots[itemData.slot]] = Resources.Load(itemData.itemPath) as Item;
            cantidades[slots[itemData.slot]] = itemData.count;
        }
    }

    public override ObjectData ToData()
    {
        float[] position = new float[2];
        position[0] = transform.position.x;
        position[1] = transform.position.y;
        SaveData();
        return new ObjectData
        {
            id = id,
            haveInventory = haveInventory,
            position = position,
            itemDatas = datas

        };
    }

    public void SaveData()
    {
        datas.Clear();
        cantidades.Clear();
        foreach(InventorySlot slot in inventoryItems.Keys)
        {
            datas.Add(inventoryItems[slot].SaveData());
            cantidades.Add(slot, inventoryItems[slot].count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsOpen)
        {
            animator.SetBool("Open", true);
            Item item;
            for (int i = 0; i < slots.Length; i++)
            {
                InventorySlot slot = slots[i];
                InventoryItem item_in_slot = slot.GetComponentInChildren<InventoryItem>();
                if (!items.TryGetValue(slot, out item))
                {
                    if(item_in_slot != null)
                    {
                        items.Add(slot, item_in_slot.item);
                        item_in_slot.slot = i;
                        inventoryItems.Add(slot, item_in_slot);
                        cantidades.Add(slot, inventoryItems[slot].count);
                    }

                }
                if(item_in_slot == null)
                {
                    if(items.ContainsKey(slot))
                    {
                        inventoryItems.Remove(slot);
                        items.Remove(slot);
                        cantidades.Remove(slot);
                    }
                }
            }
        }
        else
        {
            animator.SetBool("Open", false);
        }
        DoSomething();
    }

    public virtual void DoSomething()
    {

    }
}
