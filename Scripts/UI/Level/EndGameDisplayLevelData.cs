using TMPro;
using UnityEngine;

public class EndGameDisplayLevelData : MonoBehaviour
{
    public TextMeshProUGUI dataText;
    public void DisplayEndGameMenu(AttemptData levelData, int levelNumber)
    {
        PlayerLevelData data = LoaderLevelData.Instance.GetLevelData("Level" + levelNumber);

        string formattedText =
            $"Level Name: {data.levelName}\n\n\n" +
            $"Difficulty: <color={UIColors.DifficultyColors[data.difficulty]}>{data.difficulty}</color>\n\n\n\n\n\n" +
            $"Time completion: {TimeFormatter.Instance.GetFormattedTime(levelData.time)}\n\n\n" +
            $"Bricks Destroyed: {levelData.bricksDestroyed}\n\n\n" +
            $"Bricks Remaining: {levelData.bricksNotDestroyed}\n\n\n" +
            $"Attempts: {data.numberAttempts}";
        dataText.text = formattedText;
    }
}
