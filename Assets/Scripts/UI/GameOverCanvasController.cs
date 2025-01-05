using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverCanvasController : MonoBehaviour
{
    [Header("事件监听")] 
    public VoidEventSO gameOverEventSo;

    public VoidEventSO gameRestEventSo;

    public SceneLoadEventSO loadMenuSceneEventSo;
    public GameSceneSO menuSceneSo;

    private GameObject bg;
    public GameObject btnRest;
    public GameObject btnExit;

    private void Start()
    {
        bg = transform.GetChild(0).gameObject;
        btnExit.GetComponent<Button>().onClick.AddListener(LoadMenuSceneEvent);
        btnRest.GetComponent<Button>().onClick.AddListener(() =>
        {
            gameRestEventSo.RaiseEvent();
        });
    }

    private void OnEnable()
    {
        gameOverEventSo.OnEventRaised += GameOverEvent;
        gameRestEventSo.OnEventRaised += GameRestEvent;
    }

    private void OnDisable()
    {
        gameOverEventSo.OnEventRaised -= GameOverEvent;
        gameRestEventSo.OnEventRaised -= GameRestEvent;
    }

    public void LoadMenuSceneEvent()
    {
        loadMenuSceneEventSo.RaiseLoadRequestEvent(menuSceneSo,true);
        bg.SetActive(false);
    }

    //游戏结束
    private void GameOverEvent()
    {
        bg.SetActive(true);
        EventSystem.current.SetSelectedGameObject(btnRest);
    }
    //游戏开始
    private void GameRestEvent()
    {
        bg.SetActive(false);
    }
}
