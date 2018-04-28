using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectHIt : MonoBehaviour {
	int health = 10;
	int damage = 5;
	Animator anim;

	void OnTriggerEnter(Collider other){
		Debug.Log ("HIT");
		if (other.gameObject.tag == "HitBox") {
			health -= damage;
			anim.SetBool ("takingdmg", true);

			if (health == 0) {
				anim.SetBool ("isDead", true);	
				Debug.Log ("Dead");
			}
		} else {
			anim.SetBool ("takingdmg", false);
		}
	
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
