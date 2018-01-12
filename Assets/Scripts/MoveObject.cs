using UnityEngine;

public class MoveObject : MonoBehaviour {

	public static MoveObject Instance { get; private set; }
	public static float speedCounter { get; set; }
	float speed;

	void Start(){
		Instance = this;
	}

	void Update () {
		speed = speedCounter;
		float move = (speed * Time.deltaTime * -1f) + transform.position.y;
		gameObject.transform.position = new Vector3 (transform.position.x, move);
	}
}
