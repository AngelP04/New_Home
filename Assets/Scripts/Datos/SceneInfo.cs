using Cultive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "SceneInfo", menuName = "Persistence")]
public class SceneInfo : ScriptableObject
{
    public bool isNextScene;
    public bool buttonPressed;
    public Dictionary<Vector3Int, Model> seeds = new();
    public Dictionary<int, DatosJuego> datosEscenas = new();
    public List<TileInfo> tiles = new();
    public float timePassed;
    public int hora, minuto;
}
