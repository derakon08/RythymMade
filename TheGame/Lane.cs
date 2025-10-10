public class Lane //Destroy me if no map is playing
{
	//Info variables
	readonly float impossible = GameSettings.Tolerance[Accuracy.Impossible];
	readonly float veryGood = GameSettings.Tolerance[Accuracy.Very_Good];
	readonly float good = GameSettings.Tolerance[Accuracy.Good];
	readonly AudioHandler audio; //Fix once we're in unity
	readonly Note[] map;
	readonly int BPM;

	float totalBeats;
	Note nextNote;

	int mapIterator = -1;
	private bool forceEnd = false;
	private bool isHeld = false;

	public void Update() //of sorts
	{

		if (audio.SongPlaying && !forceEnd)
		{
			totalBeats = audio.SpmTime * BPM / 60;

			if (
			(!isHeld && totalBeats > nextNote.time + good) ||
			(isHeld && totalBeats > nextNote.time + nextNote.duration + good))
			{
				ScoreHandler.AddScore(Accuracy.Miss);
				MapIterator();
			}
		}

		//else Game.EndGame();
	}

	public void Trigger(bool isKeyUp = false)
	{
		if (nextNote.time - totalBeats > 3f) { return; }


		if (nextNote.duration == 0)
		{
			Score();
			MapIterator();
			return;
		}
		else
        {
            if (isKeyUp)
            {
                
            }
        }
        
    }

	private void Score()
	{
		float inputTime = Math.Abs(totalBeats - nextNote.time);

		if (inputTime <= impossible)
		{
			ScoreHandler.AddScore(Accuracy.Impossible);
		}
		else if (inputTime <= veryGood)
		{
			ScoreHandler.AddScore(Accuracy.Very_Good);
		}
		else if (inputTime <= good)
		{
			ScoreHandler.AddScore(Accuracy.Good);
		}
		else
		{
			ScoreHandler.AddScore(Accuracy.Miss);
		}
	}

	private void MapIterator()
	{
		mapIterator++;

		if (mapIterator + 1 > map.Length) //end after this note
		{
			forceEnd = true;
		}

		nextNote = map[mapIterator];

		return;
	}

	public Lane(Note[] map, AudioHandler audio, int bpm)
	{
		if (map.Length < 1)
		{
			forceEnd = true;
			Debug.Log("At least one lane is empty");
		}
		else
		{
			this.audio = audio; //Fix once we're in unity
			this.map = map;
			BPM = bpm;

            MapIterator();
        }
	}
}

