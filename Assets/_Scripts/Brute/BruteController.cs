using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteController : MonoBehaviour {

	private Animator anim;
	private int state;
	private bool isCrouching;
	private enum AttackState{BASICATTACK = 20, BLOCK = 21};
	private enum CharacterState { IDLE = 0, WALK = 1, RUNBACK = 2, WALKLEFT = 3, WALKRIGHT = 4, RUN = 5 , JUMP = 6 , JUMPRUNNING = 7 , CROUCH = 8 , CROUCHWALKINGFORWARD = 9 , CROUCHWALKINGBACKWARDS = 10, CROUCHWALKINGLEFT = 11 , CROUCHWALKINGRIGHT = 12}; 
	private float speed = 10.0f;
	private float rotationSpeed = 100.0f;
	int health = 10;
	int damage = 5;

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
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		CharacterMovment ();

	}

	public void CharacterMovment(){

		if (Input.GetKeyDown (KeyCode.W)) {
			state = (int)CharacterState.RUN;
		}else if (Input.GetKeyUp(KeyCode.W)) {

			state = (int)CharacterState.IDLE;
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			state = (int)CharacterState.RUNBACK;
		}else if (Input.GetKeyUp(KeyCode.S)) {

			state = (int)CharacterState.IDLE;
		}

		if (Input.GetKeyDown(KeyCode.C)){
			isCrouching = true;
			state = (int)CharacterState.CROUCH;
		}else if (Input.GetKeyUp(KeyCode.C)){
			isCrouching = false;
			state = (int)CharacterState.IDLE;
		}

		if (isCrouching == true && Input.GetKeyDown (KeyCode.W)) {
			state = (int)CharacterState.CROUCHWALKINGFORWARD;
		} else if (Input.GetKeyUp (KeyCode.W) && isCrouching == true) {
			state = (int)CharacterState.CROUCH;
		}

		if (isCrouching == true && Input.GetKeyDown (KeyCode.S)) {
			state = (int)CharacterState.CROUCHWALKINGBACKWARDS;
		} else if (Input.GetKeyUp (KeyCode.S) && isCrouching == true) {
			state = (int)CharacterState.CROUCH;
		}

		if (Input.GetMouseButtonUp(0)) {
			anim.SetTrigger ("attack");
			//state = (int)AttackState.BASICATTACK;
		} else if (Input.GetMouseButtonDown (0)) {
			state = (int)CharacterState.IDLE;
		}

		if (Input.GetMouseButtonDown (1)) {
			state = (int)AttackState.BLOCK;
		} else if (Input.GetMouseButtonUp (1)) {
			state = (int)CharacterState.IDLE;
		}
			


		if (Input.GetKeyDown (KeyCode.Space)) {
			anim.SetTrigger ("isJumping");
		}

		if (Input.GetKeyDown (KeyCode.Space) && state == (int)CharacterState.RUN) {
			state = (int)CharacterState.JUMPRUNNING;
		} else if(Input.GetKeyUp (KeyCode.Space) && state == (int)CharacterState.JUMPRUNNING) {
			state = (int)CharacterState.RUN;
		}







		float translation = Input.GetAxis ("Vertical") * speed;
		float rotation = Input.GetAxis ("Horizontal") * rotationSpeed;	
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;
		transform.Translate (0, 0, translation);
		transform.Rotate (0, rotation, 0);
		if (translation != 0) {
			//anim.SetBool ("isRunning", true);
			anim.SetInteger ("BruteState", state);
		} //else {
			//anim.SetBool ("isRunning", false);
			//	anim.SetInteger ("BruteState", state);
		//}
		anim.SetInteger ("BruteState", state);
	
	
	}
	/*public void Movment(){
	
		float translation = Input.GetAxis ("Vertical") * speed;
		float rotation = Input.GetAxis ("Horizontal") * rotationSpeed;	
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;
		transform.Translate (0, 0, translation);
		transform.Rotate (0, rotation, 0);
		if (translation != 0) {
			anim.SetBool ("isRunning", true);
		} else {
			anim.SetBool ("isRunning", false);
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			anim.SetTrigger ("isJumping");
		}
	
	
	}*/
}
