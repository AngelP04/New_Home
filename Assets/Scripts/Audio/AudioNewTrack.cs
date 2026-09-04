using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioNewTrack : MonoBehaviour
{
    public int newTrack;
    public bool playOnStart;
    // Start is called before the first frame update
    void Start()
    {
        if(playOnStart)
        {
            GameManager.instance.newTrack(newTrack);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
