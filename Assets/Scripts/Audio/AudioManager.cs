using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public PlayAudioEventSO bgmAudioEventSO;
    public PlayAudioEventSO fxAudioEventSO;

    public AudioSource bgmSource;
    public AudioSource fxSource;


    public AudioMixer mixer;
    public FloatEventSo audioChangedSO;

    private void OnEnable()
    {
        bgmAudioEventSO.OnAudioPlayEvent += OnBGMPlayEvent;
        fxAudioEventSO.OnAudioPlayEvent += OnFXPlayEvent;
        audioChangedSO.OnEventRaised += OnVolumeEvent;
    }


    private void OnDisable()
    {
        bgmAudioEventSO.OnAudioPlayEvent -= OnBGMPlayEvent;
        fxAudioEventSO.OnAudioPlayEvent -= OnFXPlayEvent;
        audioChangedSO.OnEventRaised -= OnVolumeEvent;
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

    private void OnVolumeEvent(float value)
    {
        mixer.SetFloat("MasterVolume", value * 100 - 80);
    }
}