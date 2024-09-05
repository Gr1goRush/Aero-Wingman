using System;
using System.Collections;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public float Volume { get; private set; }

    public AudioSource soundsSource;

    public event Action OnVolumeChanged;

    protected override void Awake()
    {
        base.Awake();

        Volume = PlayerPrefs.GetFloat("Volume", 1f);
        soundsSource.volume = Volume;
    }

    public void SetVolume(float v)
    {
        Volume = Mathf.Clamp(v, 0f, 1f);
        soundsSource.volume = Volume;
        PlayerPrefs.SetFloat("Volume", Volume);

        OnVolumeChanged?.Invoke();
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            soundsSource.PlayOneShot(clip);
        }
    }
}