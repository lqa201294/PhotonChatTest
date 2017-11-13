using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamePlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PhotonNetwork.isMasterClient) 
		{
			gameObject.GetComponent<Text> ().text = PhotonNetwork.masterClient.NickName;
		}

		else 
		{
			gameObject.GetComponent<Text> ().text = PhotonNetwork.player.NickName;
		}
	}
	

}
