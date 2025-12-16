using System.IO;
using UnityEngine;

public class LoaderLevelData : MonoBehaviour {
    public static LoaderLevelData Instance { get; private set; }

    private string source;
    private string target;
    private string json;
    private PlayerLevelData data;

    protected void Awake() { Instance = this; }
    public PlayerLevelData GetLevelData(string levelName)
    {
        source = Path.Combine(Application.streamingAssetsPath, "LevelData", levelName + "PlayerData.json");
        target = Path.Combine(Application.persistentDataPath, levelName + "PlayerData.json");
        Debug.Log("Прочитали JSON по пути: " + target);
        if (!File.Exists(target)) {
            File.Copy(source, target);
        }
        json = File.ReadAllText(target);
        data = JsonUtility.FromJson<PlayerLevelData>(json);
        Debug.Log(data.difficulty);
        return data;
    }
}
