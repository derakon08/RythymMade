public class Lane
{
	//Don't mind me
	const bool yes = true;
	const bool no = false;

	//Info variables
	readonly Dictionary<Accuracy, float> Tolerance = GameSettings.Tolerance;
	readonly AudioHandler Audio; //Fix once we're in unity
	readonly Note[] LaneMap; //Note.Time is in absolute beats, so if i get confused again i'm gonna throw a fit.
	//In the map, the note is obviously measured in absolute, otherwise i wouldn't know where it goes.
	readonly int BPM;
	readonly int LaneNumber;

	public Queue<float> PlayerInputs = new(); //So it's gonnna be Time, but then how do i check if they released the key???????? AHG
	float NoteToCheck = -1f;
	int SongMapIterator = 0; //used by MoveToNextNote
	
	public Lane(int lane, Note[] lane_map, AudioHandler audio, int bpm)
	{
		try
		{
			NoteToCheck = lane_map[0].Time;
		}

		catch (IndexOutOfRangeException ex)
		{
			Debug.Log("Empty Map for lane " + lane + ": " + ex.Message);
			Debug.Log("Shutting Game Down"); //This should be logged outside the lanes script
			EndGame();
		}

		LaneNumber = lane;
		LaneMap = lane_map;
		Audio = audio;
		BPM = bpm;
	}

	public void RunLane() //AudioHandler better be by reference i swear to *BONK*
	{

		while (SongMapIterator <= LaneMap.Length)
		{
			float SongTranscurred = Audio.SpmTime + 0.0167f;  // Assume 60 FPS, increment by ~1 frame per loop (~16.67ms) -chatgpt
			float BeatsTranscurred = SongTranscurred * BPM / 60; //this is the time inside the game, base everything off of this or your eyes pop out

			if (BeatsTranscurred > NoteToCheck + 1.5f && PlayerInputs.Count != 0)
			{
				//miss lol
				ScoreHandler.AddScore(Accuracy.Miss);
				MoveToNextNote(no);
			}

			if (PlayerInputs.Count != 0)
			{
				//print the input queue length here
				var CurrentNote = LaneMap[SongMapIterator];

				switch (CurrentNote)
				{
					case HeldNote:
						HeldHandler();
						break;

					case SpamNote:
						SpamHandler();
						break;

					default:
						NormalHandler();
						break;
				}
			}
		}

		EndGame();
	}

	private void NormalHandler()
	{
		var InputTime = Math.Abs(PlayerInputs.Peek() - NoteToCheck);

		if (InputTime < Tolerance[Accuracy.Impossible])
		{
			ScoreHandler.AddScore(Accuracy.Impossible);
			MoveToNextNote(yes);
		}

		else if (InputTime < Tolerance[Accuracy.Very_Good])
		{
			ScoreHandler.AddScore(Accuracy.Very_Good);
			MoveToNextNote(yes);
		}

		else if (InputTime < Tolerance[Accuracy.Good])
		{
			ScoreHandler.AddScore(Accuracy.Good);
			MoveToNextNote(yes);
		}

		else if (PlayerInputs.Peek() < NoteToCheck + 16f ||
				 PlayerInputs.Peek() > NoteToCheck - 16f)
		{
			ScoreHandler.AddScore(Accuracy.Miss);
			MoveToNextNote(yes);
		}
	}

	private void HeldHandler()
	{
		/* var InputTime = Math.Abs(PlayerInputs.Peek() - NoteToCheck);

		if (InputTime < Tolerance[Accuracy.Impossible])
		{
			ScoreHandler.AddScore(Accuracy.Impossible);
			MoveToNextNote(yes);
		}

		else if (InputTime < Tolerance[Accuracy.Very_Good])
		{
			ScoreHandler.AddScore(Accuracy.Very_Good);
			MoveToNextNote(yes);
		}

		else if (InputTime < Tolerance[Accuracy.Good])
		{
			ScoreHandler.AddScore(Accuracy.Good);
			MoveToNextNote(yes);
		}

		else if (PlayerInputs.Peek() < NoteToCheck + 16f ||
				 PlayerInputs.Peek() > NoteToCheck - 16f)
		{
			ScoreHandler.AddScore(Accuracy.Miss);
			MoveToNextNote(yes);
		} */
	}

	private void SpamHandler()
	{
		float SongTranscurred = Audio.SpmTime + 0.0167f;  // Assume 60 FPS, increment by ~1 frame per loop (~16.67ms) -chatgpt
		float BeatsTranscurred = SongTranscurred * BPM / 60; //this is the time inside the game, base everything off of this or your eyes pop out
		HeldNote CurrentNote = (HeldNote)LaneMap[SongMapIterator];

		while (BeatsTranscurred < CurrentNote.Time + CurrentNote.Duration)
		{
			if (PlayerInputs.Count != 0) // || PlayerInputs.Peek() != Input.KeyUp or such
			{
				//Avoid checking for key release
				ScoreHandler.AddScore(Accuracy.Impossible);
				MoveToNextNote(true, true);	
			}
		}
	}

	void MoveToNextNote(bool pop, bool Hold = false)
	{
		if (!Hold)
		{
			SongMapIterator++;
			NoteToCheck = LaneMap[SongMapIterator].Time;
		}

		if (pop) { PlayerInputs.Dequeue(); }
	}

	void EndGame()
	{
		Debug.Log($"Shutting lane {LaneNumber} down");
		//Signal to main to close song
	}
}

