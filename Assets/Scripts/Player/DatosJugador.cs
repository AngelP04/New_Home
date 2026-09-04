using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosJugador : MonoBehaviour
{
    public void Save(ref PlayerSaveData data)
    {
        data.position = transform.position;
    }

    public void Load(ref PlayerSaveData data)
    {
        transform.position = data.position;
    }
}

[System.Serializable]
public struct PlayerSaveData
{
    public Vector3 position;
}
