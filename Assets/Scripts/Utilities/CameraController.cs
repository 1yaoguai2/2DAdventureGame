using System;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineConfiner2D _confiner2D;
    private CinemachineImpulseSource _impulseSource;
    public VoidEventSO cameraShakeEvent;
    private void Awake()
    {
        _confiner2D = GetComponent<CinemachineConfiner2D>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Start()
    {
        GetNewCameraBounds();
        cameraShakeEvent.OnEventRaised += OnCameraShakeEvent;
    }

    /// <summary>
    /// 角色受伤事件广播
    /// </summary>
    private void OnCameraShakeEvent()
    {
        _impulseSource.GenerateImpulse();
    }

    /// <summary>
    /// 获取新边界，并清除缓存
    /// </summary>
    private void GetNewCameraBounds()
    {
        var bounds = GameObject.FindWithTag("Bounds");
        if (bounds is null) return;
        _confiner2D.m_BoundingShape2D = bounds.GetComponent<Collider2D>();
        _confiner2D.InvalidateCache();
    }
}