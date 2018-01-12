using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

public class ScriptGPS : MonoBehaviour {

	public Text signInButtonTxt;
	public Text authStatus;

	public Button leaderBoard;
	public Button achievements;

	void Start () {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate();

		PlayGamesPlatform.Instance.Authenticate(SignInCallback, true);
		//SignIn ();
	}

	public void SignIn(){
		// Cara 3
		/* Social.localUser.Authenticate(success => { });*/
		// Cara 1
		if (!PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.Authenticate (SignInCallback, false);
			authStatus.text = "authenticating";
		} else {
			PlayGamesPlatform.Instance.SignOut ();

			signInButtonTxt.text = "Sign In";
			authStatus.text = "";
		}

		// Cara 2
		/*
		Social.localUser.Authenticate ((bool success) => { 
			if (success) {
				signInButtonTxt.text = "Sign Out";
				authStatus.text = "Hallo " + Social.localUser.userName;

				leaderBoard.interactable = true;
				achievements.interactable = true;
			} else {
				signInButtonTxt.text = "Sign In";
				authStatus.text = "";

				leaderBoard.interactable = false;
				achievements.interactable = false;
			} 
		});
		*/
	}

	void SignInCallback(bool success){
		if (success) {
			signInButtonTxt.text = "Sign Out";
			authStatus.text = "Hallo " + Social.localUser.userName;

			leaderBoard.interactable = true;
			achievements.interactable = true;
		} else {
			signInButtonTxt.text = "Sign In";
			authStatus.text = "";

			leaderBoard.interactable = false;
			achievements.interactable = false;
		}
	}

	#region Achievements
	public static void UnlockAchievement(string id)
	{
		Social.ReportProgress(id, 100, success => { });
	}

	public static void IncrementAchievement(string id, int stepsToIncrement)
	{
		PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
	}

	public static void ShowAchievementsUI()
	{
		Social.ShowAchievementsUI();
	}
	#endregion /Achievements

	#region Leaderboards
	public static void AddScoreToLeaderboard(string leaderboardId, long score)
	{
		Social.ReportScore(score, leaderboardId, success => { });
	}

	public static void ShowLeaderboardsUI()
	{
		Social.ShowLeaderboardUI();
	}
	#endregion /Leaderboards

}
