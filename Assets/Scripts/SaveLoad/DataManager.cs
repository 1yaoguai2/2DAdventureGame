using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;


    [Header("保存事件")] public VoidEventSO saveGameEventSo;
    public VoidEventSO restGameEventSo;
    private List<ISaveable> saveableList = new List<ISaveable>();

    private SaveData _saveData;

    private void Awake()
    {
        Instance = this;
        _saveData = new SaveData();
    }

    private void OnEnable()
    {
        saveGameEventSo.OnEventRaised += Save;
        restGameEventSo.OnEventRaised += Load;
    }

    private void OnDisable()
    {
        saveGameEventSo.OnEventRaised -= Save;
        restGameEventSo.OnEventRaised -= Load;
    }

    // private void Update()
    // {
    //     if (Keyboard.current.pKey.wasPressedThisFrame)
    //     {
    //         Load();
    //     }
    // }

    public void RegisterSaveData(ISaveable saveable)
    {
        if (!saveableList.Contains(saveable))
        {
            saveableList.Add(saveable);
        }
    }

    public void UnRegisterSaveDara(ISaveable saveable)
    {
        if (saveableList.Contains(saveable))
        {
            saveableList.Remove(saveable);
        }
    }

    private void Save()
    {
        foreach (var saveable in saveableList)
        {
            saveable.GetSaveData(_saveData);
        }

        foreach (var item in _saveData.characterPosDict)
        {
            CustomLogger.Log($"ID:{item.Key} Data{item.Value}");
        }
    }

    public void Load()
    {
        foreach (var saveable in saveableList)
        {
            saveable.LoadData(_saveData);
        }
    }
}