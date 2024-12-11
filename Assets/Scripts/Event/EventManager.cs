using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [Header("事件监听")] 
    public CharacterEventSO healthEvent;

    public MainCanvasController mainCanvasController;

    public void OnEnable()
    {
        healthEvent.OnEventReised += OnHealthEvent;
    }

    private void OnDisable()
    {
        healthEvent.OnEventReised += OnHealthEvent;
    }

    private void OnHealthEvent(Character character)
    {
        var persentage = character.CurrentHealth / character.maxHealth;
        mainCanvasController.OnHealthChanged(persentage);
    }
}