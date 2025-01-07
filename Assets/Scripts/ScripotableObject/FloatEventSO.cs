using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/FloatEventSO")]
public class FloatEventSo : ScriptableObject
{
    public UnityAction<float> OnEventRaised;

    public void RaiseEvent(float t)
    {
        OnEventRaised?.Invoke(t);
    }
}
