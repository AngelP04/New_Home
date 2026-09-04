using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    [Header("UI")]
    public Image image;
    public Text countText;
    public TextMeshProUGUI descripcionText;

    [HideInInspector] public Item item;
    [HideInInspector] public int count;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public bool grabbing;

    [Multiline]
    private string description;

    public int id;
    public bool showing_description;
    public string updgrade;
    public bool hasUpgrade;
    public int slot;


    public void InitializeItem(Item newItem, int cantidad = 1)
    {
        item = newItem;
        description = newItem.description;
        descripcionText.text = description;
        image.sprite = newItem.image;
        id = newItem.id;
        count = cantidad;
        hasUpgrade = newItem.hasUpgrade;
        RefreshCount();
    }

    public ItemData SaveData()
    {
        ItemData data = new();
        string origionalPath = AssetDatabase.GetAssetPath(item);
        string itemPath = origionalPath.Remove(origionalPath.Length - 6);
        data.itemPath = itemPath.Remove(0,17);
        data.slot = slot;
        data.count = count;

        /*
        data.id = item.nameID;
        data.name = item.name;
        data.description = description;
        data.hasUses = item.hasUses;
        data.hasUpgrades = hasUpgrade;
        string origionalPath = AssetDatabase.GetAssetPath(image.sprite.texture);
        string spritePath = origionalPath.Remove(origionalPath.Length - 4);
        data.spritePath = spritePath.Remove(0, 17);
        data.actionType = item.actionType;
        data.type = item.type;
        data.count = count;
        data.gameObject = item.gameObject.GetComponent<PersistenceObject>().ToData();
        */
        return data;
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        InventoryManager.instance.updated = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        InventoryManager.instance.updated = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.Find("Canvas").transform.Find("Description_text").gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.Find("Canvas").transform.Find("Description_text").gameObject.SetActive(false);
    }
}
