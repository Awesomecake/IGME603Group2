using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataTracker : MonoBehaviour {
	private int[,] dataCounts = new int[3, 6];
	private string fileName;

	private int firstRaider = -1;
	private int firstRaidDay = -1;
	private int firstDonor = -1;
	private int firstDonationDay = -1;
	private bool hasConcluded = false;

	private void Awake ( ) {
		// Make a unique file each time the game is played
		fileName = $"GameData-{DateTime.Now.TimeOfDay}".Replace(':', '-').Replace('.', '-') + ".txt";
	}

	/// <summary>
	/// Increment the value of a player's saved data
	/// </summary>
	/// <param name="fromPlayerID">The player index to set the data of. This means that the action was performed by this player ID</param>
	/// <param name="toPlayerID">The player index that the action was applied to</param>
	/// <param name="isRaid">Whether or not this action was a raid or not. If it was not (set to false), then it is assumed that the action was a donate</param>
	public void IncrementPlayerData(int fromPlayerID, int toPlayerID, bool isRaid) {
		// Get a reference to the player ID that was the first raider or the first donor
		if (firstRaider == -1 && isRaid) {
			firstRaider = fromPlayerID;
			firstRaidDay = GetComponent<GameManager>( ).DayCount;
		}
		if (firstDonor == -1 && !isRaid) {
			firstDonor = fromPlayerID;
			firstDonationDay = GetComponent<GameManager>( ).DayCount;
		}

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

	/// <summary>
	/// Log a bunch of calculated data about the game the player's just played
	/// </summary>
	public void LogConclusion ( ) {
		if (hasConcluded) {
			return;
		}

		LogAction("\nFinal Stats For All Players:");

		// Log information pertaining to player raids
		int player1Raids = dataCounts[0, 0] + dataCounts[0, 1] + dataCounts[0, 2];
		int player2Raids = dataCounts[1, 0] + dataCounts[1, 1] + dataCounts[1, 2];
		int player3Raids = dataCounts[2, 0] + dataCounts[2, 1] + dataCounts[2, 2];
		int totalRaids = player1Raids + player2Raids + player3Raids;
		LogAction($"\nThere were a total of {totalRaids} raids between all players.");
		LogAction($"Player 1 raided {player1Raids} other player{(player1Raids == 1 ? char.MinValue : 's')}.");
		LogAction($"Player 2 raided {player2Raids} other player{(player2Raids == 1 ? char.MinValue : 's')}.");
		LogAction($"Player 3 raided {player3Raids} other player{(player3Raids == 1 ? char.MinValue : 's')}.");
		if (firstRaider != -1) {
			LogAction($"Player {firstRaider + 1} was the first to raid another player.");
			LogAction($"It took {firstRaidDay - 1} day{(firstRaidDay == 2 ? char.MinValue : 's')} before a player raided another player.");
		}

		// Log information pertaining to player donations
		int player1Donations = dataCounts[0, 3] + dataCounts[0, 4] + dataCounts[0, 5];
		int player2Donations = dataCounts[1, 3] + dataCounts[1, 4] + dataCounts[1, 5];
		int player3Donations = dataCounts[2, 3] + dataCounts[2, 4] + dataCounts[2, 5];
		int totalDonations = player1Donations + player2Donations + player3Donations;
		LogAction($"\nThere were a total of {totalDonations} donations between all players.");
		LogAction($"Player 1 donated to {player1Donations} other player{(player1Donations == 1 ? char.MinValue : 's')}.");
		LogAction($"Player 2 donated to {player2Donations} other player{(player2Donations == 1 ? char.MinValue : 's')}.");
		LogAction($"Player 3 donated to {player3Donations} other player{(player3Donations == 1 ? char.MinValue : 's')}.");
		if (firstDonor != -1) {
			LogAction($"Player {firstDonor + 1} was the first to donate to another player.");
			LogAction($"It took {firstDonationDay - 1} day{(firstDonationDay == 2 ? char.MinValue : 's')} before a player donated to another player.");
		}

		// Log the players' score
		float score = totalRaids / Mathf.Max(1, totalDonations);
		if (firstRaider != -1) {
			score -= Mathf.Exp(-(firstRaidDay - 1));
		}
		if (firstDonor != -1) {
			score += Mathf.Exp(-(firstDonationDay - 1));
		}
		LogAction($"\nThe players' overall score is: {score}.");
		if (score < 1) {
			LogAction($"A score of less than 1 indicates that players were generally friendlier towards each other and tried to work together more than they tried to raid each other.\n");
		} else {
			LogAction($"A score of greater than 1 indicates that players were generally more hostile and sought to raid other players for their own gain rather than help others.\n");
		}

		hasConcluded = true;
	}
}
