using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class LoginPlayfab : MonoBehaviour {
	public string titleID = "9EE2";
	public string customID;
	public string PlayerStatiticsName;

	public InputField inputID;
	public InputField inputPlayerStatitics;

	public Text notify;
	public Text LeaderboardResult;

	public void Login()
	{
		customID = inputID.text;

		LoginWithCustomIDRequest rq = new LoginWithCustomIDRequest () 
		{
			TitleId = 	titleID,
			CustomId = customID,
			CreateAccount = true
				
		};

		PlayFabClientAPI.LoginWithCustomID (rq, loginresult , loginerror);

	}


	void loginresult(LoginResult result)
	{
		notify.text = "Login Success!";
	}

	void loginerror(PlayFabError error)
	{
		notify.text = error.ErrorMessage;
	}


	public void createPlayerStatistics()
	{
		PlayerStatiticsName = inputPlayerStatitics.text;

		UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest ();
		request.Statistics = new List<StatisticUpdate> ();
		request.Statistics.Add (new StatisticUpdate{

			StatisticName = PlayerStatiticsName,
			Value = 100,
			Version = 0

		});

		PlayFabClientAPI.UpdatePlayerStatistics (request, (result) =>
			{
				notify.text = "Create or update a PlayerStatitics Success!";
			}, 
			(error) => 
			{
				notify.text = " Create or update a PlayerStatitics Fail!";
			});

	}

	public void GetLeaderBoard()
	{
		
		PlayerStatiticsName = inputPlayerStatitics.text;

		GetLeaderboardRequest rq = new GetLeaderboardRequest ()
		{
			StartPosition = 1,
			StatisticName = PlayerStatiticsName
		};

		PlayFabClientAPI.GetLeaderboard (rq, (result) =>
			{
				result.Version = 0;

				for(int i =0 ; i < result.Leaderboard.Count; i++ )
				{
					PlayerLeaderboardEntry entry = result.Leaderboard[i];

					LeaderboardResult.text =  entry.Position +" "+ entry.DisplayName +" " + entry.StatValue +"\n";
					notify.text  = "Get LeaderBoard Success!";
				}

			},

			(error) =>
			{
				notify.text = error.ErrorMessage;
			} );
	}
}
