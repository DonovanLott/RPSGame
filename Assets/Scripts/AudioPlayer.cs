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
    public Slider slider;

    [SerializeField]
    public GameObject sliderText;

    const string MIXER_SFX = "sfxVolume";

    float prevVolume;

    /*void Awake()
    {
        slider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        slider.onValueChanged.AddListener(SetSFXVolume);
        sliderText = GameObject.Find("VolumeSliderText");

        if (PlayerPrefs.GetFloat("SliderVolumeLevel", slider.value) == 0)
        {
            slider.value = 1;
        }
        else
        {
            slider.value = PlayerPrefs.GetFloat("SliderVolumeLevel", slider.value);
        }


    }

    void Update()
    {
        sliderText.GetComponent<TMPro.TextMeshProUGUI>().text = "Volume: " + Math.Round((slider.value * 100), 0) + "%";
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }

    public void BackVolume()
    {
        slider.value = prevVolume;
    }

    public void SaveVolume()
    {
        if (prevVolume != slider.value)
        {
            prevVolume = slider.value;
            PlayerPrefs.SetFloat("SliderVolumeLevel", slider.value);
        }
    }*/

    public void PlayAudio(AudioSource audioSource, AudioClip[] sounds, int startIndex, int endIndex)
    {
        if (!audioSource.isPlaying)
        {
            int index = Random.Range(startIndex, endIndex);
            audioSource.clip = sounds[index];
            audioSource.Play();
        }  
    }
}
