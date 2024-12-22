using System;
using Cinemachine;
using UnityEngine;

public class Sign : MonoBehaviour
{
    private GameObject _signSprite;
    private Animator _animator;
    private bool _canPress;
    private Vector3 _spriteDir;
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            CanPress = false;
        }
    }
}