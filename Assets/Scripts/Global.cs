public enum Difficulties {
    Easy = 1,
    Medium = 2,
    Hard = 3,
}

public static class GlobalData {
    public static int Level = 1;
    public static float Score = 0;
    public static float Damage = 0;
    public static string RemoveDotZeroZero(string input) {
        if (input.EndsWith(".00")) {
            return input.Substring(0, input.Length - 3);  // Remove the last three characters
        }
        return input;
    }
}