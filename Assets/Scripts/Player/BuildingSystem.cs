using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using static UnityEngine.UI.Image;

namespace PlayerBuildingSystem
{
    public class BuildingSystem : MonoBehaviour
    {

        [SerializeField] private TileBase highlightTile;
        private Tilemap tilemap;
        private Tilemap tempTileMap;
        private Tilemap interactableMap;

        private Vector3Int playerPos;
        private Vector3Int originPos;
        private Vector3Int CursorPos;
        private Vector3Int CursorPosPos;
        private Vector3Int CursorPosNeg;
        private Vector3Int posPos;
        private Vector3Int posNeg;
        private Vector3Int range = new Vector3Int(2, 2, 0);
        public bool highLighted;

        private void Awake()
        {
            FindTilemap();
        }

        public bool IsIntereractable(Vector3 position)
        {
            if (!interactableMap) return false;
            Vector3Int coords = interactableMap.WorldToCell(position);
            TileBase tile = interactableMap.GetTile(coords);
            if (tile != null)
            {
                return true;
            }
            return false;
        }

        public Vector3Int GetMouseOnGrid()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int mouseCellPos = tilemap.WorldToCell(mousePos);
            mouseCellPos.z = 0;

            return mouseCellPos;
        }

        private void FindTilemap()
        {
            tilemap = GameObject.FindGameObjectWithTag("Background").GetComponent<Tilemap>();
            tempTileMap = GameObject.FindGameObjectWithTag("Temp").GetComponent<Tilemap>();
            interactableMap = GameObject.FindGameObjectWithTag("Interactable").GetComponent<Tilemap>();
        }

        private void GetUpdgrades(Vector3Int origin, string upgrade = "")
        {
            originPos = origin;
            switch (upgrade)
            {
                case "":
                    break;
                case "Horizontal":
                    posPos = new Vector3Int(origin.x + 1, origin.y, 0);
                    posNeg = new Vector3Int(origin.x - 1, origin.y, 0);
                    tempTileMap.SetTile(posPos, highlightTile);
                    tempTileMap.SetTile(posNeg, highlightTile);
                    CursorPosPos = posPos;
                    CursorPosNeg = posNeg;
                    break;
                case "Vertical":
                    posPos = new Vector3Int(origin.x, origin.y + 1, 0);
                    posNeg = new Vector3Int(origin.x, origin.y - 1, 0);
                    tempTileMap.SetTile(posPos, highlightTile);
                    tempTileMap.SetTile(posNeg, highlightTile);
                    CursorPosPos = posPos;
                    CursorPosNeg = posNeg;
                    break;
                case "Area":
                    for (int x = origin.x - 1; x <= origin.x + 1; x++)
                    {
                        for (int y = origin.y - 1; y <= origin.y + 1; y++)
                        {
                            if (x == originPos.x && y == originPos.y)
                            {
                                continue;
                            }
                            tempTileMap.SetTile(new Vector3Int(x, y, 0), highlightTile);
                        }
                    }
                    break;
                case "Array":
                    tempTileMap.SetTile(origin, null);
                    bool parx = origin.x % 2 == 0;
                    bool pary = origin.y % 2 == 0;
                    for (int x = origin.x - 2; x <= origin.x + 2; x++)
                    {
                        for (int y = origin.y - 1; y <= origin.y + 1; y++)
                        {
                            if (y == origin.y && x == origin.x)
                            { continue; }
                            if ((parx && pary || !parx && !pary) && (x % 2 == 0 && y % 2 == 0 || x % 2 != 0 && y % 2 != 0))
                            {
                                continue;
                            }
                            if ((!parx && pary || parx && !pary) && (x % 2 != 0 && y % 2 == 0 || x % 2 == 0 && y % 2 != 0))
                            { continue; }
                            tempTileMap.SetTile(new Vector3Int(x, y, 0), highlightTile);
                        }
                    }
                    break;
            }
        }


        private void HighLightTile()
        {
            Vector3Int mouseGridPos = GetMouseOnGrid();
            InventoryItem item = InventoryManager.instance.inventorySlots[InventoryManager.instance.selectedSlot].GetComponentInChildren<InventoryItem>();

            if (CursorPos != mouseGridPos)
            {
                tempTileMap.SetTile(CursorPos, null);
                tempTileMap.SetTile(CursorPosPos, null);
                tempTileMap.SetTile(CursorPosNeg, null);
                CleanArea();

                if (InRange(playerPos, mouseGridPos, range))
                {
                    tempTileMap.SetTile(mouseGridPos, highlightTile);
                    if (item != null)
                    {
                        GetUpdgrades(mouseGridPos, item.updgrade);
                    }
                    CursorPos = mouseGridPos;
                    originPos = CursorPos;
                    highLighted = true;
                }
                else
                {
                    highLighted = false;
                }
            }
            else
            {
                if (highLighted)
                {
                    if (item != null)
                    {
                        GetUpdgrades(mouseGridPos, item.updgrade);
                    }
                    else
                    {
                        CleanArea();
                    }
                }
            }
        }

        private void CleanArea()
        {
            if (originPos != Camera.main.ScreenToWorldPoint(Input.mousePosition))
            {
                for (int x = originPos.x - 2; x <= originPos.x + 2; x++)
                {
                    for (int y = originPos.y - 1; y <= originPos.y + 1; y++)
                    {
                        if (x == originPos.x && y == originPos.y)
                        {
                            continue;
                        }
                        tempTileMap.SetTile(new Vector3Int(x, y, 0), null);
                    }
                }
            }
        }

        private void Update()
        {
            if (!tilemap || !tempTileMap || !interactableMap)
            {
                FindTilemap();
            }

            playerPos = tilemap.WorldToCell(transform.position);

            HighLightTile();
        }

        private bool InRange(Vector3Int positionA, Vector3Int positionB, Vector3Int range)
        {
            Vector3Int distance = positionA - positionB;
            if (Math.Abs(distance.x) >= range.x || Math.Abs(distance.y) >= range.y)
            {
                return false;
            }
            return true;
        }
    }
}