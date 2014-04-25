/// <summary>
/// Author: Vijay Poduval
/// Date:	04/27/2014
/// This script is used to draw a 2D frame around 
/// an object using it's bounding box.
/// INPUTS: tracked_object - object to be tracked
///			frame_texture - frame to be drawn around the object's bounding box	
/// OUTPUTS: None
/// </summary>
using UnityEngine;
using System.Collections;


public class BoundingFrame : MonoBehaviour {

	// Public variables
	// Object to be tracked
	public GameObject tracked_object;
	// The 2D frame texture to draw over the object
	public Texture frame_texture;


	// Private variables
	// The co-ordinates of the bounding box
	private float xMax, xMin, yMax, yMin, zMax, zMin;
	private float xMaxW, xMinW, yMaxW, yMinW, zMaxW, zMinW;
	// Co-ordinates of the top plane
	private Vector2 top1, top2, top3, top4;
	// Co-ordinates of the bottom plane
	private Vector2 bot1, bot2, bot3, bot4;
	// Array to store screen co-ordinates of 8 points of the bounding box
	private Vector2[] screen_points = new Vector2[8];



	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{


	}
	void OnGUI()
	{
		// Get the screen co-ordinates
		get_screen_coordinates();
		
		// Get the Minimum and max values according to screen co-ordinates
		get_min_max_screen_coordinates();

		// Draw the rectangular frame if object in front of camera
		// NOTE: We need to reduce with Screen width and height
		// as GUI considers 0,0 as top left of screen, were as the camera considers
		// 0,0 as bottom left of screen.

		// Get the direction to tracked objec
		Vector3 object_direction = tracked_object.transform.position - Camera.main.transform.position;
		// If the object is infront of camera 
		if(Vector3.Dot(Camera.main.transform.forward, object_direction) > 0)
			GUI.DrawTexture(Rect.MinMaxRect(xMin,Screen.height - yMin,xMax,Screen.height - yMax),frame_texture);

		// For debugging
		//GUI.Box(Rect.MinMaxRect(20,20,200,50),Mathf.CeilToInt(xMin)+","+Mathf.CeilToInt(yMin));
		//GUI.Box(Rect.MinMaxRect(20,50,200,80),Mathf.CeilToInt(xMax)+","+Mathf.CeilToInt(yMax));
	}
	/// <summary>
	/// Get_screen_coordinates this instance.
	/// </summary>
	void get_screen_coordinates()
	{
		// Get the max values of bounding box in world co-ordinates
		xMaxW = tracked_object.renderer.bounds.max.x;
		yMaxW = tracked_object.renderer.bounds.max.y;
		zMaxW = tracked_object.renderer.bounds.max.z;
		
		// Get the min values of bounding box in world co-ordinates
		xMinW = tracked_object.renderer.bounds.min.x;
		yMinW = tracked_object.renderer.bounds.min.y;
		zMinW = tracked_object.renderer.bounds.min.z;

		// Get the top rectangle screen co-ordinates
		screen_points[0] = Camera.main.WorldToScreenPoint(new Vector3(xMaxW, yMaxW, zMaxW)); 
		screen_points[1] = Camera.main.WorldToScreenPoint(new Vector3(xMaxW, yMaxW, zMinW)); 
		screen_points[2] = Camera.main.WorldToScreenPoint(new Vector3(xMinW, yMaxW, zMaxW)); 
		screen_points[3] = Camera.main.WorldToScreenPoint(new Vector3(xMinW, yMaxW, zMinW));
		
		// Get the bottom rectangle screen co-ordinates
		screen_points[4] = Camera.main.WorldToScreenPoint(new Vector3(xMaxW, yMinW, zMaxW)); 
		screen_points[5] = Camera.main.WorldToScreenPoint(new Vector3(xMaxW, yMinW, zMinW)); 
		screen_points[6] = Camera.main.WorldToScreenPoint(new Vector3(xMinW, yMinW, zMaxW)); 
		screen_points[7] = Camera.main.WorldToScreenPoint(new Vector3(xMinW, yMinW, zMinW));
	}
	/// <summary>
	/// Gets the top left and bottom left screen co-ordinates.
	/// </summary>
	void get_min_max_screen_coordinates()
	{	
		// Initialize the max and min values
		xMin = screen_points[0].x;
		yMin = screen_points[0].y;
		xMax = screen_points[0].x;
		yMax = screen_points[0].y;

		// Loop through the co-ordinates to find the min and max
		for(int i=1;i<7;i++)
		{	// Get the xMax
			if(screen_points[i].x >  xMax)
				xMax = screen_points[i].x;
			// Get the xMin
			else if(screen_points[i].x <  xMin)
				xMin = screen_points[i].x;
			// Get the yMax
			if(screen_points[i].y >  yMax)
				yMax = screen_points[i].y;
			// Get the yMin
			else if(screen_points[i].y <  yMin)
				yMin = screen_points[i].y;

		}
		// Clamp the values so as not to go outside screen
		Mathf.Clamp(xMin,0, Screen.width);
		Mathf.Clamp(xMax,0, Screen.width);
		Mathf.Clamp(yMin,0, Screen.height);
		Mathf.Clamp(yMax,0, Screen.height);

	}
	/// <summary>
	/// Draws spheres on edges of bounding box.
	/// Enable gizmos in game scene to view it.
	/// </summary>
	void OnDrawGizmosSelected()
	{
		// Set the color of gizmo
		Gizmos.color = Color.yellow;
		// Set the radius
		float radius = 0.05f;

		// Top rectangle of bounding box
		Gizmos.DrawSphere(new Vector3(xMaxW, yMaxW, zMaxW),0.05f);
		Gizmos.DrawSphere(new Vector3(xMaxW, yMaxW, zMinW),radius);
		Gizmos.DrawSphere(new Vector3(xMinW, yMaxW, zMaxW),radius);
		Gizmos.DrawSphere(new Vector3(xMinW, yMaxW, zMinW),radius);

		// Bottom rectangle of bounding box
		Gizmos.DrawSphere(new Vector3(xMaxW, yMinW, zMaxW),radius);
		Gizmos.DrawSphere(new Vector3(xMaxW, yMinW, zMinW),radius);
		Gizmos.DrawSphere(new Vector3(xMinW, yMinW, zMaxW),radius);
		Gizmos.DrawSphere(new Vector3(xMinW, yMinW, zMinW),radius);

	}
}
