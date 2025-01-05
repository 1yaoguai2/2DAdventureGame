using System;
using UnityEngine;

public class DataDefinition : MonoBehaviour
{
    public PersistentType persistentType;
    public string ID;

    private void OnValidate()
    {
        if (persistentType == PersistentType.ReadWrite)
        {
            if (ID == String.Empty)
                ID = System.Guid.NewGuid().ToString();
        }
        else
            ID = string.Empty;
    }
}