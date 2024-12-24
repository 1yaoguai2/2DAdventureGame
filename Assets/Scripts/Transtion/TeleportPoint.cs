using UnityEngine;

public class TeleportPoint : MonoBehaviour,IInteractable
{
    public SceneLoadEventSO sceneLoadEventSo;
    public GameSceneSO sceneToGo;
    public bool isFade;
    public void TriggerAction()
    {
        CustomLogger.Log("传送");
        sceneLoadEventSo.RaiseLoadRequestEvent(sceneToGo, isFade);
    }
}
