using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
	public GameObject SpaceShip;
	public GameObject Runway;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Runway == null)
		{
			// change color to transparent
			GetComponent<Renderer>().material.color = Color.clear;
			return;
		}
		
		// get position:
		Transform spaceShipTransform = SpaceShip.transform;
		Transform runwayTransform = Runway.transform;
		
		// check alignment.
		
		var dot = Vector3.Dot(spaceShipTransform.position, runwayTransform.position);
		
		
		
		Debug.Log(dot);
	}
}
