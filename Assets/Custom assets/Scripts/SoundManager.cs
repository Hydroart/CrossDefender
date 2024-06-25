using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource musicSource, sfxSource;
    public AudioClip[] musicSounds, sfxSounds;
    
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;
    
    [Range(0f, 1f)]
    public float sfxVolume = 0.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Demo")
        {
            PlayMusic("Theme");
        }
    }

    public void PlayMusic(string name)
    {
        AudioClip s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else 
        {
            musicSource.clip = s;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        AudioClip s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else 
        {
            sfxSource.clip = s;
            sfxSource.Play();
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float value)
    {
        musicSource.volume =  musicVolume;
    }

    public void SFXVolume(float value)
    {
        sfxSource.volume =  sfxVolume;
    }
}

