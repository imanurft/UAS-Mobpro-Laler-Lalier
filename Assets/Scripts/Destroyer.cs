using UnityEngine;

public class Destroyer : MonoBehaviour {

	public Transform spawnPoint;
	public GameObject[] Tracks;
	bool createTrack;

	void Start(){
		createTrack = false;
	}

	void FixedUpdate(){
		if (createTrack) {
			int random = Random.Range (0, Tracks.Length-1);
			Instantiate (Tracks [random], spawnPoint.position, spawnPoint.rotation);
			createTrack = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Track") {			// Nabrak Track
			createTrack = true;
			Destroy (col.gameObject);
		}
	}

}
