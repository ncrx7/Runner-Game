using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] public AudioSource _effectAudioSource;
    [SerializeField] public AudioClip _coinCollectSoundEffect, _stunSoundEffect;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayEffectSound(AudioClip audioClip)
    {
        _effectAudioSource.clip = audioClip;
        _effectAudioSource.Play();
    }
}
