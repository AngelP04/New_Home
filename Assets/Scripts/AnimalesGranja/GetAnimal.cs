using GameInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAnimal : MonoBehaviour
{

    private AnimalLayer animalLayer;
    [SerializeField] private MouseUser mouseUser;

    // Update is called once per frame
    void Update()
    {
        if(animalLayer == null)
        {
            if(FindObjectOfType<AnimalLayer>() == null)
            {
                return;
            }
            else
            {
                animalLayer = FindObjectOfType<AnimalLayer>();
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(!animalLayer.IsEmpty(mouseUser.MouseInWorldPosition))
                {
                    InventoryManager.instance.AddItem(animalLayer.returnObj(mouseUser.MouseInWorldPosition));
                }
            }
        }
    }
}
