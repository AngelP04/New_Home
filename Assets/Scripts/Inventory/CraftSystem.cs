using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using static Unity.VisualScripting.StickyNote;
using System.Drawing;
using Color = UnityEngine.Color;
using static UnityEditor.Progress;

public class CraftSystem : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] TextMeshProUGUI itemMaterials;
    [SerializeField] Image itemImage;
    public Transform menu;
    public CraftedItems[] itemsCrafteables;
    private int separation;
    private CraftedItems itemSelected;
    private bool canCraft = false;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < itemsCrafteables.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab, menu);
            button.transform.position = new Vector3(button.transform.position.x, button.transform.position.y + separation, 0);
            button.transform.SetParent(menu, false);
            int index = i;
            button.GetComponentInChildren<TextMeshProUGUI>().text = itemsCrafteables[i].name;
            button.GetComponent<Button>().onClick.AddListener(() => { SetItem(itemsCrafteables[index]); });
            button.GetComponentInChildren<Image>().sprite = itemsCrafteables[i].itemCraft.image;
            separation -= 50;
        }
    }

    public void SetItem(CraftedItems item)
    {
        itemSelected = item;
        StartCoroutine(updateText(itemSelected));
    }

    public void Craft()
    {
        if(itemSelected != null)
        {
            if(canCraft)
            {
                InventoryManager.instance.RemoveItems(itemSelected.materials, itemSelected.cantidades);
                InventoryManager.instance.AddItem(itemSelected.itemCraft);
                StartCoroutine(updateText(itemSelected));
            }
        }
    }

    private IEnumerator updateText(CraftedItems item)
    {
        itemMaterials.text = "";
        string color = "";
        string colorEnd = "";
        for (int i = 0; i < item.materials.Length; i++)
        {
            Item itemToSearch = item.materials[i];
            int cantidadNecesaria = item.cantidades[i];
            canCraft = false;
            if (InventoryManager.instance.SearchItem(itemToSearch, cantidadNecesaria) != null)
            {
                itemMaterials.color = Color.white;
                itemMaterials.text += itemToSearch.name + " x" + InventoryManager.instance.SearchItem(itemToSearch, cantidadNecesaria).count.ToString() + " " + "/" + cantidadNecesaria.ToString() + "\n";
                canCraft = true;
            }
            else
            {
                color = "<color=red>";
                colorEnd = "</color>";
                itemMaterials.text += color;
                if (InventoryManager.instance.SearchItemWithoutCount(itemToSearch) != null)
                {
                    itemMaterials.text += itemToSearch.name + " x" + InventoryManager.instance.SearchItemWithoutCount(itemToSearch).count.ToString() + " " + "/" + cantidadNecesaria.ToString() + "\n";
                }
                else
                {
                    itemMaterials.text += itemToSearch.name + " x" + "0" + " " + "/" + cantidadNecesaria.ToString() + "\n";
                }
                itemMaterials.text += colorEnd;
            }
        }
        yield return new WaitForEndOfFrame();
    }
}
