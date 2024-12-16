using System;
using UnityEngine;
using XTools.UI;

public class EventManager : MonoBehaviour
{
    [Header("事件监听")] public CharacterEventSO healthEvent;

    //private MainCanvasController mainCanvasController;

    public void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
    }

    private void OnHealthEvent(Character character)
    {
        var persentage = character.CurrentHealth / character.maxHealth;
        UIManager.Instance.panelDic.TryGetValue("MainCanvas", out BasePanel basePanel);
        if (basePanel is not null)
        {
            var mainCanvas = basePanel as MainCanvasController;
            mainCanvas.OnHealthChanged(persentage);
        }
    }
}