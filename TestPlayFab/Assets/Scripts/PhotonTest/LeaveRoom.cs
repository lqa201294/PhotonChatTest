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

			if (PhotonNetwork.isMasterClient) 
			{
				PlayerPrefs.DeleteAll ();

				int index = PlayerJoinedGame.playername.FindIndex (x =>x == PhotonNetwork.masterClient.NickName);
				if (index == -1) {
					return;
				}
				else
				{
					PlayerJoinedGame.playername.RemoveAt (index);
					print ("remove master in list");
				}
					
			}
		}
		else
		{
			print ("Left room Fail!");
		}

		ChatRoom.SetActive (false);
		Lobby.SetActive (true);
	}
}
