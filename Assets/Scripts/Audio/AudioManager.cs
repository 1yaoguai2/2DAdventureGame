using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public PlayAudioEventSO bgmAudioEventSO;
    public PlayAudioEventSO fxAudioEventSO;
    
    public AudioSource bgmSource;
    public AudioSource fxSource;


    private void OnEnable()
    {
        bgmAudioEventSO.OnAudioPlayEvent += OnBGMPlayEvent;
        fxAudioEventSO.OnAudioPlayEvent += OnFXPlayEvent;
    }

 

    private void OnDisable()
    {
        bgmAudioEventSO.OnAudioPlayEvent -= OnBGMPlayEvent;
        fxAudioEventSO.OnAudioPlayEvent -= OnFXPlayEvent;
    }

    private void OnBGMPlayEvent(AudioClip audioClip)
    {
        bgmSource.clip = audioClip;
        bgmSource.Play();
    }
    
    void OnFXPlayEvent(AudioClip audioClip)
    {
        fxSource.clip = audioClip;
        fxSource.Play();
    }
}
