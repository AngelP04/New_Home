using Cultive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.Rendering.DebugUI;

public class CollisionLayer : TilemapLayer
{
    [SerializeField] private TileBase _collisionTileBase;

    public void SetCollision(Model buildable, bool value)
    {
        _collisionTileBase = buildable.itemType.collision;
        var tile = value ? _collisionTileBase : null;
        buildable.IterateCollision(tileCoords => _tilemap.SetTile(tileCoords, tile));
    }
}