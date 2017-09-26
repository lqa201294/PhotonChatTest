using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInRoom : MonoBehaviour {
	public GameObject NotifyJoin;
	public GameObject NotifyLeave;
	public GameObject player;

	public Text Channel;
	public Text RoomMaster;

	public List <GameObject> list;

	void Awake()
	{
		list = new List<GameObject>();

	}

	void Start()
	{
		NotifyLeave.GetComponent<Text> ().text = "";

		Channel.text ="Chanel:" + PhotonNetwork.room.Name;
		RoomMaster.text = "Master: " + PhotonNetwork.masterClient.NickName;
	}
		
	private void OnMasterClientSwitched(PhotonPlayer newMasterClient)
	{
		RoomMaster.text = "Master: " + PhotonNetwork.masterClient.NickName;
	}
		
	private void OnJoinedRoom()
	{
		NotifyJoin.GetComponent<Text>().text= PhotonNetwork.playerName +" "+ "has joined the Room" ;

		list.Clear ();


		PhotonPlayer[] photonplayer = PhotonNetwork.playerList;
		for(int i =0; i < photonplayer.Length; i++)
		{
			PlayerJoinedRoom (photonplayer[i]);
		}

		Channel.text ="Chanel:" + PhotonNetwork.room.Name;
		RoomMaster.text = "Master: " + PhotonNetwork.masterClient.NickName;

		StartCoroutine (HideNotify(2f, NotifyJoin));
	}

	IEnumerator HideNotify(float time, GameObject notify)
	{
		yield return new WaitForSeconds (time);
		notify.GetComponent<Text> ().text = "";
	}

	// called when a player join the room
	private void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		PlayerJoinedRoom (player);
	}

	private void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer)
	{
		PhotonLeftRoom (photonPlayer);
	}
		
	private void PlayerJoinedRoom(PhotonPlayer plinroom)
	{
		if (plinroom == null) 
		{
			return;
		}
			
		GameObject showplayer = Instantiate (player);
		showplayer.transform.SetParent (transform, false);
		showplayer.GetComponent<Text> ().text = plinroom.NickName;

		list.Add (showplayer);


	
	}

	private void PhotonLeftRoom(PhotonPlayer playerleave)
	{
		int index = list.FindIndex (x => x.GetComponent<Text>().text == playerleave.NickName);
		if(index != -1)
		{
			Destroy (list[index].gameObject);
			list.RemoveAt (index);
		}

//		if (playerleave.IsMasterClient) 
//		{
//			PhotonPlayer[] photonplayer = PhotonNetwork.playerList;
//
//			if(photonplayer.Length > 1)
//			{
//				PhotonNetwork.SetMasterClient (photonplayer [0]);
//			}
//		}

		NotifyLeave.GetComponent<Text>().text=  playerleave.NickName +" "+ "has leave Room";
		StartCoroutine (HideNotify(2f, NotifyLeave));
	}
		
	private void OnDisconnectedFromPhoton(object [] message)
	{
		NotifyLeave.GetComponent<Text> ().text = "disconnected the photon";
	}
}
