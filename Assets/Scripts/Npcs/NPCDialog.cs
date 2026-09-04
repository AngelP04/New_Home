using Articy.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class NPCDialog : MonoBehaviour
{
    public string[] dialog;
    public string[] dialogThanks;
    public string[] dialogNoThanks;
    public string npc_name;
    public GameObject MenuNpc;
    public GameObject PersonalMenu;
    public ArticyRef myRef;
    private bool playerInTheZone;
    private int actualdialog;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            playerInTheZone = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            playerInTheZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            playerInTheZone = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        OnMouseOver();
        if(playerInTheZone && gameObject.GetComponentInParent<NpcMovement>().click) 
        {
            manager.ShowDialog(dialog, npc_name);
            if(gameObject.GetComponentInParent<NpcMovement>() != null)
            {
                gameObject.GetComponentInParent<NpcMovement>().isTalking = true;
            }
        }
        */
        if (playerInTheZone)
        {
            gameObject.GetComponentInParent<NpcMovement>().OnMouseOver();
        }
        else
        {
            gameObject.GetComponentInParent<NpcMovement>().click = false;
        }
        if (playerInTheZone && gameObject.GetComponentInParent<NpcMovement>().click)
        {
            if(!MenuNpc.activeInHierarchy && !PersonalMenu.activeInHierarchy)
            {
                if (gameObject.GetComponentInParent<NpcMovement>() != null)
                {
                    gameObject.GetComponentInParent<NpcMovement>().isTalking = true;
                    gameObject.GetComponentInParent<NpcMovement>().inMenu = true;
                }
                MenuNpc.transform.position = gameObject.GetComponentInParent<Transform>().position;
                MenuManager.Instance.Active(MenuNpc.gameObject);
            }
        }
    }

    public void Talk()
    {
        var availableDialogue = myRef.GetObject();
        DialogManager.instance.StartDialog(availableDialogue);
        DialogManager.instance.ShowDialog(dialog, npc_name);
        MenuNpc.GetComponent<NpcMenu>().Desactivate();
    }

    public void Gift()
    {
        NPCGifts npcGifts = gameObject.GetComponentInParent<NPCGifts>();
        Item gift = InventoryManager.instance.GetSelectedItem(false);
        gameObject.GetComponentInParent<NpcMovement>().inMenu = false;
        gameObject.GetComponentInParent<NpcMovement>().click = false;
        if (gift != null && npcGifts.acceptableGifts.Contains(gift))
        {
            InventoryManager.instance.GetSelectedItem(true);
            DialogManager.instance.ShowDialog(dialogThanks);
        }
        else
        {
            DialogManager.instance.ShowDialog(dialogNoThanks);
        }
        MenuNpc.SetActive(false);
    }

    public void OpenMenu()
    {
        PersonalMenu.transform.position = gameObject.GetComponentInParent<Transform>().position;
        MenuManager.Instance.Desactive(MenuNpc);
        gameObject.GetComponentInParent<NpcMovement>().isTalking = true;
        gameObject.GetComponentInParent<NpcMovement>().inMenu = true;
        gameObject.GetComponentInParent<NpcMovement>().click = false;
        MenuManager.Instance.Active(PersonalMenu);
    }

    public void TalkDialog()
    {
        var availableDialogue = myRef.GetObject();
        string[] dialog = gameObject.GetComponentInParent<NPCGuion>().guion[actualdialog];
        DialogManager.instance.StartDialog(availableDialogue);
        gameObject.GetComponentInParent<NpcMovement>().inMenu = false;
        MenuNpc.GetComponent<NpcMenu>().Desactivate();
        actualdialog++;
        if (actualdialog >= dialog.Length)
        {
            actualdialog = 0;
        }
    }
}
