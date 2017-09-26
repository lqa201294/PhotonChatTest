using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetWorkPhoton : MonoBehaviour {
	public static string PlayfabId;
	public static string PlayerName;
	public static string token;

	// Use this for initialization
	private void Awake () 
	{
		DontDestroyOnLoad (this);
	}
	

}
