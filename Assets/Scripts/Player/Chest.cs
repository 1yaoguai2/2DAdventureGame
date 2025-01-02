using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour, IInteractable
{
    [Header("触发事件")] public UnityEvent onChestEvent;
    [Header("参数")] private GameObject closeChest;
    private GameObject openChest;
    private bool isOpen;

    private void Awake()
    {
        closeChest = transform.GetChild(0).gameObject;
        openChest = transform.GetChild(1).gameObject;
    }

    public void TriggerAction()
    {
        if (!isOpen)
        {
            isOpen = true;
            CustomLogger.Log("Open Chest!");
            closeChest.SetActive(false);
            openChest.SetActive(true);
            transform.GetComponent<BoxCollider2D>().enabled = false;
            onChestEvent?.Invoke();
        }
    }
}