using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewFinderControl : MonoBehaviour {
	Vector3 newPos;

	// Update is called once per framex
	void Update () {
		Cursor.visible = false;
		newPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 12f);

		transform.position = Camera.main.ScreenToWorldPoint (newPos);
	}
}
