using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Start()
    {
        if (!PlayerMovement.playerCreated)
        {
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
