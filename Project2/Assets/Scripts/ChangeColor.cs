using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
	public GameObject SpaceShip;
	public GameObject Runway;

	private float _dotF = 1337;

	private float _dotR = 1337;

	private bool _collinear = false;
	private double _dot = 0.0;

	private bool _isGreen = false;
	private bool _isXaxisEquelsIsh = false;
	private bool _isZaxisEquelsIsh = false;

	// Use this for initialization

	void Start () {
		
	}

	// Update is called once per frame
	void Update ()
	{
		if(Runway == null)
		{
			// change color to transparent
			GetComponent<Renderer>().material.color = Color.red;
			_isGreen = false;
			return;
		}
		
		// get transformation to gather information about positioning:
		Transform spaceShipTransform = SpaceShip.transform;
		Transform runwayTransform = Runway.transform;
		
		// Forward and Right are normalized
		Debug.DrawRay(spaceShipTransform.position, spaceShipTransform.forward, Color.red, 1);
		Debug.DrawRay(spaceShipTransform.position, spaceShipTransform.right, Color.red, 1);

		Debug.DrawRay(runwayTransform.position, runwayTransform.forward, Color.blue, 1);
		Debug.DrawRay(runwayTransform.position, runwayTransform.right, Color.blue, 1);
		
		// Tolerance is based on some testing.
		var toleranceRotation = 0.003;
		var toleranceDirection= 0.03;
		
		// Checking if the angle is the same, by calculating the dot product of forward and right
		// If the dot product is 1 it means that it is the same direction
		// If the dot product is 0 it has a 90 degree
		// If positive it is to much to the right
		// If negative to much to the left:
		var dotForward = Vector3.Dot(spaceShipTransform.forward, runwayTransform.forward);
		var dotRight = Vector3.Dot(spaceShipTransform.right, runwayTransform.right);
		
		var isXaxisEquelsIsh = Math.Abs(1- dotForward) < toleranceRotation;
		var isZaxisEquelsIsh = Math.Abs(1- dotRight) < toleranceRotation;
		// Logging wise:
		if(isXaxisEquelsIsh != _isXaxisEquelsIsh || isZaxisEquelsIsh != _isZaxisEquelsIsh)
		{
			_isXaxisEquelsIsh = isXaxisEquelsIsh;
			_isZaxisEquelsIsh = isZaxisEquelsIsh;
			Debug.Log("RunwayF: " + runwayTransform.forward);
			Debug.Log("RunwayR: " + runwayTransform.right);
			Debug.Log("SSF: " + spaceShipTransform.forward);
			Debug.Log("SSR: " + spaceShipTransform.right);
			Debug.Log("dotForward: "+dotForward + " and dotRight: " + dotRight);			
		}
		
		// Check if we are aligned with the runway.
		var correctDirection = IsFlylingTheRightDirectionIsh(runwayTransform.position, runwayTransform.position + runwayTransform.right, spaceShipTransform.position, toleranceDirection);
		
		// So to be green you have to have all parameter true:
		// CorrectDirection AND isXaxisEquelsIsh AND isZaxisEquelsIsh.
		
		if(correctDirection && isXaxisEquelsIsh && isZaxisEquelsIsh)
		{
			// change to green
			if(_isGreen) return;
			GetComponent<Renderer>().material.color = Color.green;
			_isGreen = true;
		}
		else if(_isGreen)
		{
			// change to red
			GetComponent<Renderer>().material.color = Color.red;
			_isGreen = false;
		}
		
		
	}
	
	/*
	 * This method is finding the dotproduct between the right direction of the runway
	 * and the vector between the runway and spaceship
	 */
	private bool IsFlylingTheRightDirectionIsh(Vector3 v1, Vector3 v2, Vector3 v3, double tolerance)
	{
		// The runwayTransform.right vector of the runway
		var AB = v2 - v1;
		// The vector between runway and spaceship
		var AC = v3 - v1;
		var dot = Vector3.Dot(AB, AC);
		if(dot != _dot)
		{
			Debug.Log("Change in collinear dot: " + dot + " is in zone: " + (Math.Abs(dot) < tolerance));
			_dot = dot;
		}

		return Math.Abs(dot) < tolerance;
	}
}
