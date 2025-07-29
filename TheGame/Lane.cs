using System.Collections.Concurrent;

public class Lane
{
	//No more public queue, each lane has its own queue
	//This is horrible, but it works for now lol Yes it does copilot, yes it does
	public Queue<float> PlayerInputs = new();
	public void RunLane(Note[] lane_map, AudioHandler audio, int lane, int bpm) //AudioHandler better be by reference i swear to *BONK*
	{
		float NoteToCheck = -1f;
		int SongMapIterator = 0; //used by MoveToNextNote
		var Tolerance = GameSettings.Tolerance;

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

		while (SongMapIterator < lane_map.Length)
		{
			float SongTranscurred = audio.SpmTime + 0.0167f;  // Assume 60 FPS, increment by ~1 frame per loop (~16.67ms) -chatgpt
			float BeatsTranscurred = SongTranscurred * bpm / 60; //this is the time inside the game, base everything off of this or your eyes pop out

			if (BeatsTranscurred > NoteToCheck + 1.5f)
			{
				//miss lol
				ScoreHandler.AddScore(Accuracy.Miss);
				MoveToNextNote(false);
			}

			if (PlayerInputs.Count != 0) { InputHandler(); }

			void InputHandler()
			{

				//print the input queue length here

				var InputTime = Math.Abs(PlayerInputs.Peek() - NoteToCheck);

				if (InputTime < Tolerance[Accuracy.Impossible])
				{
					ScoreHandler.AddScore(Accuracy.Impossible);
					MoveToNextNote(true);
				}

				else if (InputTime < Tolerance[Accuracy.Very_Good])
				{
					ScoreHandler.AddScore(Accuracy.Very_Good);
					MoveToNextNote(true);
				}

				else if (InputTime < Tolerance[Accuracy.Good])
				{
					ScoreHandler.AddScore(Accuracy.Good);
					MoveToNextNote(true);
				}

				else if (PlayerInputs.Peek() < NoteToCheck + 16f ||
						 PlayerInputs.Peek() < NoteToCheck - 16f)
				{
					ScoreHandler.AddScore(Accuracy.Miss);
					MoveToNextNote(false);
				}
			}

			void MoveToNextNote(bool pop)
			{
				SongMapIterator++;
				if (SongMapIterator < lane_map.Length)
				{
					NoteToCheck = lane_map[SongMapIterator].Time;
				}

				if (pop) { PlayerInputs.Dequeue(); }
			}
		}

		void EndGame()
		{
			Debug.Log($"Shutting lane {lane} down");
			//Signal to main to close song
		}
	}
}
