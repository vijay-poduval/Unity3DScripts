/// <summary>
/// Author: Vijay Poduval
/// Date:	04/27/2014
/// This draws a timer countdown bar using the transparency 
/// of a texture.
/// </summary>

using UnityEngine;
using System.Collections;
using System;

public class TimerController : MonoBehaviour {

	// Variable declaration
	public float start_value;
	public float final_value = 1;
	public float duration = 10.0f;
	public UILabel timerLabel;
	
	// Use this for initialization
	void Start () {

		// Set the start times 
		float currentTime = GameTimer.remainingGameTime;
		// Get the time duration of game
		float totalTime = ManageGameTime.entireGameTimeSec * 1.0000f;
		// get the starting alpha value (start to final (1))
		start_value = (1.0000f - currentTime / totalTime);
		// Time left
		duration = GameTimer.remainingGameTime;
		// Start the co-routine
		StartCoroutine(alpha_gradient(start_value, final_value, duration));
		
	}
	
	// Update is called once per frame
	void Update () {
		
		setLabelText();
	}
	IEnumerator alpha_gradient(float start_value, float final_value, float duration)
	{
		// Set the timer and duration
		float cur_time = 0.0f;
		// Get the ratio
		float cut_off = (float) ((cur_time / duration));
		// Lerp from start to final
		while(cur_time < duration)
		{
			// Update the current time
			cur_time += Time.deltaTime;
			// Update the ratio / cutoff
			cut_off = (float) ((cur_time / duration));
			// Update the alpha value cutoff 
			renderer.material.SetFloat("_Cutoff", Mathf.Lerp(start_value, final_value, cut_off));
			// return the control to update
			yield return null;
		}
	}
	void OnEnable()
	{

		// Get the current game time and update the start value
		float currentTime = GameTimer.remainingGameTime;
		float totalTime = ManageGameTime.entireGameTimeSec * 1.0000f;
		// get the starting alpha value (start to final (1))
		start_value = (1.0000f - currentTime / totalTime);
		// Time left
		duration = GameTimer.remainingGameTime;
		//Start the co-routine
		StartCoroutine(alpha_gradient(start_value, final_value, duration));
	}

	void setLabelText()
	{
		// Get the seconds and minutes from the reamining time
		float displaySeconds = GameTimer.remainingGameTime % 60;
		float displayMinutes = GameTimer.remainingGameTime / 60; 		
		// Update the timer UI label
		string timetext = Mathf.FloorToInt(displayMinutes) + " : " + Mathf.FloorToInt(displaySeconds); 
		timerLabel.text = timetext;

	}

}
