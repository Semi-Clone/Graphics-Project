using System.Collections;
using UnityEngine;
using UnityEngine.AI;




public class Monster_script : MonoBehaviour {

	public Transform  player ;
	public Transform head;
	 Animator anim ;

	string state ="patrol";
	public GameObject[] waypoints;
	int currentWP = 0;
	public float speed=1.5f;
	public float accuracyWP = 5.0f;
	public float rotSpeed = 0.2f;
	 //public bool pursuing = false; 

	public float lookRadius = 10f;

	// Use this for initialization
	void Start () {
		
		player = GameManager.instance.Player.transform;
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 direction = player.position - this.transform.position; 
		direction.y = 0;
		float angle = Vector3.Angle (direction, head.up);

		if (state == "patrol" && waypoints.Length > 0) {


			anim.SetBool ("isIdle", false);
			anim.SetBool ("isWalking", true);
			if (Vector3.Distance (waypoints [currentWP].transform.position, transform.position) < accuracyWP) {

				currentWP = Random.Range (0, waypoints.Length);
				//currentWP++;
				//if (currentWP >= waypoints.Length) {
				//	currentWP = 0;
				//}
			}

			//rotate twards waypoints
			direction = waypoints[currentWP].transform.position-transform.position;
			this.transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), rotSpeed * Time.deltaTime);
			this.transform.Translate (0, 0, Time.deltaTime * speed);
		}


		float  distance = Vector3.Distance (player.position, this.transform.position);

		if (Vector3.Distance (player.position, this.transform.position)< 10 && angle < 50 || state=="pursuing") 
		{
			
			//direction.y = 0;

			state="pursuing";
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation,
				Quaternion.LookRotation (direction), 0.1f);
			
			//anim.SetBool ("isIdle", false);
			if (direction.magnitude > 10)
				state = "patrol";
			if (direction.magnitude >2) {
				this.transform.Translate (0, 0, 0.05f);
				anim.SetBool ("isWalking", true);
				anim.SetBool ("isAttacking", false);
			} else {
				anim.SetBool ("isWalking", false);
				anim.SetBool ("isAttacking", true);
			}
		}
		else {
			
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isAttacking", false);
			state = "patrol";
		}

}
	void OnDrawGizmosSelected (){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, lookRadius);
	}
}