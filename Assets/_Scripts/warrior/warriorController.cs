using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warriorController : MonoBehaviour {
	private Animator animator;
	private bool isStanding, isBlocking;
	private string prevAttack;
	private float speed, hp;
	public int dmg = 5;


	void OnTriggerEnter(Collider col){
	
		if (col.gameObject.tag == "HitBox") {
		
			hp -= dmg;
			animator.SetBool ("faceHit", true);

			if (hp == 0) {
			
				animator.SetBool ("isDead", true);
			}
		} else {
			animator.SetBool ("isDead", false);
		}
	
	
	
	
	}

	void Start () {
		animator = GetComponent<Animator> ();
		isStanding = true;
		isBlocking = false; 
		speed = animator.GetFloat ("speed");
		hp = animator.GetFloat ("hp");
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.C)) {
			isStanding = !isStanding;
			animator.SetBool ("isStanding", isStanding);
		}

		if (Input.GetKeyDown (KeyCode.F) && isStanding) {
			animator.SetBool (prevAttack, false);
			prevAttack = "attack" + Random.Range (1, 11);
			animator.SetBool (prevAttack, true);
		} 

		if(Input.GetKeyUp(KeyCode.F))
			animator.SetBool (prevAttack, false);
		
		if (Input.GetKey (KeyCode.W)) {
			if (speed < 1f)
				speed += 0.1f;
			animator.SetFloat ("speed", speed);
			rotate ();
		} else if (Input.GetKey (KeyCode.S)) {
			if (speed > -1f)
				speed -= 0.1f;
			animator.SetFloat ("speed", speed);
			rotate ();
		} else {
			if (speed > 0.02f) {
				speed -= 0.02f;
				animator.SetFloat ("speed", speed);
			} else if (speed <= -0.02f) {
				speed += 0.02f;
				animator.SetFloat ("speed", speed);
			}
		}

		if (Input.GetKeyDown (KeyCode.Space) && isStanding) {
			animator.SetBool ("jump", true);
		} else {
			animator.SetBool ("jump", false);
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			isBlocking = !isBlocking;
			animator.SetBool ("block", isBlocking);
		}
	}

	private void rotate(){
		if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (new Vector3(0,100) * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.A)) {

			transform.Rotate (new Vector3(0,-100) * Time.deltaTime);
		}
	}
			
}
