using UnityEngine;

public class AudioDefinition : MonoBehaviour
{
    public PlayAudioEventSO audioPlayEventSO;
    public AudioClip audioClip;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        audioPlayEventSO.OnAudioPlayEvent?.Invoke(audioClip);
    }
}
