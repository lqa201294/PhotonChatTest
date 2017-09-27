using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingTime : MonoBehaviour {
	Color startcolor;
	Color endcolor;
	Color currentColor;

	public float speed;

	// Use this for initialization
	void Start () {
		currentColor = GetComponent<Text> ().color;

		startcolor = new Color (0,0,0,1);
		endcolor = new Color (0,0,0,0);

	}
	
	// Update is called once per frame
	void Update () {
		currentColor= Color.Lerp(startcolor, endcolor, Mathf.PingPong(Time.time * speed, 1f));
		GetComponent<Text> ().color = currentColor;
	}
}
