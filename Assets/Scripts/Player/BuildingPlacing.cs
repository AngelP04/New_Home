using Cultive;
using GameInput;
using PlayerBuildingSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacing : MonoBehaviour
{
    [SerializeField] private float _maxBuildingDistanceX, _maxBuildingDistanceY;
    private ConstructionLayer _constructionLayer;
    private CultiveLayer _aliveLayer;
    [SerializeField] private MouseUser mouseUser;
    private Item actualItem;
    private BuildingSystem buildingSystem;
    public SceneInfo sceneInfo;
    private PlayerMovement player;

    private void Start()
    {
        buildingSystem = FindObjectOfType<BuildingSystem>();
        _constructionLayer = FindObjectOfType<ConstructionLayer>();
        _aliveLayer = FindObjectOfType<CultiveLayer>();
        player = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (sceneInfo.isNextScene || !buildingSystem.highLighted) return;
        if (!_aliveLayer || !_constructionLayer)
        {
            buildingSystem = FindObjectOfType<BuildingSystem>();
            _constructionLayer = FindObjectOfType<ConstructionLayer>();
            _aliveLayer = FindObjectOfType<CultiveLayer>();
        }
        if (buildingSystem.IsIntereractable(mouseUser.MouseInWorldPosition))
        {
            actualItem = InventoryManager.instance.GetSelectedItem(false);
            Vector2 posTile = mouseUser.MouseInWorldPosition;

            if (Input.GetMouseButtonDown(1) && actualItem != null)
            {
                player.Spin(posTile);
                switch (actualItem.actionType)
                {
                    case ActionType.None:
                        _constructionLayer.Placing(mouseUser.MouseInWorldPosition, actualItem);
                        break;
                    case ActionType.Cultive:
                        _aliveLayer.Plow(mouseUser.MouseInWorldPosition, actualItem);
                        break;
                    case ActionType.Plant:
                        _aliveLayer.Placing(mouseUser.MouseInWorldPosition, actualItem);
                        break;
                }
            }
        }
    }
}