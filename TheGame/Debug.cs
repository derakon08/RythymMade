public static class Debug
{

    static Debug()
    {
        //Placeholder class
    }

    public static void Log(string message)
    {
        Console.WriteLine(message);
        //Log the message to a file
        //And remember to close the file
    }
}

public class Input
{
    static Input()
    {
        //Placeholder class
    }


}

public class AudioHandler
{
    public bool SongPlaying = false;
    public float SpmTime = 0.0f;
    public AudioHandler()
    {
        //placeholder class
    }
    
}