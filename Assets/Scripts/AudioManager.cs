using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip[] musicClips;
    public AudioClip[] sfxClips;

    private Dictionary<string, AudioClip> musicDictionary;
    private Dictionary<string, AudioClip> sfxDictionary;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            musicDictionary = new Dictionary<string, AudioClip>();
            foreach (var clip in musicClips)
            {
                musicDictionary.Add(clip.name, clip);
            }

            sfxDictionary = new Dictionary<string, AudioClip>();
            foreach (var clip in sfxClips)
            {
                sfxDictionary.Add(clip.name, clip);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        if (musicDictionary.ContainsKey(name))
        {
            musicSource.clip = musicDictionary[name];
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music clip not found: " + name);
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(string name)
    {
        if (sfxDictionary.ContainsKey(name))
        {
            sfxSource.PlayOneShot(sfxDictionary[name]);
        }
        else
        {
            Debug.LogWarning("SFX clip not found: " + name);
        }
    }

    // º¼·ý Á¶Àý
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        musicSource.mute = volume == 0;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        sfxSource.mute = volume == 0;
    }
}
