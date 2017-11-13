using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
	
	public float speed;
	public GameObject bulletprefab;
	public GameObject viewFinder;	
	Vector3 offset;

	bulletManage manageBullet;
	lifemanage playerlife;

	private float timeperShoot = 1.5f;
	public float damagebullet = 10f;

	private GameObject otherPlayer;

	void Start()
	{
		
		offset = new Vector3 (0, 0f ,1f);
		manageBullet = FindObjectOfType<bulletManage> ();
		playerlife = FindObjectOfType<lifemanage> ();
		otherPlayer = GameObject.FindGameObjectWithTag ("otherplayer");

		PhotonNetwork.OnEventCall += this.GetEventplayer;
	}

	// Update is called once per frame
	void Update () {
		if (timeperShoot > 0) 
		{
			timeperShoot -= Time.deltaTime;
		}

		if (Input.GetMouseButtonDown (0) && timeperShoot <= 0 && manageBullet.canShoot) 
		{
			timeperShoot = 1.5f;
			manageBullet.decreaseBullet ();
			Vector3 shoot = (viewFinder.transform.position -  transform.position).normalized;
			GameObject projectile = (GameObject)Instantiate (bulletprefab, transform.position + offset, Quaternion.identity);
			projectile.GetComponent<Rigidbody>().AddForce(shoot * speed);

			PhotonPlayer[] otherplayers = PhotonNetwork.otherPlayers;
			int[] idPlayerJoined = new int[otherplayers.Length];

			for(int i= 0; i < otherplayers.Length; i++)
			{
				idPlayerJoined [i] = otherplayers[i].ID;
			}

			PhotonNetwork.RaiseEvent (7, new object []{shoot}, true, new RaiseEventOptions()
				{
					Receivers = ReceiverGroup.All,
					CachingOption= EventCaching.AddToRoomCache,
					TargetActors = idPlayerJoined
				});
		}
	}


	private void GetEventplayer(byte eventcode, object content, int senderid)
	{
		if (eventcode == 7) 
		{
			object[] data = (object[])content;

				Vector3 otherBulletdirection = (Vector3)data [0];
				GameObject enemyBullet = (GameObject)Instantiate (bulletprefab, otherPlayer.transform.position + offset, Quaternion.identity);
				enemyBullet.GetComponent<Rigidbody>().AddForce(otherBulletdirection * speed);




			//PhotonPlayer sender = PhotonPlayer.Find (senderid);
		}
		
	}

	void OnDisable()
	{
		PhotonNetwork.OnEventCall -= GetEventplayer;
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "bullet") 
		{
			playerlife.takeDamage (damagebullet);
		}
	}
}
