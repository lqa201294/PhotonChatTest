using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
	public float speed;
	public GameObject bulletprefab;
	public GameObject viewFinder;	
	Vector3 offset;

	private float timeperShoot =1.5f;

	void Start()
	{
		offset = new Vector3 (0, 1f ,1f);

	}
	// Update is called once per frame
	void Update () {
		if (timeperShoot > 0) 
		{
			timeperShoot -= Time.deltaTime;
		}


		if (Input.GetMouseButtonDown (0) && timeperShoot <= 0) 
		{
//			var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//			RaycastHit hit;
//			if (Physics.Raycast( ray, out hit, maxDistance))
//			{
//				GameObject projectile = (GameObject)Instantiate (bulletprefab, transform.position + offset, Quaternion.identity);
//				projectile.transform.LookAt (hit.point);
//				projectile.GetComponent<Rigidbody> ().AddForce(projectile.transform.forward * speed) ;
//			}

//			Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, maxDistance);
//			position = Camera.main.ScreenToWorldPoint(position);
//			var go = Instantiate(bulletprefab, transform.position + offset , Quaternion.identity) as GameObject;
//			go.transform.LookAt(position);    
//		
//			go.GetComponent<Rigidbody>().AddForce(go.transform.forward * speed);

			timeperShoot = 1.5f;

			Vector3 shoot = (viewFinder.transform.position -  transform.position).normalized;
			GameObject projectile = (GameObject)Instantiate (bulletprefab, transform.position + offset, Quaternion.identity);
			projectile.GetComponent<Rigidbody>().AddForce(shoot * speed);
			
		}
	}
}
