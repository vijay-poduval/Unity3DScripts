/// <summary>
/// Author: Vijay Poduval
/// Date:	04/27/2014
/// This function draws a cool down timer in the UI by interpolating the
/// alpha gradient of the texture of the object.
/// </summary>
using UnityEngine;
using System.Collections;

public class CoolDown : MonoBehaviour {

	// Variable declaration
	public float start_value;
	public float final_value;
	public float duration = 10.0f;

	// Use this for initialization
	void Start () {

		// Start the co-routine
		StartCoroutine(alpha_gradient(start_value, final_value, duration));
	
	}
	
	// Update is called once per frame
	void Update () {


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
		StartCoroutine(alpha_gradient(start_value, final_value, duration));
	}
}
