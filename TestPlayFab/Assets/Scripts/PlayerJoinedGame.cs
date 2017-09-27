using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerJoinedGame : MonoBehaviour {
	public Text Notify;
	public Text playername1;
	public Text playername2;
	public Text RoomName;

	public Sprite[] avatar;
	public GameObject[] player;

	public Button ready;
	private bool clientready;
	bool randomAvatar;
	int index0;
	int index1;

	public GameObject waiting;
	public GameObject StartButton;
	List<string> playername = new List<string> ();

	// Use this for initialization
	void Start () 
	{
		ready.onClick.AddListener (Playerready);
		StartButton.GetComponent<Button> ().onClick.AddListener (StartGame);

		PhotonNetwork.OnEventCall += this.GetEvent;

	}


	void Update()
	{
		if (clientready && PhotonNetwork.isMasterClient) 
		{
			waiting.SetActive (false);
			StartButton.SetActive (true);
		
		}
	}

	private void StartGame()
	{
		gameObject.SetActive (false);
		StartButton.SetActive (false);

	}

	private void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		PlayerJoinedRoom (player);
	}

	private void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer)
	{
		PlayerLeftRoom (photonPlayer);
	}

	private void OnJoinedRoom()
	{
		Notify.GetComponent<Text>().text= PhotonNetwork.playerName +" "+ "has joined the Room" ;

		PhotonPlayer[] photonplayer = PhotonNetwork.playerList;
		foreach(PhotonPlayer player in photonplayer)
		{
			PlayerJoinedRoom (player);
		}
			
		RoomName.text = "Room:" + " " + PhotonNetwork.room.Name;

		StartCoroutine (HideNotify(2f, Notify));
	}

	private void PlayerJoinedRoom(PhotonPlayer playerJoined)
	{
		waiting.SetActive (true);
		randomAvatar = true;
		int index = playername.FindIndex (x =>x == playerJoined.NickName);
		if (index == -1) 
		{
			if (randomAvatar) 
			{
				randomAvatar = false;
				index0 = UnityEngine.Random.Range (0, avatar.Length-1);

			}
				
			if (playerJoined.NickName == PhotonNetwork.masterClient.NickName)
			{
				PhotonNetwork.RaiseEvent (3, new object []{ index0 }, true, new RaiseEventOptions () {
					Receivers = ReceiverGroup.All,
					CachingOption = EventCaching.AddToRoomCache
				});
				print ("init image player1" +" "+ index0);

			}
			else
			{
				print ("init image player2" +" "+  index1);
				PhotonNetwork.RaiseEvent (4, new object []{index1}, true, new RaiseEventOptions () {
					Receivers = ReceiverGroup.All,
					CachingOption = EventCaching.AddToRoomCache
				});
			

			}
			playername.Add (playerJoined.NickName);
			player [playername.Count - 1].SetActive (true);
		}
	}
		
	private void PlayerLeftRoom(PhotonPlayer playerleave)
	{
		int index = playername.FindIndex (x => x == playerleave.NickName);
		if(index != -1)
		{
			player [index].SetActive (false);
			playername.RemoveAt (index);
		}

		Notify.GetComponent<Text> ().text = playerleave.NickName + " " + "has left the Room";
		StartCoroutine (HideNotify(2f, Notify));
	}

	public void Playerready()
	{
		if (PhotonNetwork.isMasterClient == false) 
		{
			ready.interactable = false;
			clientready = true;

			PhotonNetwork.RaiseEvent (2, new object []{ready.interactable, clientready}, true, new RaiseEventOptions()
				{
					Receivers = ReceiverGroup.All,
					CachingOption= EventCaching.AddToRoomCache
				});

		}

	}

	private void GetEvent(byte eventcode, object content, int senderid)
	{
		if (eventcode == 2) 
		{
			object[] data = (object[])content;

			ready.interactable = Convert.ToBoolean (data[0]);
			clientready = Convert.ToBoolean (data [1]);

			PhotonPlayer sender = PhotonPlayer.Find (senderid);

			Notify.text = sender.NickName +" "+ "is Ready!";

			StartCoroutine (HideNotify(2f, Notify));
			print("playerjoinedGame click ready");

		}

		if (eventcode == 3) 
		{
			object[] data = (object[])content;

			index1 = (int)data [0] + 1;
			player[0].GetComponent<Image>().sprite = avatar [(int)data[0]];

			PhotonPlayer sender = PhotonPlayer.Find (senderid);
			playername1.text = sender.NickName;


		}
			
		if (eventcode == 4) 
		{
			
			object[] data = (object[])content;
		
			player[1].GetComponent<Image>().sprite = avatar [(int)data[0]];

			PhotonPlayer sender = PhotonPlayer.Find (senderid);
			playername2.text = sender.NickName;
		}
	}
		
	IEnumerator HideNotify(float time, Text notify)
	{
		yield return new WaitForSeconds (time);
		notify.text = string.Empty;
	}

}
