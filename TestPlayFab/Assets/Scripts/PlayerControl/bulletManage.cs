using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bulletManage : MonoBehaviour {
	Text numberbullet;
	public int maxBullet = 10;
	public int currentbullet;
	public bool canShoot;
	public bool charging;

	// Use this for initialization
	void Start () {
		canShoot = true;
		charging = false;
		currentbullet = maxBullet;
		numberbullet = GetComponent<Text> ();
		numberbullet.text = currentbullet + "/" + maxBullet;
	}

	void Update()
	{
		
		if (currentbullet == maxBullet) 
		{			
			canShoot = true;
		}
		
		if (Input.GetMouseButtonDown (1) && currentbullet < maxBullet) 
		{
			Recoverbullet ();
		}

	}

	private void Recoverbullet ()
	{
		charging = true;
		canShoot = false;
	
		StartCoroutine (Chargingbullet(1f));
			
	}

	IEnumerator Chargingbullet(float timeperCharge)
	{
		while (currentbullet < maxBullet) 
		{
			currentbullet++;
			numberbullet.text = currentbullet + "/" + maxBullet;
			yield return new WaitForSeconds (timeperCharge);
		}

		charging = false;
	}

	public void decreaseBullet()
	{
		if (currentbullet > 0) {
			currentbullet--;
			numberbullet.text = currentbullet + "/" + maxBullet;
		}
		else 
		{
			canShoot = false;
		}

	}
}
