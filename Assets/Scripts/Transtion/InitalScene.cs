using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class InitalScene : MonoBehaviour
{
    public AssetReference persistentScene;

    private void Awake()
    {
        persistentScene.LoadSceneAsync();
    }
}
