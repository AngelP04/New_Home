using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Upgrade")]

public class Upgrades : ScriptableObject
{
    public string upgradeName;
    public string description;
    public Sprite image;
    public Item material;
    public int cantidad;
}
