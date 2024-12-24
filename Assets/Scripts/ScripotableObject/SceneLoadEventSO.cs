using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, bool> LoadRequestEvent;

    /// <summary>
    /// 场景加载请求
    /// </summary>
    /// <param name="locationToLoad">目标场景资产</param>
    /// <param name="fadeScreen">渐入渐出效果开关</param>
    public void RaiseLoadRequestEvent(GameSceneSO locationToLoad, bool fadeScreen)
    {
        LoadRequestEvent?.Invoke(locationToLoad, fadeScreen);
    }
}