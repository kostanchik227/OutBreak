using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SettingsLoader : MonoBehaviour {
    public static SettingsLoader Instance { get; private set; }

    public string source;
    public string target;
    string json;
    SettingsData data;
    private void Awake()
    {
        Instance = this;

        source = Path.Combine(Application.streamingAssetsPath, "Settings", "SettingsData.json");
        target = Path.Combine(Application.persistentDataPath, "SettingsData.json");
        Debug.Log("Сохранили JSON по пути: " + target);
        if (!File.Exists(target)) {
            File.Copy(source, target);
        }

        json = File.ReadAllText(target);
        data = JsonUtility.FromJson<SettingsData>(json);
    }

    public SettingsData GetSettingsData()
    {
        return data;
    }

    public void SaveData(SettingsData dataSettings)
    {
        data = dataSettings;
        string updatedJson = JsonUtility.ToJson(data, true);
        File.WriteAllText(target, updatedJson);
    }
}
