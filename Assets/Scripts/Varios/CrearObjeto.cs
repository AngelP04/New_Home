using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearObjeto : MonoBehaviour
{
    public SceneInfo sceneInfo;
    public void Press()
    {
        sceneInfo.buttonPressed = true;
    }
}
