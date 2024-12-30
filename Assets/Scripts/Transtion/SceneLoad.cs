using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneLoad : MonoBehaviour
{
    [Header("加载场景事件监听")] public SceneLoadEventSO sceneLoadEvent;
    [Header("加载场景完成事件广播")] public VoidEventSO onSceneLoadEndEvent;
    [Header("加载场景淡入淡出事件广播")] public VoidEventSO onFadeImageEvent;
    [Header("新游戏")] public VoidEventSO onNewGameEvent;
    public GameSceneSO firstScene;
    public GameSceneSO leveScene;
    private GameSceneSO currentLoadedLevel;
    private GameSceneSO loadScene;
    private bool isFade;
    public float fadeTime;

    private void Start()
    {
        OnLoadSceneRequestEvent(firstScene, false);
    }

    private void OnEnable()
    {
        sceneLoadEvent.LoadRequestEvent += OnLoadSceneRequestEvent;
        onNewGameEvent.OnEventRaised += OnNewGame;
    }

    private void OnDisable()
    {
        sceneLoadEvent.LoadRequestEvent -= OnLoadSceneRequestEvent;
        onNewGameEvent.OnEventRaised -= OnNewGame;
    }

    private void OnNewGame()
    {
        OnLoadSceneRequestEvent(leveScene, true);
    }

    private void OnLoadSceneRequestEvent(GameSceneSO scene, bool fadeScene)
    {
        loadScene = scene;
        this.isFade = fadeScene;
        CustomLogger.Log("请求切换场景");
        if (loadScene is null)
        {
            CustomLogger.LogError("场景加载错误：将加载的场景为空！");
            return;
        }

        StartCoroutine(UnLoadPreviousScene());
    }

    private IEnumerator UnLoadPreviousScene()
    {
        //异步加载新场景,但是不激活
        var asyncOperation = loadScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, false);
        if (isFade)
        {
            //TO-DO:逐渐黑暗
            onFadeImageEvent.RaiseEvent();
            yield return new WaitForSeconds(fadeTime);
        }

        //TO/DO:先加载再启用启用新场景
        // 等待加载完成
        while (!asyncOperation.IsDone)
        {
            // float progress = asyncOperation.PercentComplete;
            // Debug.Log($"Loading progress: {progress * 100}%");
            yield return null;
        }

        //卸载当前加载的场景
        if (currentLoadedLevel is not null)
            currentLoadedLevel.sceneReference.UnLoadScene();
        //激活场景
        var setActiveResult = asyncOperation.Result.ActivateAsync();
        if (!setActiveResult.isDone)
        {
            yield return null;
        }
        //跟新当前场景
        currentLoadedLevel = loadScene;
        if (isFade)
        {
            //TO-DO:逐渐变亮
            onFadeImageEvent.RaiseEvent();
            yield return new WaitForSeconds(fadeTime);
        }
        //场景切换完毕事件广播
        onSceneLoadEndEvent.RaiseEvent();
    }
}