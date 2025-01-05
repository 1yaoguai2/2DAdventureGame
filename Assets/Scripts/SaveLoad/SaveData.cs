using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public string sceneToSave;
    public Dictionary<string, Vector3> characterPosDict = new Dictionary<string, Vector3>();
    public Dictionary<string, float> floatDataDict = new Dictionary<string, float>();

    public void SaveCurrentScene(GameSceneSO sceneSo)
    {
        sceneToSave = JsonUtility.ToJson(sceneSo);
        CustomLogger.Log($"保存场景{sceneSo}To{sceneToSave}");
    }

    public GameSceneSO GetSavedScene()
    {
        var newScene = ScriptableObject.CreateInstance<GameSceneSO>();
        JsonUtility.FromJsonOverwrite(sceneToSave, newScene);
        return newScene;
    }
}