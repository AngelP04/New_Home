using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Item")]
public class Item : ScriptableObject
{

    [Header("Only gameplay")]
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);
    public bool hasUpgrade;
    public bool hasUses;

    [Header("Only UI")]
    [Multiline]
    public string description;
    public bool stackable = true;
    public int id;

    [Header("Both")]
    public Sprite image;

    public string nameID;

    [field: SerializeField] public bool UseCustomCollision { get; private set; }

    [field: SerializeField] public RectInt CollisionSpace { get; private set; }

    [field: SerializeField] public TileBase Tile { get; private set; }

    [field: SerializeField] public Vector3 tileOffset { get; private set; }

    [field: SerializeField] public GameObject gameObject { get; set; }

    [field: SerializeField] public GameObject objMenu { get; private set; }

    [field: SerializeField] public TileBase collision { get; private set; }

    public Upgrades[] upgrades;

    public Item Son;
}

public enum ItemType
{
    Material,
    Tool,
    Producto,
    Seed,
    Object
}
public enum ActionType
{
    Dig,
    Mine,
    Cultive,
    Plow,
    Plant,
    None
}

[Serializable]
public class ItemData
{
    public string itemPath;
    public int count;
    public int slot;
}

[CreateAssetMenu(menuName = "Scriptable Craft Item")]
public class CraftedItems: ScriptableObject
{
    public Item itemCraft;
    public Item[] materials;
    public int[] cantidades;
}
