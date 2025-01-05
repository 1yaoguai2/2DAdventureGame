
public interface ISaveable
{
    
    void RegisterSaveData() => DataManager.Instance.RegisterSaveData(this);

    void UnRegisterSaveData() => DataManager.Instance.UnRegisterSaveDara(this);

    void GetSaveData(SaveData data);

    void LoadData(SaveData data);
}
