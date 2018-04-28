﻿using System.Collections;
using UnityEngine;




public class reptilechase : MonoBehaviour {

	public Transform player ;
	static Animator anim ;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {

		Vector3 direction = player.position - this.transform.position; 
		float angle = Vector3.Angle (direction, this.transform.forward);

		if (Vector3.Distance (player.position, this.transform.position) < 10 && angle < 50) {

			direction.y = 0;
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation,
				Quaternion.LookRotation (direction), 0.1f);

			anim.SetBool ("isIdle",false);
			if (direction.magnitude >2) {
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
