using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float speed = 6f;
	public float powerJump = 100f;
	Vector3 movement;
	Vector3 Limitmovement;

	Rigidbody rig;
	bool onGround;

	Quaternion camRotation;

	public float xMax;
	public float xMin;
	public float zMax;
	public float zMin;
	public float ymin;
	public float ymax;

	// Use this for initialization
	void Start () {
		rig = gameObject.GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
	
		movement = new Vector3 (h,0,v);

		if (movement != Vector3.zero) 
		{
			transform.rotation = Quaternion.Slerp (transform.rotation ,Quaternion.LookRotation(movement.normalized),.1f);
		}
			
		transform.Translate (movement * speed * Time.deltaTime, Space.World);

		Limitmovement.x = Mathf.Clamp (transform.position.x, xMin, xMax);
		Limitmovement.z = Mathf.Clamp (transform.position.z, zMin, zMax);
		Limitmovement.y = Mathf.Clamp (transform.position.y, ymin,ymax);

		transform.position = Limitmovement;

		if (Input.GetKeyDown (KeyCode.Space) && onGround)
		{
			onGround = false;
			rig.velocity = new Vector3 (0f, powerJump * speed * Time.deltaTime, 0f);
			print ("press jump");
		}
	

	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Ground") 
		{
			onGround = true;

		}
	}
}
