using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Range(0.0f, 1.0f)] [SerializeField] private float volume;
    [SerializeField] private AudioSource backgroundMusic;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (PlayerPrefs.HasKey("volume"))
        {
            volume = PlayerPrefs.GetFloat("volume");
        }
    }

    private void Update()
    {
        backgroundMusic.volume = volume;
    }

    public void SetVolume(float volumeIn)
    {
        volume = volumeIn;
        PlayerPrefs.SetFloat("volume", volume);
    }

    public float GetVolume()
    {
        return volume;
    }
}
