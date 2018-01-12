using UnityEngine;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour {

	bool isDead;
	Animator anim;
	float timer; 
	float scoring, hgScore;
	int milestone;
	float speed;

	public GameObject gameOverCanvas;
	public Text txScore;
	public Text txHgScore;
	public AudioSource Audio;

	//Swipe

	public Swipe swipeControl;
	public Transform player;
	float inMove= 1.7f;
	private Vector3 desiredPosition;

	void Start () {
		isDead = false;
		anim = gameObject.GetComponent<Animator> ();
		speed = 4;
		milestone = 10;
		Audio = GetComponent<AudioSource> ();
		GameData.HighScore = PlayerPrefs.GetFloat ("hgScore", 0);
		hgScore = GameData.HighScore;
	}

	void FixedUpdate(){

	}

	void Update () {

		#region Dead
		// Kondisi Mati

		if (isDead == true) {
			timer = timer + 1*Time.deltaTime;
			if (timer > 0.5f) {
				Audio.Stop();
				Time.timeScale = 0;
				txScore.text = "Score\n" + scoring.ToString("000");
				txHgScore.text = "Highscore\n" + GameData.HighScore.ToString("000");

				// HighScore 

				if (scoring > hgScore) {
					PlayerPrefs.SetFloat ("hgScore", scoring);
					GameData.HighScore = scoring;
					ScriptGPS.AddScoreToLeaderboard (GPGSIds.leaderboard_leaderboard, (int)scoring );

					if (hgScore >= 100)
						ScriptGPS.UnlockAchievement (GPGSIds.achievement_laleur_warior);
					if (hgScore >= 300)
						ScriptGPS.UnlockAchievement (GPGSIds.achievement_laleur_captain);
					if (hgScore >= 750)
						ScriptGPS.UnlockAchievement (GPGSIds.achievement_dodge_master_of_laleur);
					if (hgScore >= 1000)
						ScriptGPS.UnlockAchievement (GPGSIds.achievement_the_god_of_laleur_dodge);
				}

				gameOverCanvas.gameObject.SetActive(true);

			}
		}else{
			txScore.text = scoring.ToString ("000");
		}
		#endregion /Dead

		#region Control and Scoring
		// Swipe Controller

		if (swipeControl.SwipeLeft)
			desiredPosition += Vector3.left * inMove;
		//desiredPosition = desiredPosition + new Vector3(-1, -3.5f, 0)*inMove;
		if (swipeControl.SwipeRight)
			desiredPosition += Vector3.right * inMove;
		//desiredPosition = desiredPosition + new Vector3(1, -3.5f, 0)*inMove;

		player.transform.position = Vector3.MoveTowards (player.transform.position, desiredPosition, 7f * Time.deltaTime);

		if (swipeControl.Tap) {
			Debug.Log ("Geser");
		}

		// Scoring

		scoring += Time.deltaTime * 2;

		if (scoring > milestone) {
			speed += 0.05f;
			milestone += 10;
			if (milestone > 50) {
				speed += 0.5f;
				milestone += 20;
			} else if (milestone > 250) {
				speed += 1f;
				milestone += 50;
			}
		}
		MoveObject.speedCounter = speed;
		//Debug.Log (MoveObject.speedCounter +" : "+ milestone);

		#endregion /Control and Scoring
	}

	void OnCollisionEnter2D(Collision2D col){

		// Nabrak Track

		if (col.gameObject.tag == "Track") {
			isDead = true;

			anim.SetBool ("isDead", true);
		}
	}
		
}