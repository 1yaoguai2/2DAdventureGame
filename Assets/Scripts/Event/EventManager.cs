using System;
using UnityEngine;
using XTools.UI;

public class EventManager : MonoBehaviour
{
    [Header("事件监听")] public CharacterEventSO healthEvent;
    public VoidEventSO newGameEvent;

    private MainCanvasController _mainCanvasController;

    public MainCanvasController m_MainCanvasController
    {
        get
        {
            if (_mainCanvasController is null)
            {
                UIManager.Instance.panelDic.TryGetValue("MainCanvas", out BasePanel basePanel);
                if (basePanel is not null)
                {
                    _mainCanvasController = basePanel as MainCanvasController;
                }
            }

            return _mainCanvasController;
        }
    }

    public void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        newGameEvent.OnEventRaised += OnNewGameEvent;
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
        newGameEvent.OnEventRaised -= OnNewGameEvent;
    }

    private void OnHealthEvent(Character character)
    {
        var persentage = character.CurrentHealth / character.maxHealth;
        m_MainCanvasController.OnHealthChanged(persentage);
    }


    private void OnNewGameEvent()
    {
        m_MainCanvasController.OnHealthChanged(1f);
    }
}