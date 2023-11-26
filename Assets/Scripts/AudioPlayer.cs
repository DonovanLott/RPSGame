using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    AudioMixer mixer;

    [SerializeField]
    public Slider SFXslider;

    [SerializeField]
    public GameObject SFXsliderText;

    const string MIXER_SFX = "sfxVolume";

    float prevSFXVolume;

    void Awake()
    {
        SFXslider = GameObject.Find("SFXVolumeSlider").GetComponent<Slider>();
        SFXslider.onValueChanged.AddListener(SetSFXVolume);
        SFXsliderText = GameObject.Find("SFXVolumeSliderText");

        if (PlayerPrefs.GetFloat("SfxSliderVolumeLevel", SFXslider.value) == 0)
        {
            SFXslider.value = 1;
        }
        else
        {
            SFXslider.value = PlayerPrefs.GetFloat("SfxSliderVolumeLevel", SFXslider.value);
        }


    }

    void Update()
    {
        SFXsliderText.GetComponent<TMPro.TextMeshProUGUI>().text = "Game Volume: " + Math.Round((SFXslider.value * 100), 0) + "%";
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }

    public void BackVolume()
    {
        SFXslider.value = prevSFXVolume;
    }

    public void SaveVolume()
    {
        if (prevSFXVolume != SFXslider.value)
        {
            prevSFXVolume = SFXslider.value;
            PlayerPrefs.SetFloat("SfxSliderVolumeLevel", SFXslider.value);
        }
    }

    public void PlayAudio(AudioSource audioSource, AudioClip[] sounds, int startIndex, int endIndex)
    {
        int index = Random.Range(startIndex, endIndex);
        audioSource.clip = sounds[index];
        audioSource.Play();
    }
}
