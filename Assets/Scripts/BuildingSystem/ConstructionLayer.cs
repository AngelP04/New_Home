using Cultive;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Progress;

namespace PlayerBuildingSystem
{
    public class ConstructionLayer : TilemapLayer
    {
        public Dictionary<Vector3Int, GameObject> _buildables = new();

        public void Placing(Vector3 worldsCoords, Item item)
        {
            Place(worldsCoords, item, _buildables);
        }

        public void Load(List<Model> seeds = null, List<GameObject> objs = null)
        {           
            if (seeds != null && objs != null)
            {
                for (int i = 0; i <= seeds.Count - 1; i++)
                {
                    _buildables.Add(seeds[i].Coordinates, objs[i]);
                    var model = new Model(seeds[i].itemType, seeds[i].Coordinates, _tilemap, objs[i]);
                    _objs.Add(model.Coordinates, model);
                }
            }

        }

        public GameObject returnBuildable(Vector3 worldCoords)
        {
            return reuturnGameObject(worldCoords, _buildables);
        }
    }
}