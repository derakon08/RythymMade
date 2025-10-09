public class Lane //Destroy me if no map is playing
{
	//Info variables
	readonly float impossible = GameSettings.Tolerance[Accuracy.Impossible];
	readonly float veryGood = GameSettings.Tolerance[Accuracy.Very_Good];
	readonly float good = GameSettings.Tolerance[Accuracy.Good];
	readonly AudioHandler audio; //Fix once we're in unity
	readonly Note[] map;
	readonly int BPM;

 	//used by MapIterator
	int mapIterator = -1;
	private bool forceEnd = false;

	public Lane(Note[] map, AudioHandler audio, int bpm)
	{
		if (map.Length < 0) Debug.Log("At least one lane is empty");
		this.audio = audio; //Fix once we're in unity
		this.map = map;
		BPM = bpm;
	}

	public void updaet() //of sorts
	{

		while (audio.SongPlaying && !forceEnd)
		{
			float totalBeats = audio.SpmTime * BPM / 60;
			var nextNote = MapIterator();

			if (totalBeats > nextNote.Time + 1.5f)
			{
				//miss lol
				ScoreHandler.AddScore(Accuracy.Miss);
				MapIterator();
			}
		

			

				
			
		}

		//Game.EndGame();
	}

	private void Score(float currentBeat, float noteToCheck)
	{
		float inputTime = Math.Abs(currentBeat - noteToCheck);

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

	private Note MapIterator()
	{
		mapIterator++;

		if (mapIterator + 1 > map.Length) //end after this note
		{
			forceEnd = true;
		}

		return map[mapIterator];
	}
}

