using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Progress;

public class AnimalLayer : TilemapLayer
{
    public List<TileBase> tiles = new();


    public void SpawnAnimal(Item animal)
    {
        foreach (var pos in _tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = _tilemap.CellToWorld(localPlace);
            if (!_objs.ContainsKey(localPlace))
            {
                tiles.Add(_tilemap.GetTile(localPlace));
                SetTile(localPlace, animal);
                GameObject itemObject = CreateItem(localPlace, animal);
                var animalGranja = new Model(animal, localPlace, _tilemap, itemObject);
                _objs.Add(localPlace, animalGranja);
                break;
            }
            else
            {
                break;
            }
        }
    }
}
