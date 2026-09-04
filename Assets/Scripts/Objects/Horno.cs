using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class Horno : Objeto
{
    public float actual_Load = 0;
    public float speed_charge;
    private bool is_Loading;
    private InventoryItem actualItem;

    public override void DoSomething()
    {
        is_Loading = slots[0].transform.childCount>0;
        if (actual_Load >= 1)
        {
            actualItem.count--;
            actualItem.RefreshCount();
            if (actualItem.count == 0)
            {
                Destroy(actualItem.gameObject);
            }
            inventoryManager.AddItem(actualItem.item.Son, slots[1]);
            actual_Load = 0;
        }
        if(is_Loading)
        {
            if(actualItem == null)
            {
                actualItem = slots[0].GetComponentInChildren<InventoryItem>();
            }

            Charge();
        }
        UpdateProgressBar();
    }

    private void Charge()
    {
        actual_Load += speed_charge * Time.deltaTime;
    }

    private void UpdateProgressBar()
    {
        UIManager.instance.progressBar.size = actual_Load;
    }
}
