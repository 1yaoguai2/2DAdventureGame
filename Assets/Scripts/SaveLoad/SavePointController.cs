using UnityEngine;

public class SavePointController : MonoBehaviour
{
    public VoidEventSO onSaveGameEvent;

    /// <summary>
    /// 保存游戏
    /// </summary>
    public void SaveGame()
    {
        onSaveGameEvent.RaiseEvent();
    }
}