using Cultive;
using GameInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultiveManager : MonoBehaviour
{
    private CultiveLayer cultiveLayer;
    private MouseUser mouseUser;
    // Start is called before the first frame update
    void Start()
    {
        cultiveLayer = FindObjectOfType<CultiveLayer>();
        mouseUser = FindObjectOfType<MouseUser>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cultiveLayer == null) return;
        if(Input.GetMouseButtonDown(0))
        {
            if(!cultiveLayer.IsEmpty(mouseUser.MouseInWorldPosition))
            {
                if(cultiveLayer.returnObj(mouseUser.MouseInWorldPosition))
                {
                    Semilla seed = cultiveLayer.returnPlant(mouseUser.MouseInWorldPosition).GetComponent<Semilla>();
                    if(seed.readyToPick)
                    {
                        InventoryManager.instance.AddItem(cultiveLayer.returnObj(mouseUser.MouseInWorldPosition));
                        seed.ispicked = true;
                    }
                }

            }
        }
    }
}
