using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataTracker : MonoBehaviour {
	private int[,] dataCounts = new int[3, 6];
	private string fileName;

	private void Awake ( ) {
		// Make a unique file each time the game is played
		fileName = "GameData " + DateTime.Now.TimeOfDay + ".txt";
	}

	/// <summary>
	/// Increment the value of a player's saved data
	/// </summary>
	/// <param name="fromPlayerID">The player index to set the data of. This means that the action was performed by this player ID</param>
	/// <param name="toPlayerID">The player index that the action was applied to</param>
	/// <param name="isRaid">Whether or not this action was a raid or not. If it was not (set to false), then it is assumed that the action was a donate</param>
	public void IncrementPlayerData(int fromPlayerID, int toPlayerID, bool isRaid) {
		// Increment the data value
		dataCounts[fromPlayerID, toPlayerID + (isRaid ? 0 : 3)]++;

		// Log this action to the data log
		string actionString = (isRaid ? "raided" : "donated to");
		LogAction($"Player {fromPlayerID + 1} {actionString} Player {toPlayerID + 1}");
	}

	/// <summary>
	/// Log an action that took place in the game to a text file
	/// </summary>
	/// <param name="output">The line of text to output to the file</param>
	public void LogAction (string output) {
		// This text is added only once to the file.
		if (!File.Exists(fileName)) {
			// Create a file to write to.
			using (StreamWriter sw = File.CreateText(fileName)) {
				sw.WriteLine(output);
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
		LogAction("\nFinal Stats For All Players:\n");

		// LogAction($"Final Stats for Player {playerID}:");
		// LogAction($" > Raided Player 1 {player1raids} times, Donated {player1donations} times");
		// LogAction($" > Raided Player 2 {player2raids} times, Donated {player2donations} times");
		// LogAction($" > Raided Player 3 {player3raids} times, Donated {player3donations} times");
	}
}
