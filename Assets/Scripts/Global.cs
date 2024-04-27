public enum Difficulties {
    Easy = 1,
    Medium = 2,
    Hard = 3,
}

public static class GlobalData {
    public static int Level = 1;
    public static float Score = 0;
    public static float Damage = 0;
    public static float Deaths = 0;
    public static float Healing = 0;
    public static float Enemies = 0;
    public static float LevelMultiplier = 1;
    public static float DifficultyMultiplier = 1;
    public static Difficulties Difficulty = Difficulties.Easy;

    public static string RemoveDotZeroZero(string input) {
        if (input.EndsWith(".00")) {
            return input.Substring(0, input.Length - 3);  // Remove the last three characters
        }
        return input;
    }
}