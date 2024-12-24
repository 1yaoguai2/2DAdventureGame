using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneLoad : MonoBehaviour
{
    [Header("加载场景事件监听")] public SceneLoadEventSO sceneLoadEvent;

    public GameSceneSO currentLoadedLevel;

    private GameSceneSO loadScene;
    private bool isFade;
    public float fadeTime;

    public void Awake()
    {
        //Addressables.LoadSceneAsync(currentLoadedLevel.sceneReference, LoadSceneMode.Additive);
        currentLoadedLevel.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        sceneLoadEvent.LoadRequestEvent += OnLoadSceneRequestEvent;
    }

    private void OnDisable()
    {
        sceneLoadEvent.LoadRequestEvent -= OnLoadSceneRequestEvent;
    }

    private void OnLoadSceneRequestEvent(GameSceneSO scene, bool fadeScene)
    {
        loadScene = scene;
        this.isFade = fadeScene;
        CustomLogger.Log("请求切换场景");
        if (loadScene is null || currentLoadedLevel is null)
        {
            CustomLogger.LogError("场景加载错误：卸载或者将加载的场景为空！");
            return;
        }

        StartCoroutine(UnLoadPreviousScene());
    }

    private IEnumerator UnLoadPreviousScene()
    {
        if (isFade)
        {
            //TODO:逐渐黑暗
        }

        //异步加载新场景,但是不激活
        var asyncOperation = loadScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, false);
        yield return new WaitForSeconds(fadeTime);
        //TO/DO:先加载再启用启用新场景
        // 等待加载完成
        while (!asyncOperation.IsDone)
        {
            // float progress = asyncOperation.PercentComplete;
            // Debug.Log($"Loading progress: {progress * 100}%");
            yield return null;
        }

        //卸载当前加载的场景
        currentLoadedLevel.sceneReference.UnLoadScene();

        //激活场景
        asyncOperation.Result.ActivateAsync();
        currentLoadedLevel = loadScene;
    }
}