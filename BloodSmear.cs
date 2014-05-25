﻿/// <summary>
/// Author: Vijay Poduval
/// Date:	04/27/2014
/// This creates a blood smear effect on the screen by interpolating
/// the transparency of the texture.
/// </summary>
using UnityEngine;
using System.Collections;

public class BloodSmear : MonoBehaviour {

	// Variable declaration
	public GUITexture blood_texture;

	// Color of texture
	private Color texture_color; 
	private float texture_alpha;

	// Use this for initialization
	void Start () {

		// Get the color of the texture
		texture_color = blood_texture.color;
		// Set the blood texture transparency to 0
		texture_alpha = 0.0f;
		// Start the co routine for smear effect
		StartCoroutine (blood_smear());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator blood_smear()
	{
		// Insitialize the beginning and ending values for alpha
		float start_alpha = texture_alpha;
		float final_alpha = 1.0f;
		//Set the time duration
		float currTime = 0.0f;
		float duration = 2.0f;
		// temp variable for swapping the start and end transparencies
		float temp = 0.0f;
		// No of cycles for the smear effect to run
		int num_cycles = 4;

		for(int c =0; c < num_cycles; c++)
		{
			// Run two times from start - final and back
			for (int i=0; i<2 ; i++)
			{
				while(currTime < duration)
				{
					// Update the current time
					currTime += Time.deltaTime;
					// Inetrpolate between start and final alphas
					texture_alpha = Mathf.Lerp(start_alpha, final_alpha, currTime/duration );

					// Return control to update
					yield return null;
				}
				// Reset the timer
				currTime = 0.0f;
				// Swap the start and final alphas
				temp = start_alpha;
				start_alpha = final_alpha;
				final_alpha = temp;
			}
		}
	}
	void OnGUI()
	{
		blood_texture.pixelInset.center.Set(0.0f,0.0f);
		blood_texture.pixelInset = new Rect(-Screen.width/2,-Screen.height/2,Screen.width,Screen.height);
		blood_texture.color  = new Color(texture_color.r,texture_color.g,texture_color.b,texture_alpha);
		//GUI.DrawTexture(Rect.MinMaxRect(0,0,Screen.width,Screen.height),blood_texture.texture);

	}

}
