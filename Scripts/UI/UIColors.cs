using System.Collections.Generic;

public static class UIColors {
    public enum Difficulty {
        Beginner,
        Easy,
        Normal,
        Challenging,
        Hard,
        Expert,
        Master,
        Insane,
        Impossible
    }

    public static readonly Dictionary<Difficulty, string> DifficultyColors = new Dictionary<Difficulty, string>()
    {
        {Difficulty.Beginner, "#99e550"},
        {Difficulty.Easy, "#6abe30"},
        {Difficulty.Normal, "#37946e"},
        {Difficulty.Challenging, "#5fcde4"},
        {Difficulty.Hard, "#639bff"},
        {Difficulty.Expert, "#5b6ee1"},
        {Difficulty.Master, "#d77bba"},
        {Difficulty.Insane, "#d95763"},
        {Difficulty.Impossible, "#ac3232"}
    };

    public static readonly Dictionary<bool, string> passedColor = new Dictionary<bool, string>()
    {
        {true, "green"},
        {false, "red"}
    };
}