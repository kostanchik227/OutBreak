using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButtonsCreater : MonoBehaviour
{
    public int numberOfLevels = 0;
    public GameObject levelButtonPrefab;
    public Vector3 positionButton = Vector3.zero;
    public MenuDisplayLevelData textMenu;

    private void Start()
    {
        for (int i = 0; i < numberOfLevels; i++) {
            int levelIndex = i+1;

            PlayerLevelData data = LoaderLevelData.Instance.GetLevelData("Level" + (levelIndex));

            GameObject button = Instantiate(levelButtonPrefab, transform);
            button.transform.localPosition = positionButton;

            string levelName = $"{levelIndex} level - {data.levelName}";
            string difficulty = $"<color={UIColors.DifficultyColors[data.difficulty]}>{data.difficulty}</color>";
            
            button.transform.Find("NumberLevel").GetComponent<TextMeshProUGUI>().text = levelName;
            button.transform.Find("Difficulty").GetComponent<TextMeshProUGUI>().text = difficulty;
            if (data.passed) {
                button.transform.Find("IsCompleted").GetComponent<Image>().color =
                     ColorUtility.TryParseHtmlString("#99e550", out var c) ? c : Color.white;
            }else if (data.numberAttempts > 0) {
                button.transform.Find("IsCompleted").GetComponent<Image>().color =
                     ColorUtility.TryParseHtmlString("#d95763", out var c) ? c : Color.white;
            }

                button.GetComponent<LevelButtonController>().numberLevel = levelIndex;



            positionButton.y -= 75;
        }
    }
}
