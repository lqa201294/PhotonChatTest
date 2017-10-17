using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateRoom : MonoBehaviour {
	public InputField inputName;
	private string RoomName;
	public GameObject Notify;
	public byte maxPlayerinRoom;
	public GameObject ChatPanel;
	public GameObject MakeRoomPanel;

	public GameObject GameRoom;

	public void PublishRoom()
	{
		Notify.SetActive (true);

		RoomName = inputName.text;

		RoomOptions option = new RoomOptions ();
		option.MaxPlayers = maxPlayerinRoom;


		if (PhotonNetwork.CreateRoom (RoomName, option, TypedLobby.Default)) 
		{
			Notify.GetComponent<Text>().text = "Sent request create room Success!";
		}
		else 
		{
			Notify.GetComponent<Text>().text = "Fail! Room'name is existing already. Try with other room name";

		}
	}

	private void OnPhotonCreateRoomFailed(object[] message)
	{
		Notify.GetComponent<Text>().text ="Create room Fail!" + message[1].ToString();
	}

	private void OnCreatedRoom()
	{
		print (PhotonNetwork.playerName + " created a Room! ^^");
		Notify.GetComponent<Text> ().text = PhotonNetwork.playerName + " created a Room! ^^";
			
		StartCoroutine (hideNotify(2f , Notify));
		GameRoom.SetActive (true);
		MakeRoomPanel.SetActive (false);
	}


	IEnumerator hideNotify(float time , GameObject notify)
	{
		yield return new WaitForSeconds (time);
		notify.GetComponent<Text> ().text = "";
	}

}
