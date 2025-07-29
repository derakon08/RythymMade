public class Note
{
    public float Time = 0f;

    public Note(float time)
    {
        this.Time = time;
    }
}

public class FlickerNote : Note//for esthetic purposes
{
    public FlickerNote(float time) : base(time)
    {}
}

public class HeldNote : Note
{
    public float Duration = 0f;

    public HeldNote(float time, float duration) : base(time)
    {
        this.Duration = duration;
    }
}

public class SpamNote : Note
{
    public float Duration = 0f;

    public SpamNote(float time, float duration) : base(time)
    {
        this.Duration = duration;
    }
}