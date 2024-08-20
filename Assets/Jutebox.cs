using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jutebox : MonoBehaviour
{
    private string trackName = null;

    void Start()
    {
        AudioManager.SetJutebox(this);
    }

    private void OnDestroy()
    {
        AudioManager.RemoveJutebox(this);
    }

    public string GetTrackName()
    {
        return trackName;
    }

    public void SetTrackName(string name)
    {
        this.trackName = name;
    }
}
