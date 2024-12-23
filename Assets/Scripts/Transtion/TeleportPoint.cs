using UnityEngine;

public class TeleportPoint : MonoBehaviour,IInteractable
{
    public void TriggerAction()
    {
        CustomLogger.Log("传送");
    }
}
