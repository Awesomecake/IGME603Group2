using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataTracker : MonoBehaviour {
	static string fileName = "DataTracking.txt";

	[Header("Properties")]
	public int Player1RaidCount = 0;
	public int Player2RaidCount = 0;
	public int Player3RaidCount = 0;
	public int Player1DonateCount = 0;
	public int Player2DonateCount = 0;
	public int Player3DonateCount = 0;

	/// <summary>
	/// Log an action that took place in the game to a text file
	/// </summary>
	/// <param name="output">The line of text to output to the file</param>
	public void LogAction (string output) {
		// This text is added only once to the file.
		if (!File.Exists(fileName)) {
			// Create a file to write to.
			using (StreamWriter sw = File.CreateText(fileName)) {
				sw.WriteLine("First Action : " + output);
			}
		} else {
			// This text is always added, making the file longer over time
			// if it is not deleted.
			using (StreamWriter sw = File.AppendText(fileName)) {
				sw.WriteLine(output);
			}
		}
	}

	public void LogConclusion ( ) {
		// LogAction($"Final Stats for Player {playerID}:");
		// LogAction($" > Raided Player 1 {player1raids} times, Donated {player1donations} times");
		// LogAction($" > Raided Player 2 {player2raids} times, Donated {player2donations} times");
		// LogAction($" > Raided Player 3 {player3raids} times, Donated {player3donations} times");
	}
}
