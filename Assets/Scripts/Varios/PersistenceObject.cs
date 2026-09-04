using Cultive;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class PersistenceObject: MonoBehaviour
{
    public string id;

    public abstract void FromData(ObjectData data);

    public abstract ObjectData ToData();
}


[System.Serializable]
public class ObjectData
{
    //General
    public string id;
    public float[] position = new float[2];
    //Planta
    public int actualstage;
    public float timeOfGrow, actualTime, timeToDestroy, timeNoWater;
    public bool isPicked, readyToPicked;
    //Objetos Buildables
    public string menu;
    public bool haveInventory;
    public List<ItemData> itemDatas;
    //Animales de granja
    public float timeBetweenPicks, timeLastPick;
    public bool picked;
}

[Serializable]
public class TileInfo
{
    public TileBase tile;
    public Vector3Int position;
    public GameObject gameObject;

    public TileInfo(TileBase tile, Vector3Int position, GameObject gameObject = null)
    {
        this.tile = tile; 
        this.position = position;
        this.gameObject = gameObject;
    }
}