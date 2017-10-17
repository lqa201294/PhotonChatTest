using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;


public class PlayFabAuthenticator : MonoBehaviour {
	public string PlayfabId;
	public string customid;

	public InputField inputCustomID;
	public Text Notify;

	public void Authenticator()
	{
		Notify.text = "Playfab authenticating using customID...";
		customid = inputCustomID.text;

		PlayFabClientAPI.LoginWithCustomID (new LoginWithCustomIDRequest () 
		{
				CreateAccount = true,
				CustomId = customid
				
			}, RequestPhotonToken, OnPlayFabError);
	}

	void RequestPhotonToken(LoginResult result)
	{
		Notify.text = "Playfab authenticated. Requesting photon token...";

		PlayfabId = result.PlayFabId;

		PlayFabClientAPI.GetPhotonAuthenticationToken (new GetPhotonAuthenticationTokenRequest()
			{
				PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppID
			}, AuthenticatewithPhoton, OnPlayFabError);
					
		UpdateUserTitleDisplayNameRequest request = new UpdateUserTitleDisplayNameRequest ()
		{
			DisplayName = customid
				
		};

		PlayFabClientAPI.UpdateUserTitleDisplayName(request, (Inforesult) =>
			{
				print("update displayname success your displayname is" +" " +Inforesult.DisplayName);
				
			}, (error)=>
			{
				print("getaccinfo" + error);

			});
	}

	void AuthenticatewithPhoton(GetPhotonAuthenticationTokenResult result)
	{
		Notify.text = "Photon token acquired Authentication complete.";

		var customAuth = new AuthenticationValues 
		{ 
			AuthType = CustomAuthenticationType.Custom
		};
		customAuth.AddAuthParameter("username", PlayfabId);   
		customAuth.AddAuthParameter("token", result.PhotonCustomAuthenticationToken);
	
		PhotonNetwork.AuthValues = customAuth;

		SceneManager.LoadScene (1);
	}
		
	void OnPlayFabError(PlayFabError error)
	{
		print( error.ErrorMessage);
	}
}
