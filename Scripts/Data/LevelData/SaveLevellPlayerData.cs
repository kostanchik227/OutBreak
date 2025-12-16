using System.IO;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;

public class SaveLevellPlayerData : MonoBehaviour
{
    public static SaveLevellPlayerData Instance { get; private set; }
    public string source;
    public string target;
    string json;
    PlayerLevelData data;
    private void Awake()
    {
        Instance = this;
    }

    void Start() { 
        source = Path.Combine(Application.streamingAssetsPath, "LevelData", "Level" + GameManager.Instance.numberScene + "PlayerData.json");
        target = Path.Combine(Application.persistentDataPath, "Level" + GameManager.Instance.numberScene + "PlayerData.json");
        Debug.Log("Сохранили JSON по пути: " + target);
        if (!File.Exists(target)) {
            File.Copy(source, target);
        }

        json = File.ReadAllText(target);
        data = JsonUtility.FromJson<PlayerLevelData>(json);
    }

    public PlayerLevelData GetPlayerLevelData()
    {
        return data;
    }

    public void SaveData(AttemptData passedLevel)
    {
        if ((passedLevel.passed && !data.passed) || (passedLevel.time > data.statsBestPass.time && passedLevel.passed == false && data.passed == false) || (passedLevel.time < data.statsBestPass.time && passedLevel.passed == true && data.passed == true)) {
            data.statsBestPass = passedLevel;
        }
        data.passed = (passedLevel.passed || data.passed);
        if (passedLevel.passed) {
            if (data.minTime == -1 || passedLevel.time < data.minTime) {
                data.minTime = passedLevel.time;
            }
        }
        data.maxTime = Math.Max(passedLevel.time, data.maxTime);
        if (!data.passed || data.numberFirstAttempts == 0) {
            data.numberFirstAttempts++;
        }
        data.numberAttempts++;

        string updatedJson = JsonUtility.ToJson(data, true);
        File.WriteAllText(target, updatedJson);
    }
}
