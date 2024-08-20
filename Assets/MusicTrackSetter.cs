using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrackSetter : MonoBehaviour
{
    [SerializeField] private string trackName;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.PlayTrack(trackName);
    }
}
