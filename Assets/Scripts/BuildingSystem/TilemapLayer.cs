using Cultive;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TilemapLayer : MonoBehaviour
{
    protected Tilemap _tilemap { get; private set; }
    protected Dictionary<Vector3Int, Model> _objs = new();
    public SceneInfo sceneInfo;
    [SerializeField] private CollisionLayer collisionLayer;

    public bool updated = true;

    protected void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    public bool IsEmpty(Vector3 worldsCoords)
    {
        var coords = _tilemap.WorldToCell(worldsCoords);

        return !_objs.ContainsKey(coords);
    }
    public void DeleteObj(Vector3 position)
    {
        Vector3Int coords = _tilemap.WorldToCell(position);
        if (!_objs.ContainsKey(coords))
        {
            return;
        }
        var obj = _objs[coords];
        _objs.Remove(coords);
        obj.Destroy();
    }

    public Item returnObj(Vector3 worldsCoords)
    {
        Model model;
        Item item = null;
        var coords = _tilemap.WorldToCell(worldsCoords);
        if(_objs.ContainsKey(coords))
        {
            if(_objs.TryGetValue(coords, out model))
            {
                item = _objs[coords].itemType.Son;
            }
        }
        return item;
    }

    public GameObject reuturnGameObject(Vector3 worldsCoords, Dictionary<Vector3Int, GameObject> dict)
    {
        Vector3Int coords = _tilemap.WorldToCell(worldsCoords);
        GameObject obj = null;
        if(dict.ContainsKey(coords))
        {
            obj = dict[coords];
        }
        return obj;
    }

    private void Update()
    {
        if(!updated)
        {
            GameObject obj = null;
            sceneInfo.tiles.Clear();
            foreach (var pos in _tilemap.cellBounds.allPositionsWithin)
            {
                Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                if(_objs.ContainsKey(localPlace))
                {
                    obj = _objs[localPlace].itemType.gameObject;
                }
                TileInfo tile = new(_tilemap.GetTile(localPlace), localPlace, obj);
                if (_tilemap.HasTile(localPlace) && !sceneInfo.tiles.Contains(tile))
                {
                    sceneInfo.tiles.Add(tile);
                }
            }
            sceneInfo.seeds = _objs;
            updated = true;
        }
    }

    public void SetTile(Vector3Int coords, Item item)
    {
        if(item.Tile != null)
        {
            if (item.Tile != null)
            {
                var tileChangeData = new TileChangeData(
                    coords,
                    item.Tile,
                    Color.white,
                    Matrix4x4.Translate(item.tileOffset)
                    );
                _tilemap.SetTile(tileChangeData, false);
            }
        }
    }

    public void Place(Vector3 worldsCoords, Item item, Dictionary<Vector3Int, GameObject> dict)
    {
        Vector3Int coords = _tilemap.WorldToCell(worldsCoords);
        GameObject obj = Build(worldsCoords, item);
        dict.Add(coords, obj);
    }


    private GameObject Build(Vector3 worldsCoords, Item item)
    {
        GameObject itemObject = null;
        Vector3Int coords = _tilemap.WorldToCell(worldsCoords);
        if (!_objs.ContainsKey(coords))
        {
            //SetTile(coords, item); Prueba para ver como funciona sin agregar tile
            itemObject = CreateItem(coords, item);
            Model model = new(item, coords, _tilemap, itemObject);
            if(item.UseCustomCollision)
            {
                collisionLayer.SetCollision(model, true);
                RegisterBuildableCollision(model);
            }
            else
            {
                _objs.Add(coords, model);
            }
            updated = false;
            InventoryManager.instance.GetSelectedItem(true);
        }
        return itemObject;
    }
    public GameObject CreateItem(Vector3Int coords, Item item)
    {
        GameObject itemObject = null;
        if (item.gameObject != null)
        {
            itemObject = Instantiate(item.gameObject, _tilemap.CellToWorld(coords) + _tilemap.cellSize / 2 + item.tileOffset, Quaternion.identity);
            itemObject.GetComponent<PersistenceObject>().id = item.nameID;
        }
        return itemObject;
    }

    public void RegisterBuildableCollision(Model model)
    {
        model.IterateCollision(tileCoords => _objs.Add(tileCoords, model));
    }

}
