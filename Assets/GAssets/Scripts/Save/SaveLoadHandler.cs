using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class SaveLoadHandler : MonoBehaviour
{
    private List<ISavable> _savables = new List<ISavable>();
    private string SaveFilePath = Application.streamingAssetsPath + "/savefile.json";

    private bool _isSave = true;

    public void RegisterSavable(ISavable savable)
    {
        if(!_savables.Contains(savable))
            _savables.Add(savable);
    }
    private void OnApplicationQuit()
    {
        if(!_isSave) return;
        SaveAll();
    }

    /// <summary>
    /// Asks all ISavable 's to send data
    /// </summary>
    public void SaveAll()
    {
        var saveData = new Dictionary<string, string>();
        foreach (var savableObject in _savables)
        {
            saveData[savableObject.GetType().Name] = savableObject.Save();
        }

        File.WriteAllText(SaveFilePath, JsonConvert.SerializeObject(saveData));
    }

    public void LoadAll()
    {
        if (!File.Exists(SaveFilePath)) return;
       
        var saveData = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(SaveFilePath));
        foreach (var savableObject in _savables)
        {
            var typeName = savableObject.GetType().Name;
            if (saveData.TryGetValue(typeName, out var jsonData))
            {
                savableObject.Load(jsonData);
            }
        }
        _isSave = true;
    }
    public void DeleteSaveFile()
    {
        if (!File.Exists(SaveFilePath)) return;
        File.Delete(SaveFilePath);

        _isSave = false;
    }
    public bool HasSaveFile()
    {
        return File.Exists(SaveFilePath);
    }
}

