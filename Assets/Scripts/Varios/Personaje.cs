using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Character", menuName = "Personajes")]
public class Personaje : ScriptableObject
{
    public GameObject personaje;
    public Sprite imagen;
    public string nombre;
}
