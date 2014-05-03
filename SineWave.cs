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


		float curr_time = start_position;
		float x = start_position;
		float y = 0.0f;

		while( curr_time < duration	)
		{
			y = amplitude * Mathf.Sin(curr_time);
			// Un-Comment this section inorder to make it translate in x -direction
			//x = frequency * curr_time;
			transform.localPosition = new Vector3(x,y,0.0f);
			curr_time += frequency * delay;
			yield return new WaitForSeconds(delay);
		}
	}
}
