using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuSeleccionPersonaje : MonoBehaviour
{
    private int index;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nombre;
    private CharactersManager characterManager;

    private void Start()
    {
        characterManager = CharactersManager.instance;

        index = PlayerPrefs.GetInt("PlayerIndex");

        if(index > characterManager.personajes.Count - 1)
        {
            index = 0;
        }

        CambiarPantalla();
    }

    private void CambiarPantalla()
    {
        PlayerPrefs.SetInt("PlayerIndex", index);
        image.sprite = characterManager.personajes[index].imagen;
        nombre.text = characterManager.personajes[index].nombre;
    }

    public void SiguientePersonaje()
    {
        if(index == characterManager.personajes.Count - 1)
        {
            index = 0;
        }
        else
        {
            index += 1;
        }
        CambiarPantalla();
    }

    public void AnteriorPersonaje()
    {
        if (index == 0)
        {
            index = characterManager.personajes.Count - 1;
        }
        else
        {
            index -= 1;
        }
        CambiarPantalla();
    }

    public void IniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
