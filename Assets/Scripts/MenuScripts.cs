using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour {

	public void Start(){
		Time.timeScale = 1;
	}

	public void Play(){
		SceneManager.LoadScene ("GamePlay");
	}

	public void Exit(){
		Application.Quit ();
	}

	public void ShowLeaderboards(){
		ScriptGPS.ShowLeaderboardsUI ();
	}

	public void ShowAchievemnts(){
		ScriptGPS.ShowAchievementsUI ();
		Debug.Log ("Lihat Achievements");
	}

	public void ToMenu(){
		SceneManager.LoadScene ("MainMenu");
	}
}
