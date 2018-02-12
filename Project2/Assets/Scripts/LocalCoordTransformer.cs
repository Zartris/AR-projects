using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCoordTransformer : MonoBehaviour {

	public GameObject toTransform;
	private string guiLabel = "Nothing Yet";

	private void OnGUI() {
		GUI.color = Color.black;
		GUI.Label (new Rect (10, 10, 500, 100), guiLabel);
	}

	private bool isWithinBounds(Matrix4x4 result){
		float x = result.m03;
		float z = result.m23;
		float y = result.m13;
		Debug.Log ((Mathf.Pow (x, 2) + Mathf.Pow (z, 2)));
		return (Mathf.Pow (x, 2) + Mathf.Pow (z, 2)) < 0.2f && y < 40;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Matrix4x4 subjectTransform = toTransform.transform.localToWorldMatrix;
		Matrix4x4 inv = transform.localToWorldMatrix.inverse;
		Matrix4x4 res = inv * subjectTransform;
		guiLabel = res.ToString ();
		if(isWithinBounds(res)) {
			if (res.m23 > 0) {
				guiLabel += "\nHemisphere: North";
			} else {
				guiLabel += "\nHemisphere: South";
			}
		}
	}
}
