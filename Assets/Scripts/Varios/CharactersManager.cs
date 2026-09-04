using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{
    public static CharactersManager instance;
    public List<Personaje> personajes;

    private void Awake()
    {
        if(CharactersManager.instance == null)
        {
            CharactersManager.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
            
    }
}
