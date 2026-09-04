using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;

public class TimeLinePlayer : MonoBehaviour
{
    private PlayableDirector director;
    // Start is called before the first frame update
    void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
        director.played += DirectorStopped;
    }

    private void Director_Played(PlayableDirector obj)
    {
        Debug.Log("Played");
    }

    private void DirectorStopped(PlayableDirector obj)
    {
        Debug.Log("Stopped");
    }

    public void StartTimeLine()
    {
        director.Play();
    }
}
