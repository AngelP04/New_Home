using Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class Model
{
    [field: SerializeField] public Tilemap ParentTilemap { get;  set; }
    [field: SerializeField] public Vector3Int Coordinates { get;  set; }

    [field: SerializeField] public GameObject gameObject { get; set; }


    [field: SerializeField] public Item itemType { get; set; }

    public Model(Item type, Vector3Int coords, Tilemap tilemap, GameObject obj = null)
    {
        ParentTilemap = tilemap;
        itemType = type;
        Coordinates = coords;
        gameObject = obj;
    }

    public void IterateCollision(RectIntExtensions.RectAction action)
    {
        itemType.CollisionSpace.Iterate(Coordinates, action);
    }

    public bool IterateCollision(RectIntExtensions.RectActionBool action)
    {
        return itemType.CollisionSpace.Iterate(Coordinates, action);
    }

    public void Destroy()
    {
        if (gameObject != null)
        {
            Objeto.Destroy(gameObject);
        }
        ParentTilemap.SetTile(Coordinates, null);
    }
}
