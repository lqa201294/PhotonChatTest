using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ChatPhotonTest : MonoBehaviour 
{
	public InputField inputTextchat;
	public Text Chatbox;
	public Text NotifyStatus;
	string[] content;

	void Awake()
	{
		content = new string[1];

		PhotonNetwork.OnEventCall += this.OnEvent;
	}

	public void OnEnterSend()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			SendChatMessage(this.inputTextchat.text);
			this.inputTextchat.text = "";

		}

	}

	public void OnClickSend()
	{
		if (this.inputTextchat != null)
		{
			SendChatMessage(this.inputTextchat.text);
			this.inputTextchat.text = "";
		}
	}

	public void SendChatMessage(string textchat)
	{
		if (string.IsNullOrEmpty(textchat))
		{
			return;
		}

		//content.Add (textchat);
		content[0] = textchat;

		PhotonPlayer[] playerinRoom = PhotonNetwork.playerList;
		int[] idPlayerJoined = new int[playerinRoom.Length];

		for(int i= 0; i < playerinRoom.Length; i++)
		{
			idPlayerJoined [i] = playerinRoom [i].ID;
		}

		PhotonNetwork.RaiseEvent (0,content, true, new RaiseEventOptions()
			{Receivers = ReceiverGroup.All, CachingOption= EventCaching.AddToRoomCache, TargetActors = idPlayerJoined});
		
	}

	private void OnEvent(byte eventcode,object content, int senderid)
	{
		if (eventcode == 0) 
		{
			PhotonPlayer sender = PhotonPlayer.Find (senderid);
			string[] chatcontent = (string[])content;

			foreach (string textchat in chatcontent) 
			{
				Chatbox.text += sender.NickName +":"+" "+ textchat +"\n";
			}
		}

		if (eventcode == 1) 
		{
			Chatbox.text = string.Empty;
		}
	}


}
