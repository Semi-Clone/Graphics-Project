using System.Collections;
using UnityEngine;




public class reptilechase : MonoBehaviour {

	//public Transform player ;
	public GameObject [] players; 
	static Animator anim ;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		players= GameObject.FindGameObjectsWithTag("Player");
	}

	// Update is called once per frame
	void Update () {
		foreach (GameObject p in players) {
			Vector3 direction = p.transform.position - this.transform.position; 
			float angle = Vector3.Angle (direction, this.transform.forward);

			if (Vector3.Distance (p.transform.position, this.transform.position) < 10 && angle < 50) {

				direction.y = 0;
				this.transform.rotation = Quaternion.Slerp (this.transform.rotation,
					Quaternion.LookRotation (direction), 0.1f);

				anim.SetBool ("isIdle", false);
				if (direction.magnitude > 2) {
					this.transform.Translate (0, 0, 0.05f);
					anim.SetBool ("isWalking", true);
					anim.SetBool ("isAttacking", false);
				} else {
					anim.SetBool ("isWalking", false);
					anim.SetBool ("isAttacking", true);
				}


			} else {
				anim.SetBool ("isIdle", true);
				anim.SetBool ("isWalking", false);
				anim.SetBool ("isAttacking", false);
			}
		}
	}
}
