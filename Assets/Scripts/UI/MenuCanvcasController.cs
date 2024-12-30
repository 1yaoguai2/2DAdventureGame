using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XTools.UI;

public class MenuCanvcasController : MonoBehaviour
{
    public GameObject btnNewGame;

    public Button btnExit;

    private void Start()
    {
        btnExit.onClick.AddListener(Exit);
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(btnNewGame);
    }
    
    private void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}