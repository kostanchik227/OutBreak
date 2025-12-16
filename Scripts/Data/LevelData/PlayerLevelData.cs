using System.Numerics;
using static UIColors;

[System.Serializable]
public class PlayerLevelData {
    public int numberLevel;
    public string levelName;
    public Difficulty difficulty;
    public bool passed = false;
    public float minTime = 0f;
    public float maxTime = 0f;
    public int numberAttempts = 0;
    public int numberFirstAttempts = 0;
    public AttemptData statsBestPass;
}
