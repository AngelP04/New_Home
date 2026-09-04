using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcMenu : MonoBehaviour
{

    public string action;
    public TextMeshProUGUI text;
    private PlayerMovement player;
    public GameObject menu_Construccion;
    // Start is called before the first frame update
    void Start()
    {
        text.text = action;
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeInHierarchy)
        {
            player.talking = true;
            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                Desactivate();
                player.talking = false;
            }
        }
    }

    public void Desactivate()
    {
        MenuManager.Instance.Desactive(this.gameObject);
    }
}
