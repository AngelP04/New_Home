using GameInput;
using PlayerBuildingSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance { get; private set; }
    private ConstructionLayer constructionLayer;
    private PlayerInventory playerInventory;
    public Dictionary<InventorySlot, Item> ItemsInObject = new Dictionary<InventorySlot, Item>();
    public Dictionary<InventorySlot, int> cantidades = new();
    public Objeto obj;
    public bool InMenu;
    public string menu;
    [SerializeField] private MouseUser mouseUser;

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

    // Start is called before the first frame update
    void Start()
    {
        constructionLayer = FindObjectOfType<ConstructionLayer>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        mouseUser = FindObjectOfType<MouseUser>();
    }

    // Update is called once per frame
    void Update()
    {
        constructionLayer = FindObjectOfType<ConstructionLayer>();
        if (constructionLayer == null)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if(!constructionLayer.IsEmpty(mouseUser.MouseInWorldPosition))
            {
                obj = constructionLayer.returnBuildable(mouseUser.MouseInWorldPosition).GetComponent<Objeto>();
                InMenu = true;
                obj.IsOpen = true;
                menu = obj.menu;
                if(obj.haveInventory)
                {
                    ItemsInObject = obj.items;
                    cantidades = obj.cantidades;
                    playerInventory.OpenInventory();
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(InMenu)
            {
                obj.IsOpen = false;
                InMenu = false;
            }
        }
    }
}
