public static class ScoreHandler
{
    private static int Score = 0;

    static ScoreHandler()
    {
        Score = 0;
    }

    public static void AddScore(Accuracy acc)
    {
        Score + acc;
    }
}