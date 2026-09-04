using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using static UnityEditor.Progress;

public class BuildMenu : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] TextMeshProUGUI buildDescription , buildMaterials;
    [SerializeField] Image buildImage;
    public Transform menu;
    public BuildedEstructura[] estructuras;
    private int separation;
    private Estructura estructuraSelected;
    private bool CanBuild = false;

    private void Start()
    {
        for (int i = 0; i < estructuras.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab, menu);
            button.transform.position = new Vector3 (button.transform.position.x, button.transform.position.y + separation, 0);
            button.transform.SetParent(menu, false);
            int index = i;
            button.GetComponentInChildren<TextMeshProUGUI>().text = estructuras[i].name;
            button.GetComponent<Button>().onClick.AddListener(() => { SetBuild(estructuras[index]); });
            separation -= 50;
        }
    }

    void SetBuild(BuildedEstructura estructura)
    {
        buildMaterials.text = "";
        string color = "";
        string colorEnd = "";
        buildDescription.text = estructura.description;
        for(int i = 0; i < estructura.estructura.materials.Length; i++)
        {
            color = "";
            colorEnd = "";
            Item item = estructura.estructura.materials[i];
            int actuallCantidad = estructura.estructura.cantidades[i];
            CanBuild = false;
            if (InventoryManager.instance.SearchItem(item, actuallCantidad) != null)
            {
                buildMaterials.color = Color.white;
                CanBuild = true;
                buildMaterials.text += item.name + " x" + InventoryManager.instance.SearchItem(item, actuallCantidad).count.ToString() + " " + "/" + actuallCantidad.ToString() + "\n";
            }
            else
            {
                color = "<color=red>";
                colorEnd = "</color>";
                buildMaterials.text += color;
                if(InventoryManager.instance.SearchItemWithoutCount(item)  != null)
                {
                    buildMaterials.text += item.name + " x" + InventoryManager.instance.SearchItemWithoutCount(item).count.ToString() + " " + "/" + actuallCantidad.ToString() + "\n";
                }
                else
                {
                    buildMaterials.text += item.name + " x" + "0" + " " + "/" + actuallCantidad.ToString() + "\n";
                }
                buildMaterials.text += colorEnd;
            }
        }
        buildImage.gameObject.SetActive(true);
        buildImage.sprite = estructura.estructura.image;
        estructuraSelected = estructura.estructura;
    }

    public void Build()
    {
        if(estructuraSelected != null)
        {
            foreach (BuildedEstructura prefab in estructuras)
            {
                if (prefab.id == estructuraSelected.id)
                {
                    if(CanBuild)
                    {
                        Instantiate(prefab, prefab.estructura.position, Quaternion.Euler(Vector3.zero));
                        InventoryManager.instance.RemoveItems(estructuraSelected.materials, estructuraSelected.cantidades);
                        return;
                    }
                }
            }
        }

    }

    public void Desactive()
    {
        MenuManager.Instance.Desactive(this.gameObject);
    }


}
