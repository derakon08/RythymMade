public static class Debug
{
    static Debug()
    {
        //Initiazile file access and file variables
    }

    public static void Log(string message)
    {
        Console.WriteLine(message);
        //Log the message to a file
        //And remember to close the file
    }
}