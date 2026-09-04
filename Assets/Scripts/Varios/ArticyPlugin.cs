using Articy.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticyPlugin : MonoBehaviour
{
    public ArticyRef myFirstArticyModel;
    // Start is called before the first frame update
    void Start()
    {
        var techName = myFirstArticyModel.GetObject().TechnicalName;
        Debug.Log(techName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
