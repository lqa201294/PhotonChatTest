using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveRoom : MonoBehaviour {
	public GameObject Lobby;
	public GameObject ChatRoom;

	public void LeftRoom()
	{
		if (PhotonNetwork.LeaveRoom ()) 
		{
			print ("Left Room Send Request");
		}
		else
		{
			print ("Left room Fail!");
		}
		ChatRoom.SetActive (false);
		Lobby.SetActive (true);
	}
}
