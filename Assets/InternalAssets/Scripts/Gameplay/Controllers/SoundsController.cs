using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : Singleton<SoundsController>
{
    [SerializeField] private AudioClip successClip, failClip;
    [SerializeField] private AudioSource[] audioSources;


    public void PlaySuccess()
    {
        PlayOneShot(successClip);
    }

    public void PlayFail()
    {
        PlayOneShot(failClip);
    }

    private void PlayOneShot(AudioClip clip)
    {
        AudioManager.Instance.PlaySound(clip);
    }

    public void StopAllSounds()
    {
        foreach (var _source in audioSources)
        {
            _source.Stop();
        }
    }
}
