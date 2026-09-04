using System;
using System.Collections;
using System.Collections.Generic;
using TimeSystem;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class DatosJuego
{
    public Vector2 position;

    public int escena;

    public int vida;

    public SerializableDateTime dateTime = new();

    public List<ObjectData> objsDataSaved = new();

    public List<Model> modelsSaved = new();

    public List<Model> seedsSaved = new();

    public List<TileInfo> tileInfoSaved = new();

    public List<ItemData> itemDatas = new();

    public int hora, minutos;
}
