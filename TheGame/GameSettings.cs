
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

    /* public static Dictionary<Input, int> KeyBinds{
        {Input.Key('S'), 0},
        {Input.Key('D'), 0},
        {Input.Key('F'), 0},
        {Input.Key('J'), 0},
        {Input.Key('K'), 0},
        {Input.Key('L'), 0},
    } */

    static GameSettings()
    {
        // Will check settings on initialization
    }

    //static void SetSettings(){}
}