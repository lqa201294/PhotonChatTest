using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour {
	public Transform target;
	public float smoothtime= 5f;

	Vector3 offset;
	public float distanceCam = 10f;

	// Use this for initialization
	void Start () {
		offset = transform.position - target.position;
		transform.position = new Vector3 (target.position.x, 5f, target.position.z - distanceCam );
	}
	
	// Update is called once per frame
	void LateUpdate () {

		transform.position = Vector3.Lerp (transform.position, target.position + offset, smoothtime * Time.deltaTime );
	}
}
