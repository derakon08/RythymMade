
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

    public static float OffSet = 0f;

    public static Input[] Keys = { //I've got no idea, find this in debug file btw
    };

    static GameSettings()
    {
        // Will check settings on initialization
    }

    //static void SetSettings(){}
}