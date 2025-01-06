using System;
using UnityEngine;

public class MobileTouchCanvasController : MonoBehaviour
{

    private GameObject _mobileTouch;

    private void Awake()
    {
        _mobileTouch = transform.GetChild(0).gameObject;
#if UNITY_STANDALONE
        _mobileTouch.SetActive(false);
#endif
    }
}