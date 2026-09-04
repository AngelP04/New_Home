using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Rendering;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

namespace Cultive
{
    public class CultiveLayer : TilemapLayer
    {
        [SerializeField] private Tilemap interactableMap;
        private List<TileBase> tiles = new List<TileBase>();
        public Dictionary<Vector3Int, GameObject> _semillas = new();
        public void Plow(Vector3 position, Item item)
        {
            Vector3Int coords = _tilemap.WorldToCell(position);
            if (interactableMap.GetTile(coords) != null)
            {
                _tilemap.SetTile(coords, item.Tile);
                updated = false;
                tiles.Add(_tilemap.GetTile(coords));
                InventoryManager.instance.GetSelectedItem(true);
            }
        }

        public void Load(List<TileInfo> tilesInfo, List<Model> seeds = null, List<GameObject> objs = null)
        {
            foreach(TileInfo tile in tilesInfo)
            {
                _tilemap.SetTile(tile.position, tile.tile);
                tiles.Add(tile.tile);
            }
            if(seeds != null && objs != null)
            {
                for (int i = 0; i <= seeds.Count - 1; i ++)
                {
                    _semillas.Add(seeds[i].Coordinates, objs[i]);
                    var model = new Model(seeds[i].itemType, seeds[i].Coordinates, _tilemap, objs[i]);
                    _objs.Add(model.Coordinates, model);
                }
            }

        }

        public void Placing(Vector3 position, Item item)
        {
            Vector3Int coords = _tilemap.WorldToCell(position);
            if (tiles.Contains(_tilemap.GetTile(coords)))
            {
                Place(position, item, _semillas);
            }
        }

        public GameObject returnPlant(Vector3 worldsCoords)
        {
            GameObject obj;
            GameObject seed = null;
            var coords = _tilemap.WorldToCell(worldsCoords);
            if (_semillas.ContainsKey(coords))
            {
                if (_semillas.TryGetValue(coords, out obj))
                {
                    seed = _semillas[coords];
                }
            }
            return seed;
        }

        public void RegarPlanta(Vector3 worldsCoords, Item item)
        {
            GameObject obj;
            var coords = _tilemap.WorldToCell(worldsCoords);
            if (_semillas.ContainsKey(coords))
            {
                _tilemap.SetTile(coords, item.Tile);
                if (_semillas.TryGetValue(coords, out obj))
                {
                    _semillas[coords].gameObject.GetComponent<Semilla>().water = true;
                }
            }
        }
    }
}