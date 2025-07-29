
public static class GameSettings
{
    private static float ToleranceGood = 0.1f;
    private static float ToleranceVeryGood = 0.05f;
    private static float ToleranceImpossible = 0.01f;

    public static Dictionary<Accuracy, float> Tolerance = new()
    {
        { Accuracy.Good, ToleranceGood },
        { Accuracy.Very_Good, ToleranceVeryGood },
        { Accuracy.Impossible, ToleranceImpossible }
    };

    static GameSettings()
    {
        // Will check settings on initialization
    }

    //static void SetSettings(){}
}