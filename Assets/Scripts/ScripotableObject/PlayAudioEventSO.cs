using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/PlayAudioEventSO")]
public class PlayAudioEventSO : ScriptableObject
{
    public UnityAction<AudioClip> OnAudioPlayEvent;

    public void PlayAudioEvent(AudioClip clip)
    {
        OnAudioPlayEvent?.Invoke(clip);
    }
}
