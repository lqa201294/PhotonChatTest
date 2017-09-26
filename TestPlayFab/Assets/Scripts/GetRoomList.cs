using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetRoomList : MonoBehaviour {
	public GameObject RoomEntry;
	public GameObject Notify;
	public GameObject ChatRoom;
	public GameObject CreateRoom;

	List<GameObject> ListRoomCreated;

	void Start()
	{
		ListRoomCreated = new List<GameObject>();
	}

	private void OnReceivedRoomListUpdate()
	{
		for (int i = 0; i < ListRoomCreated.Count; i++) 
		{
			Destroy (ListRoomCreated[i]);
			ListRoomCreated.Clear ();
		}

		RoomInfo[] rooms = PhotonNetwork.GetRoomList ();
		foreach (RoomInfo room in rooms) 
		{
			OnReceivedRoom (room);
		}


	}

	private void OnReceivedRoom(RoomInfo room)
	{
		int index = ListRoomCreated.FindIndex (x => x.transform.GetChild (0).GetComponent<Text> ().text == room.Name);

		if (index == -1) 
		{
			if (room.PlayerCount < room.MaxPlayers && room.IsVisible) 
			{
				GameObject go = Instantiate (RoomEntry);
				go.transform.SetParent (transform,false);
				go.transform.GetChild (0).GetComponent<Text> ().text = room.Name;
				go.transform.GetChild (1).GetComponent<Text> ().text = (room.PlayerCount + "/" + room.MaxPlayers).ToString ();
				go.transform.GetChild (2).GetComponent<Button> ().onClick.AddListener (delegate{JoinToRoom(room.Name);} );

				ListRoomCreated.Add (go);
				index = ListRoomCreated.Count - 1;
						
			}
				
		}
					
	}


	private void JoinToRoom(string roomName)
	{
		PhotonNetwork.JoinRoom (roomName);
		ChatRoom.SetActive (true);
		CreateRoom.SetActive (false);
	}

	private void OnPhotonJoinRoomFailed (object[] message)
	{
		Notify.SetActive (true);
		Notify.transform.GetChild (0).GetComponent<Text> ().text = message [1].ToString ();
		Notify.transform.GetChild (1).GetComponent<Button> ().onClick.AddListener (ClickOk);
	}

	void ClickOk()
	{
		Notify.SetActive (false);
	}
}
