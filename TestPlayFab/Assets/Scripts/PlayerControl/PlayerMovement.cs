using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

	public GameObject otherplayer;

	// Use this for initialization
	void Start () {
		rig = gameObject.GetComponent<Rigidbody> ();
		otherplayer = GameObject.FindGameObjectWithTag ("otherplayer");

		PhotonNetwork.OnEventCall += this.GetEvent;

	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
	
		movement = new Vector3 (h,0,v);

		if (movement != Vector3.zero) 
		{
			transform.rotation = Quaternion.Slerp (transform.rotation ,Quaternion.LookRotation(movement.normalized),0.1f);

				PhotonPlayer[] otherplayers = PhotonNetwork.otherPlayers;
				int[] idPlayerJoined = new int[otherplayers.Length];

				for(int i= 0; i < otherplayers.Length; i++)
				{
					idPlayerJoined [i] = otherplayers[i].ID;
				}

				PhotonNetwork.RaiseEvent (6, new object []{movement, onGround}, true, new RaiseEventOptions()
					{
						Receivers = ReceiverGroup.All,
						CachingOption= EventCaching.AddToRoomCache,
						TargetActors = idPlayerJoined
					});
		
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
		
	private void GetEvent(byte eventcode, object content, int senderid)
	{
		if (eventcode == 6) 
		{
			object[] data = (object[])content;

			Vector3 otherplayerMove = (Vector3)data [0];
			bool opJump = Convert.ToBoolean (data[1]);

		
				otherplayer.transform.Translate (otherplayerMove * speed * Time.deltaTime, Space.World);

				if (opJump) 
				{
					return;	
				}
				else
				{
					otherplayer.GetComponent<Rigidbody> ().velocity = new Vector3 (0, powerJump * speed * Time.deltaTime, 0);
				}
				
			//PhotonPlayer sender = PhotonPlayer.Find (senderid);
		}

	}

	void OnDisable()
	{
		PhotonNetwork.OnEventCall -= GetEvent;
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Ground") 
		{
			onGround = true;

		}
	}
}
