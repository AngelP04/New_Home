using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeManager : MonoBehaviour
{
    private AudioVolume[] audios;
    public float maxVolume;
    public float currentVolume;

    // Start is called before the first frame update
    void Start()
    {
        audios = FindObjectsOfType<AudioVolume>();
        ChangeVolume();
    }

    private void Update()
    {
        ChangeVolume();
    }

    public void ChangeVolume()
    {
        if(currentVolume >= maxVolume)
        {
            currentVolume = maxVolume;
        }
        foreach (AudioVolume audio in audios)
        {
            audio.SetAudioLevel(currentVolume);
        }
    }
}
