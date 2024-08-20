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
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySFX(string name)
    {
        PlayMusic(name, Instance.sfx);
    }

    public static void PlayTrack(string name)
    {
        foreach (MusicStruct music in Instance.tracks)
        {
            music.audio.volume = 0;
        }
        PlayMusic(name, Instance.tracks);
    }

    private static void PlayMusic(string name, MusicStruct[] musics, bool fromStart = false)
    {
        MusicStruct music = GetMusicStruct(name, musics);
        music.audio.volume = music.volume * volume;
        if (fromStart)
        {
            music.audio.Stop();
        }
        music.audio.Play();
    }

    private static MusicStruct GetSFX(string name)
    {
        return GetMusicStruct(name, Instance.sfx);
    }

    private static MusicStruct GetTrack(string name)
    {
        return GetMusicStruct(name, Instance.tracks);
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
}
