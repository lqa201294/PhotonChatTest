using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyNetwork : MonoBehaviour {
	public GameObject Notify;

	// Use this for initialization
	private void Start () {
		Notify.GetComponent<Text>().text = "Connecting to server...";
		PhotonNetwork.ConnectUsingSettings ("1.0");

	}
	private void OnConnectedToMaster()
	{
		Notify.GetComponent<Text>().text ="Connected to master server ...";
		PhotonNetwork.JoinLobby (TypedLobby.Default);
	
	}

	private void OnJoinedLobby()
	{
		Notify.GetComponent<Text>().text = PhotonNetwork.playerName+" "+ "joined lobby.";
		Invoke ("HideNotify", 2f);
	}

	void HideNotify()
	{
		Notify.GetComponent<Text> ().text = "";
	}
}
