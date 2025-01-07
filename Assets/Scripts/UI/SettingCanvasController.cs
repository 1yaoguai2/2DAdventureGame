using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingCanvasController : MonoBehaviour
{
    public Button btnSetting;
    public Slider audioSlider;
    public Button btnContinue;
    public Button btnBackEnum;

    private GameObject settingUI;

    public FloatEventSo audioChangedSo;
    public SceneLoadEventSO loadMenuSceneEventSo;
    public GameSceneSO menuSceneSo;

    public AudioMixer audioMixer;

    private void Awake()
    {
        //设置声音同步
        audioMixer.GetFloat("MasterVolume",out float volume);
        audioSlider.value = (volume + 80)/100f;
    }

    private void Start()
    {
        settingUI = transform.GetChild(0).gameObject;
        btnSetting.onClick.AddListener(() =>
        {
            bool isStop = !settingUI.activeInHierarchy;
            settingUI.SetActive(isStop);
            Time.timeScale = isStop ? 0 : 1;
        });
        btnContinue.onClick.AddListener(ContinueGame);
        btnBackEnum.onClick.AddListener(LoadMenuSceneEvent);

        audioSlider.onValueChanged.AddListener(audioChangedSo.RaiseEvent);
    }

    //继续游戏
    private void ContinueGame()
    {
        settingUI.SetActive(false);
        Time.timeScale = 1;
    }

    //回到菜单场景
    private void LoadMenuSceneEvent()
    {
        string currentSceneName = SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name;
        CustomLogger.Log("当前叠加的场景名字为：" + currentSceneName);
        if (!currentSceneName.Equals("Menu"))
            loadMenuSceneEventSo.RaiseLoadRequestEvent(menuSceneSo, true);
        settingUI.SetActive(false);
        Time.timeScale = 1;
    }
}