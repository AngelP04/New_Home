using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(menuName = "Scriptable Build")]
public class Estructura : ScriptableObject
{
    [Header("Gameplay")]
    public Vector3 position;
    public int[] cantidades;
    public Item[] materials;

    [Header("Both")]
    public Sprite image;
    public int id;
}
