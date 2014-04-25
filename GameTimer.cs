/// <summary>
/// Author: Vijay Poduval
/// Date:	04/27/2014
/// This is a basic timer for a game.
/// This uses DateTime class of C# instead
/// of the Time class in Unity, for more reliability and accuracy 
/// </summary>

using UnityEngine;
using System.Collections;
using System;

public class GameTimer : GlobalStatus {

	// To display the timer
	//public UILabel timer;
	// The duration of the whole game
	public float gameDurationInSec = 300.0f;

	// Variables to store the time remaining in the game
	public static float remainingGameTime;

	// Variables for date
	private DateTime startDate = DateTime.Now;

	// Use this for initialization
	void Start () {

		// Using Sytem date time instead of Time class of Unity

		// Get the current date
		startDate = DateTime.Now;
		// Reduce the delay from the current date
		startDate.AddSeconds(-gameDurationInSec);
		// Update total game duration
		gameDurationInSec = gameDurationInSec - GlobalStatus.gameStartDelay;
		// Initialize the remaining time
		remainingGameTime = gameDurationInSec;

	}
	
	// Update is called once per frame
	void Update () {

		updateTimer();

	}
	void updateTimer()
	{

		// Update the time
		//Get the current time
		DateTime currentDT = DateTime.Now;
		// Get the time elapsed from start of main game
		TimeSpan diffDT = currentDT - startDate;
		// Calculated elapsed time in sec.
		float diffDTSec = (float)(Math.Round(diffDT.TotalSeconds));
		// Update the remaining game time
		remainingGameTime = gameDurationInSec - diffDTSec;


	}
}
