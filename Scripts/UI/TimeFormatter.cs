using UnityEngine;

public class TimeFormatter : MonoBehaviour
{
    public static TimeFormatter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public string GetFormattedTime(float time)
    {
        if (time == -1f)
            return "N/A";

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);

        return $"{minutes:00}:{seconds:00}<size=70%>.{milliseconds:000}</size>";
    }
}
