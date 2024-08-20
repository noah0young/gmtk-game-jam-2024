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
    [SerializeField] private MusicStruct[] jukeboxTracks;
    private List<MusicStruct> remainingJukeboxTracks = new List<MusicStruct>();

    private static List<Jutebox> jukeboxes = new List<Jutebox>();
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
            // inits jukebox tracks
            foreach (MusicStruct jukeboxTrack in jukeboxTracks)
            {
                remainingJukeboxTracks.Add(jukeboxTrack);
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

    public static void StopSFX(string name)
    {
        MusicStruct music = GetMusicStruct(name, Instance.sfx);
        music.audio.Stop();
    }

    public static void StopMusic()
    {
        foreach (var music in Instance.tracks)
        {
            music.audio.Stop();
        }

        foreach (var music in Instance.jukeboxTracks)
        {
            music.audio.Stop();
        }
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
            if (jukeboxes.Count <= 0)
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

    public static void SetJukebox(Jutebox box)
    {
        jukeboxes.Add(box);
        if (curTrack != null)
        {
            curTrack.audio.Stop();
        }
        PlayRandomJukeboxTrack(box);
    }

    private static void PlayRandomJukeboxTrack(Jutebox box)
    {
        int len = Instance.remainingJukeboxTracks.Count;
        MusicStruct randTrack = Instance.remainingJukeboxTracks[UnityEngine.Random.Range(0, len)];
        Instance.remainingJukeboxTracks.RemoveAt(UnityEngine.Random.Range(0, len));
        randTrack.audio.volume = randTrack.volume * volume;
        randTrack.audio.Play();
        box.SetTrackName(randTrack.name);
    }

    public static void RemoveJukebox(Jutebox box)
    {
        MusicStruct playedTrack = GetMusicStruct(box.GetTrackName(), Instance.jukeboxTracks);
        playedTrack.audio.Stop();
        Instance.remainingJukeboxTracks.Add(playedTrack);
        jukeboxes.Remove(box);
        if (jukeboxes.Count <= 0)
        {
            curTrack.audio.Play();
        }
    }
}
