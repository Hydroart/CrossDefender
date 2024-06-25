using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Slider musicSlider, sfxSlider;

    public void ToggleMusic()
    {
        SoundManager.instance.ToggleMusic();
    }
    public void ToggleSFX()
    {
        SoundManager.instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        SoundManager.instance.MusicVolume(musicSlider.value);
    }

    public void SFXVolume()
    {
        SoundManager.instance.SFXVolume(sfxSlider.value);
    }
}
