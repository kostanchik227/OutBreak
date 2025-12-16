using TMPro;
using UnityEngine;

public class MenuDisplayLevelData : MonoBehaviour {
    public static MenuDisplayLevelData Instance { get; private set; }
    public TextMeshProUGUI nameLevel;
    public TextMeshProUGUI dataText;

    private void Awake()
    {
        Instance = this;
    }
    public void Display(int levelNumber)
    {
        PlayerLevelData data = LoaderLevelData.Instance.GetLevelData("Level" + levelNumber);
        
        string bestAttempText = "  N/A";
        if (data.numberAttempts > 0) {
            bestAttempText =
                $"  Completed: <color={UIColors.passedColor[data.passed]}>{data.statsBestPass.passed}</color>\n\n\n" +
                $"  Time: {TimeFormatter.Instance.GetFormattedTime(data.statsBestPass.time)}\n\n\n" +
                $"  Bricks Destroyed: {data.statsBestPass.bricksDestroyed}\n\n\n" +
                $"  Bricks Remaining: {data.statsBestPass.bricksNotDestroyed}\n\n\n";
        }
        string formattedText =
            $"Level Name: {data.levelName}\n\n\n" +
            $"Difficulty: <color={UIColors.DifficultyColors[data.difficulty]}>{data.difficulty}</color>\n\n\n" +
            $"Completed: <color={UIColors.passedColor[data.passed]}>{data.passed}</color>\n\n\n" +
            $"Best Time: {TimeFormatter.Instance.GetFormattedTime(data.minTime)}\n\n\n" +
            $"Max Survived: {TimeFormatter.Instance.GetFormattedTime(data.maxTime)}\n\n\n" +
            $"Attempts: {data.numberFirstAttempts}\n\n\n" +
            $"Best Attempt:\n\n\n" +
            $"{bestAttempText}";

        string numberLevel = $"Level {levelNumber}";

        nameLevel.text = numberLevel;
        dataText.text = formattedText;
    }
}
