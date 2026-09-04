using System;
using System.Collections;
using System.Collections.Generic;
using TimeSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Key Dates")]
public class KeyDATES : ScriptableObject
{
    [field: SerializeField] public SerializableDateTime KeyDate;
    public bool yearly;
    public bool monthly;
    public Sprite image;
    public string description;
}