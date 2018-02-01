using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
	public GameObject OtherSphere;
	private bool colliding = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		SphereCollider thisSphereCollider = GetComponent<SphereCollider>();
		SphereCollider otherSphereCollider = OtherSphere.GetComponent<SphereCollider>();
		if (thisSphereCollider == null) throw new ArgumentNullException("thisSphereCollider");
		if (otherSphereCollider == null) throw new ArgumentNullException("otherSphereCollider");

		bool isCol = isColliding(thisSphereCollider, otherSphereCollider);
		if (colliding == isCol) return;
		colliding = isCol;
		Console.WriteLine(colliding ? "It is colliding" : "It is not colliding anymore.");
	}

	private bool isColliding(SphereCollider thisSphereCollider, SphereCollider otherSphereCollider)
	{
		//Two spheres are in contact with each other if and only if
		//the distance between their centers is less than or equal to the sum of their radii.
		
		// Find radius of the two spheres and square it.
		float thisRadius = thisSphereCollider.radius;
		float otherRadius = otherSphereCollider.radius;
		float radiusSquared = (float)Math.Pow(thisRadius + otherRadius, 2);

		float distanceSquared = getDistanceSquared(thisSphereCollider.transform.position, otherSphereCollider.transform.position);
		return distanceSquared < radiusSquared;
	}

	private float getDistanceSquared(Vector3 thisPosition, Vector3 otherPosition)
	{
		return (float) Math.Pow(Vector3.Distance(thisPosition, otherPosition), 2);
	}
}
