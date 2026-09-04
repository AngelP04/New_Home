using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] InventorySlot inventorySlot;
    [SerializeField] TextMeshProUGUI descripcionMejora, materialesMejora;
    [SerializeField] Image imagenMejora;
    public Transform menu;
    private int separation;
    private bool updated = false;
    private bool canUpgrade;
    private Upgrades selectedUpgrade;
    private InventoryItem item;

    // Start is called before the first frame update
    void Start()
    {
        separation = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(inventorySlot.transform.childCount > 0)
        {
            item = inventorySlot.transform.GetComponentInChildren<InventoryItem>();
            if (item != null)
            {
                InventoryManager.instance.updated = false;
                if(item.hasUpgrade)
                {
                    if(!updated)
                    {
                        for (int i = 0; i < item.item.upgrades.Length; i++)
                        {
                            GameObject button = Instantiate(buttonPrefab, menu);
                            button.transform.position = new Vector3(button.transform.position.x, button.transform.position.y + separation, 0);
                            button.transform.SetParent(menu, false);
                            int index = i;
                            Debug.Log(item.item.upgrades[i].upgradeName);
                            button.GetComponentInChildren<TextMeshProUGUI>().text = item.item.upgrades[i].upgradeName;
                            button.GetComponent<Button>().onClick.AddListener(() => { SetUpgrade(item.item.upgrades[index]); });
                            separation -= 50;
                        }
                        updated = true;
                    }
                }
                
            }
        }
        /*for (int i = 0; i < 15; i++)
        {
            InventorySlot slot = inventoryManager.inventorySlots[i];
            if (slot.transform.childCount > 0)
            {
                InventoryItem item = slot.GetComponentInChildren<InventoryItem>();
                if (item.hasUpgrade)
                {
                    GameObject button = Instantiate(buttonPrefab, menu);
                    button.transform.position = new Vector3(button.transform.position.x, button.transform.position.y + separation, 0);
                    button.transform.SetParent(menu, false);
                    int index = i;
                    button.GetComponentInChildren<TextMeshProUGUI>().text = item.item.name;
                    separation -= 50;
                }
            }
        }*/
    }

    void SetUpgrade(Upgrades upgrade)
    {
        materialesMejora.text = "";
        descripcionMejora.text = upgrade.description;
        materialesMejora.color = Color.red;
        InventoryItem itemMaterial = InventoryManager.instance.SearchItem(upgrade.material, upgrade.cantidad);
        if(itemMaterial != null)
        {
            materialesMejora.color = Color.white;
            canUpgrade = true;
            materialesMejora.text += upgrade.material.name + " x" + itemMaterial.count.ToString() + " " + "/" + upgrade.cantidad.ToString();
        }
        if(upgrade.image != null)
        {
            imagenMejora.gameObject.SetActive(true);
            imagenMejora.sprite = upgrade.image;
            descripcionMejora.gameObject.SetActive(false);
        }
        else
        {
            imagenMejora.gameObject.SetActive(false);
            descripcionMejora.gameObject.SetActive(true);
            descripcionMejora.text = upgrade.description;
        }
        selectedUpgrade = upgrade;
    }

    public void Upgrade()
    {
        if(selectedUpgrade != null)
        {
            item.updgrade += selectedUpgrade.upgradeName;
            return;
        }
    }
}
