using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public interface IDefaultDataModel
{
    public bool IsEmpty();
    public void SetDefault();
}

public class DataAsset<T> : ScriptableObject where T: struct, IDefaultDataModel
{
    // Check file exist function
    private bool IsFileExist(string filePath)
    {
        return File.Exists(filePath) && new FileInfo(filePath).Length > 0;
    }

    private string GetFilePath(string filename)
    {
        return Path.Combine(Application.persistentDataPath, filename);
    }
    protected void SaveData(string filename, T model)
    {
        string filePath = GetFilePath(filename);

        // TODO
        // if (!IsFileExist(filePath))
        // {
        //     model = new T();
        //     model.SetDefault();
        // }
        
        string data = JsonConvert.SerializeObject(model);
        File.WriteAllText(filePath, data);
    }
    
    protected void LoadData(string filename, out T model)
    {
        string filePath = GetFilePath(filename);

        if (IsFileExist(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            model = JsonConvert.DeserializeObject<T>(jsonData);
            if (model.IsEmpty())
            {
                model = new T();
                model.SetDefault();
            }
        }
        else
        {
            model = new T();
            model.SetDefault();
        }
    }
    
}
public abstract class BaseDataAsset<T>: DataAsset<T> where T: struct, IDefaultDataModel // Model
{
    [SerializeField] private string _filename;
    [SerializeField] protected T _model;
    
    
    protected void SaveData()
    {
        base.SaveData(_filename, _model);
    }
    
    public void LoadData()
    {
        base.LoadData(_filename, out _model);
    }

}


