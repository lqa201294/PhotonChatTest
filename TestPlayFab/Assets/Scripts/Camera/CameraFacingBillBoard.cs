using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacingBillBoard : MonoBehaviour {
	private Camera cam;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (cam != null) 
		{
			gameObject.transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
				cam.transform.rotation * Vector3.up);
		}
	}
}
