/// <summary>
/// Author: Vijay Poduval
/// Date:	05/03/2014
/// This script is make an object move in a sine wave pattern (snake like).
/// INPUTS: amplitude - of the sine wave (hieght of the wave)
/// 		frequency - frequency (length of the wave)
/// 		delay	  - the delay of the movement in seconds
/// 		start_position - starting position/time of the sinewave
/// 		duration - the amount of time/postion the wave should be done
/// OUTPUTS: None
/// </summary>
using UnityEngine;
using System.Collections;

public class SineWave : MonoBehaviour {


	// public members
	public float amplitude = 10.0f;
	public float frequency = 10.0f;
	public float delay = 0.3f;
	public float start_position = -5.0f; 
	public float duration = 10.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine(sine_wave());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator sine_wave()
	{

		// Initialize the start position
		float curr_position = start_position;
		// Set the x,y postion of the object
		float x = start_position;
		float y = 0.0f;
		
		// Loop till the current postion is less than the duration (distance) 
		while( curr_position < duration	)
		{
			// Change the y position according to a sine wave
			y = amplitude * Mathf.Sin(curr_position);
			// Translate in x -direction
			x = frequency * curr_position;
			// Change the postion of the transform
			transform.localPosition = new Vector3(x,y,0.0f);
			// Update the current postion
			curr_position += frequency * delay;
			// Wait for a few seconds
			yield return new WaitForSeconds(delay);
		}
	}
}
