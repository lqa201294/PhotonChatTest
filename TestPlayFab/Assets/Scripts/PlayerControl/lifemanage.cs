using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifemanage : MonoBehaviour {
	public float maxHp = 100;
	public float currentHp;

	// Use this for initialization
	void Start () {
		currentHp = maxHp;
	}
	

	public void takeDamage(float damage)
	{
		if (currentHp >= damage) 
		{
			currentHp -= damage;
			GetComponent<Image> ().fillAmount = currentHp / maxHp;
		}
	}
}
