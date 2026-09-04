using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public PlayerMovement player;

    public static MenuManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    public void Active(GameObject menu)
    {
        menu.SetActive(true);
        player.talking = true;
        menu.gameObject.GetComponentInParent<NpcMovement>().inMenu = true;
    }

    public void Desactive(GameObject menu)
    {
        menu.SetActive(false);
        player.talking = false;
        menu.gameObject.GetComponentInParent<NpcMovement>().inMenu = false;
        menu.gameObject.GetComponentInParent<NpcMovement>().click = false;
    }
}
