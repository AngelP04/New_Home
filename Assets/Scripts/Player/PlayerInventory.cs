using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject InventoryGroup;
    public GameObject CraftGroup;
    public GameObject toolbar;
    public GameObject buttonsMenu, slotEliminar;
    public Transform positionOriginal;
    private PlayerMovement player;
    private ObjectManager objectManager;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        CraftGroup = GameObject.FindGameObjectWithTag("Inventory").transform.Find("CraftSystem").gameObject;
        toolbar = GameObject.FindGameObjectWithTag("Inventory").transform.Find("Toolbar").gameObject;
        InventoryGroup = GameObject.FindGameObjectWithTag("Inventory").transform.Find("MenuInventory").gameObject;
        buttonsMenu = GameObject.FindGameObjectWithTag("Inventory").transform.Find("MenuButtons").gameObject;
        positionOriginal = GameObject.FindGameObjectWithTag("Inventory").transform.Find("InventoryPosition");
    }


    private void Update()
    {
        if(objectManager == null)
        {
            objectManager = FindObjectOfType<ObjectManager>();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            if ((InventoryGroup.activeInHierarchy) || (CraftGroup.activeInHierarchy))
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
        GameManager.instance.inInventory = InventoryGroup.activeInHierarchy;
    }

    public void CloseInventory()
    {
        InventoryGroup.transform.position = positionOriginal.position;
        InventoryGroup.SetActive(false);
        buttonsMenu.SetActive(false);
        CraftGroup.SetActive(false);
        toolbar.SetActive(true);
        player.talking = false;
        slotEliminar.SetActive(false);
    }

    public void OpenInventory()
    {
        if(!InventoryGroup.activeInHierarchy)
        {
            player.talking = true;
            if (objectManager.InMenu)
            {
                var newPosition = InventoryGroup.transform.position;
                newPosition.y -= 100;
                newPosition.x -= 200;
                InventoryGroup.transform.position = newPosition;
            }
            else
            {
                buttonsMenu.SetActive(true);
                slotEliminar.SetActive(true);
            }
            InventoryGroup.SetActive(!InventoryGroup.activeInHierarchy);
            toolbar.SetActive(false);
        }
    }
}
