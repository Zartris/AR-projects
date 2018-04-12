using System;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public GameObject SpaceShip;
    public GameObject Runway;

    private float _f1;
    private float _f2;
    private float _f3;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		if (Runway == null)
        {
            // change color to transparent
            GetComponent<Renderer>().material.color = Color.red;
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
        
            Debug.DrawRay(spaceShipTransform.position, runwayTransform.position - spaceShipTransform.position, Color.green, Vector3.Distance(runwayTransform.position, spaceShipTransform.position));
        // Checking if the angle is the same, by calculating the dot product of forward and right
        // If the dot product is 1 it means that it is the same direction
        // If the dot product is 0 it has a 90 degree
        // If positive it is to much to the right
        // If negative to much to the left:
        var dotForward = Vector3.Dot(spaceShipTransform.forward, runwayTransform.forward);
        var dotRight = Vector3.Dot(spaceShipTransform.right, runwayTransform.right);

        var xaxisEquals = Math.Abs(dotForward);
        var zaxisEquels = Math.Abs(dotRight);

        // Check if we are aligned with the runway.    
        var correctDirection = IsFlylingTheRightDirection(runwayTransform.position,
            runwayTransform.position + runwayTransform.right, spaceShipTransform.position);

        // So to be green you have to have all parameter true:
        // CorrectDirection AND isXaxisEquelsIsh AND isZaxisEquelsIsh.
        GetComponent<Renderer>().material.color = CalculateColor(correctDirection, xaxisEquals, zaxisEquels);
    }

    /*
     * This method is finding the dotproduct between the right direction of the runway
     * and the vector between the runway and spaceship
     */
    private float IsFlylingTheRightDirection(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        // The runwayTransform.right vector of the runway
        var AB = (v2 - v1);
        // The vector between runway and spaceship
        var AC = (v3 - v1);
        var result = 1 - Math.Abs(Vector3.Dot(AB, AC));
        return result > 0 ? result : 0;
    }

    private Color CalculateColor(float f1, float f2, float f3)
    {
        // f1, f2 and f3 is a number between 0 and 1. where 1 is the perfect orientasion.
        // so we find the avg of these and make the color from this.
        var avg = f1 * f2 * f3;
        var green = avg;
        var red = 1 - green;
        if (_f1 != f1 || _f2 != f2 || _f3 != f3)
        {
            Debug.Log("f1: " + f1 + " f2: " + f2 + " f3: " + f3);
            Debug.Log("avg: " + avg + " green: " + green + " red: " + red);
            _f1 = f1;
            _f2 = f2;
            _f3 = f3;
        }

        return new Color(red, green, 0);
    }
}