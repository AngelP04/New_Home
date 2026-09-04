using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Cultive;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    public GameObject initGroup, optionGroup;
    public void NewGame()
    {
        if(File.Exists(ControladorDatosJuego.instance.archivoGuardado))
        {
            File.Delete(ControladorDatosJuego.instance.archivoGuardado);
        }
        SceneManager.LoadScene("SelectorPersonajes");
    }

    public void LoadGame()
    {
        if (File.Exists(ControladorDatosJuego.instance.archivoGuardado))
        {
            SceneManager.LoadScene(ControladorDatosJuego.instance.datosJuego.escena);
        }
    }

    public void changeGroup()
    {
        initGroup.SetActive(!initGroup.activeInHierarchy);
        optionGroup.SetActive(!optionGroup.activeInHierarchy);
    }
}
