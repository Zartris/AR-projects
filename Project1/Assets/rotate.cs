using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {
	//public GameObject sun;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//transform.RotateAround (sun.transform.position, Vector3.up, 20 * Time.deltaTime);
		transform.Rotate (Vector3.up);
	}

}


