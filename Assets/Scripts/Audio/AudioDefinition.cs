using UnityEngine;

public class AudioDefinition : MonoBehaviour
{
    public PlayAudioEventSO audioPlayEventSO;
    public AudioClip audioClip;
    public bool onEnablePlay = true;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        if (onEnablePlay)
        {
            PlayAudioClip();
        }
    }

    public void PlayAudioClip()
    {
        audioPlayEventSO.OnAudioPlayEvent?.Invoke(audioClip);
    }
}
