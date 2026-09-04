using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicioJugador : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        int indexJugador = PlayerPrefs.GetInt("PlayerIndex");
        Instantiate(CharactersManager.instance.personajes[indexJugador].personaje, transform.position, Quaternion.identity);
    }
}
