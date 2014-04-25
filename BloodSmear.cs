/// <summary>
/// Author: Vijay Poduval
/// Date:	04/27/2014
/// This creates a blood smear effect on the screen by interpolating
/// the transparency of the texture.
/// </summary>
using UnityEngine;
using System.Collections;

public class BloodSmear : MonoBehaviour {

	// Variable declaration
	public UITexture blood_texture;

	// Use this for initialization
	void Start () {

		// Set the blood texture transparency to 0
		blood_texture.alpha = 0;
		// Start the co routine for smear effect
		StartCoroutine (blood_smear(blood_texture));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator blood_smear(UITexture blood_texture)
	{
		// Insitialize the beginning and ending values for alpha
		float start_alpha = blood_texture.alpha;
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
					blood_texture.alpha = Mathf.Lerp(start_alpha, final_alpha, currTime/duration );
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

}
