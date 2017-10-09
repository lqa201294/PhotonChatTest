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

	public GameObject waiting;
	public GameObject StartButton;
	public static List<string> playername = new List<string> ();

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

	private void OnMasterClientSwitched(PhotonPlayer newMasterClient)
	{
		print ("new change masterclient" +" " + newMasterClient.NickName);
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
		int index = playername.FindIndex (x =>x == playerJoined.NickName);

		if (index == -1) 
		{
			playername.Add (playerJoined.NickName);
			index = playername.Count - 1;

			PhotonNetwork.RaiseEvent (3, new object[]{playername[index]}, true, new RaiseEventOptions () {
				Receivers = ReceiverGroup.All,
				CachingOption = EventCaching.AddToRoomCache
			});
			print ("number playerjoined :" + index);
			print ("number playerjoined :" +" "+  playername.Count);
			player [index].SetActive (true);
		}
	}
		

	private void PlayerLeftRoom(PhotonPlayer playerleave)
	{
		int index = playername.FindIndex (x => x == playerleave.NickName);
		if(index != -1)
		{
			player [1].SetActive (false);
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
	
			if (playername.Count == 1) 
			{

				int avaIndex  = PlayerPrefs.GetInt ("random",2);

				if (avaIndex == 2) 
				{
					int randomAva = UnityEngine.Random.Range (0, avatar.Length - 1);

					player [0].GetComponent<Image> ().sprite = avatar [randomAva];
					playername1.text = (string)data[0];

					PlayerPrefs.SetInt ("random", randomAva);
					PlayerPrefs.Save ();
					print ("random a avatar");
				}
				else 
				{
					
					player [0].GetComponent<Image> ().sprite = avatar [avaIndex];
					playername1.text = (string)data[0];
				
					PlayerPrefs.SetInt ("random", avaIndex);
					PlayerPrefs.Save ();

					print ("evecode3" +" "+ (string)data[0]);

				}
					
				print (avaIndex);
			}
			else
			{
				PhotonNetwork.RaiseEvent (4, new object[]{ PlayerPrefs.GetInt("random"), playername[1]}, true, new RaiseEventOptions () {
					Receivers = ReceiverGroup.All,
					CachingOption = EventCaching.AddToRoomCache
				});
			}

		}

		if(eventcode ==4)
		{
			object[] data = (object[])content;
			int ava =0;

			if ((int)data [0] == avatar.Length) 
			{
				ava = avatar.Length - 1;
			}
			else
			{
				ava = (int)data [0] + 1;
			}

			player [0].GetComponent<Image> ().sprite = avatar [(int)data[0]];
			playername1.text = PhotonNetwork.masterClient.NickName;

			player [1].GetComponent<Image> ().sprite = avatar [ava];
			playername2.text = (string)data[1];

		}
			
	}
		
	IEnumerator HideNotify(float time, Text notify)
	{
		yield return new WaitForSeconds (time);
		notify.text = string.Empty;
	}

}
