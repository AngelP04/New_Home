using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGuion : MonoBehaviour
{
    public Dictionary<int, string[]> guion = new();

    private void Start()
    {
        string[] dialog =
        {
            "Bienvenido otra vez"
        };
        guion[0] = dialog;
    }
}
