using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    private CinemachineConfiner2D _confiner2D;
    private CinemachineImpulseSource _impulseSource;

    /// <summary>
    /// 视角抖动视角
    /// </summary>
    public VoidEventSO cameraShakeEvent;

    public SceneLoadEventSO sceneLoadEventSO;

    private void Awake()
    {
        _confiner2D = GetComponent<CinemachineConfiner2D>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }


    private void OnEnable()
    {
        cameraShakeEvent.OnEventRaised += OnCameraShakeEvent;
    }

    private void OnDisable()
    {
        cameraShakeEvent.OnEventRaised -= OnCameraShakeEvent;
    }

    /// <summary>
    /// 角色受伤事件广播，视角抖动
    /// </summary>
    private void OnCameraShakeEvent()
    {
        _impulseSource.GenerateImpulse();
    }

    //TODO:新场景新边界
    /// <summary>
    /// 获取新边界，并清除缓存
    /// </summary>
    private void GetNewCameraBounds()
    {
        //var bounds = GameObject.FindWithTag("Bounds");
        // 跨场景查找物体
        var bounds = FindGameObjectWithTagInScenes("Bounds");
        if (bounds is null)
        {
            CustomLogger.LogWarning("摄像机，查找新场景边界失败！");
            return;
        }

        _confiner2D.m_BoundingShape2D = bounds.GetComponent<Collider2D>();
        _confiner2D.InvalidateCache();
    }

    /// <summary>
    /// 在所有加载的场景中查找指定标签的第一个物体
    /// </summary>
    public static GameObject FindGameObjectWithTagInScenes(string tag)
    {
        int sceneCount = SceneManager.sceneCount;

        for (int i = 0; i < sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.isLoaded)
            {
                GameObject[] rootObjects = scene.GetRootGameObjects();
                foreach (GameObject root in rootObjects)
                {
                    // 检查根物体的标签
                    if (root.CompareTag(tag))
                    {
                        return root;
                    }

                    // 在子物体中查找指定标签
                    GameObject taggedChild = root.transform.FindChildWithTag(tag);
                    if (taggedChild != null)
                    {
                        return taggedChild;
                    }
                }
            }
        }

        return null;
    }
}