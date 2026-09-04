using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{

    public GameObject menuPausa, menuOpciones;
    
    public void Continue()
    {
        menuPausa.SetActive(false);
    }

    public void ChangeGroup()
    {
        menuPausa.SetActive(!menuPausa.activeInHierarchy);
        menuOpciones.SetActive(!menuOpciones.activeInHierarchy);
    }

    public void Exit()
    {
        SceneManager.LoadScene("MenuInicio");
        menuPausa.SetActive(false);
    }
}
