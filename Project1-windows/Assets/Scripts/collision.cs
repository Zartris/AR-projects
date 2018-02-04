using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
	public GameObject OtherSphere;
	private bool colliding = false;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		var component = GetComponent<Transform>();
		var otherComponent = OtherSphere.GetComponent<Transform>();

		bool isCol = isColliding(component, otherComponent);
		if (isCol)
		{
			// To calculate the collision point on the this component.
			Vector3 componentPosition = component.position;
			Vector3 otherComponentPosition = otherComponent.position;
			
			// to find the vector going from this to meteor:
			Vector3 z = otherComponentPosition - componentPosition;

			// We normalize to scale it for the right radius:
			Vector3 zNormalized = z.normalized;
			zNormalized.Scale(new Vector3(component.localScale.x, component.localScale.y, component.localScale.z));

			// adds it to earth position:
			Vector3 collidPoint = componentPosition + zNormalized;

			Debug.Log("It is colliding" + collidPoint.ToString());
		}
	}

	private bool isColliding(Transform component, Transform otherComponent)
	{
		// Since we now it is a complete circle any axis in local will be the radius
		float thisRadius = component.localScale.x;
		float otherRadius = otherComponent.localScale.x;

		//Two spheres are in contact with each other if and only if
		//the distance between their centers is less than or equal to the sum of their radii.

		// Find radius of the two spheres and square it.
		float radiusSquared = (float) Math.Pow(thisRadius + otherRadius, 2);

		float distanceSquared = getDistanceSquared(component.position, otherComponent.position);
		return distanceSquared < radiusSquared;
	}

	private float getDistanceSquared(Vector3 thisPosition, Vector3 otherPosition)
	{
		return (float) Math.Pow(Vector3.Distance(thisPosition, otherPosition), 2);
	}
}