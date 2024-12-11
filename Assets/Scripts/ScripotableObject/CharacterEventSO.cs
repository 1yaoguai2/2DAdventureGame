using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/CharacterEventSO")]
public class CharacterEventSO : ScriptableObject
{
    public UnityAction<Character> OnEventReised;

    public void RaiseEvent(Character character)
    {
        OnEventReised?.Invoke(character);
    }
}