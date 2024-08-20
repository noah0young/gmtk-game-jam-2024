using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MusicStruct
{
    public string name;
    public float volume = 0.5f;
    public AudioSource audio;
}

public class AudioManager : MonoBehaviour
{
    private static AudioManager Instance;
    private static float volume = .5f;

    [SerializeField] private MusicStruct[] sfx;
    [SerializeField] private MusicStruct[] tracks;
    [SerializeField] private MusicStruct[] juteboxTracks;
    private List<MusicStruct> remainingJuteboxTracks = new List<MusicStruct>();

    private static List<Jutebox> juteboxes = new List<Jutebox>();
    private static MusicStruct curTrack;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.LogWarning("Destroying this audio manager, since its the second");
            return;
        }
        else
        {
            Instance = this;
            // inits jutebox tracks
            foreach (MusicStruct juteboxTrack in juteboxTracks)
            {
                remainingJuteboxTracks.Add(juteboxTrack);
            }
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySFX(string name, bool fromStart = false)
    {
        MusicStruct music = GetMusicStruct(name, Instance.sfx);
        music.audio.volume = music.volume * volume;
        if (fromStart)
        {
            music.audio.Stop();
        }
        music.audio.Play();
    }

    public static void PlayTrack(string name, bool fromStart = false)
    {
        foreach (MusicStruct music in Instance.tracks)
        {
            music.audio.volume = 0;
        }
        if (name != "" && name != "None")
        {
            MusicStruct music = GetMusicStruct(name, Instance.tracks);
            music.audio.volume = music.volume * volume;
            if (fromStart)
            {
                music.audio.Stop();
            }
            curTrack = music;
            if (juteboxes.Count <= 0)
            {
                music.audio.Play();
            }
        }
    }

    private static MusicStruct GetMusicStruct(string name, MusicStruct[] musics)
    {
        foreach (MusicStruct music in musics)
        {
            if (music.name == name)
            {
                return music;
            }
        }
        throw new Exception("Music not found by name of \"" + name + "\"");
    }

    public static void SetVolume(float volume)
    {
        AudioManager.volume = volume;
    }

    public static void SetJutebox(Jutebox box)
    {
        juteboxes.Add(box);
        if (curTrack != null)
        {
            curTrack.audio.Stop();
        }
        PlayRandomjuteboxTrack(box);
    }

    private static void PlayRandomjuteboxTrack(Jutebox box)
    {
        int len = Instance.remainingJuteboxTracks.Count;
        MusicStruct randTrack = Instance.remainingJuteboxTracks[UnityEngine.Random.Range(0, len)];
        Instance.remainingJuteboxTracks.RemoveAt(UnityEngine.Random.Range(0, len));
        randTrack.audio.volume = randTrack.volume * volume;
        randTrack.audio.Play();
        box.SetTrackName(randTrack.name);
    }

    public static void RemoveJutebox(Jutebox box)
    {
        MusicStruct playedTrack = GetMusicStruct(box.GetTrackName(), Instance.juteboxTracks);
        playedTrack.audio.Stop();
        Instance.remainingJuteboxTracks.Add(playedTrack);
        juteboxes.Remove(box);
        if (juteboxes.Count <= 0)
        {
            curTrack.audio.Play();
        }
    }
}
