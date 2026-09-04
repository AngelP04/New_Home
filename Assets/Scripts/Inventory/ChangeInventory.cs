using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeInventory : MonoBehaviour
{
    public GameObject inventory, craft_system;

    public void ChangeSystem()
    {
        craft_system.SetActive(!craft_system.activeInHierarchy);
        inventory.SetActive(!inventory.activeInHierarchy);
    }
}
