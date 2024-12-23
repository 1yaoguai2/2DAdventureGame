using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class Sign : MonoBehaviour
{
    private GameObject _signSprite;
    private Animator _animator;
    private bool _canPress;
    private Vector3 _spriteDir;
    private InputSystem_Actions _playerInput;
    private IInteractable _currentItem;

    private bool CanPress
    {
        get => _canPress;
        set
        {
            if (_canPress != value)
            {
                _canPress = value;
                _signSprite.SetActive(_canPress);
            }
        }
    }

    private void Awake()
    {
        _signSprite = transform.GetChild(0).gameObject;
        _animator = _signSprite.GetComponent<Animator>();
        _playerInput = new InputSystem_Actions();
        _playerInput.Enable();
    }

    private void OnEnable()
    {
        //InputSystem.onActionChange += OnActionChange;
        _playerInput.GamePlay.Conifrm.started += OnConfirm;
    }

    /// <summary>
    /// 输入方式
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="actionChange"></param>
    private void OnActionChange(object obj, InputActionChange actionChange)
    {
        if (actionChange == InputActionChange.ActionStarted)
        {
            var d = ((InputAction)obj).activeControl.device;
            CustomLogger.Log(d.ToString());
            switch (d.device)
            {
                case Keyboard:
                    CustomLogger.Log("使用键鼠操作！");
                    break;
                case DualShockGamepad:
                    CustomLogger.Log("使用手柄操作！");
                    break;
            }
        }
    }

    private void OnConfirm(InputAction.CallbackContext obj)
    {
        CustomLogger.Log("触发按钮按下");
        _currentItem?.TriggerAction();
    }

    private void FixedUpdate()
    {
        _spriteDir = transform.parent.localScale;
        _signSprite.transform.localScale = _spriteDir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            CanPress = true;
            _currentItem = other.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            CanPress = false;
            _currentItem = null;
        }
    }
}