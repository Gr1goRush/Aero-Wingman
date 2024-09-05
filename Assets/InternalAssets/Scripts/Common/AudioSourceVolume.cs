using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceVolume : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;

    [Range(0f, 1f)]
    [SerializeField] private float multiplier = 1f;

    private void Start()
    {
        AudioManager.Instance.OnVolumeChanged += SetVolume;

        SetVolume();
    }

    private void SetVolume()
    {
        audioSource.volume = AudioManager.Instance.Volume * multiplier;
    }

    private void Reset()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        AudioManager.Instance.OnVolumeChanged -= SetVolume;
    }
}
