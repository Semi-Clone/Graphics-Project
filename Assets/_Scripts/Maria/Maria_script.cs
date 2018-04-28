using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maria_script : MonoBehaviour {

    private Animator anim;
    private Rigidbody rbody;
    //public float speed = 7.0f;
    // public float rotation_speed = 70.0f;
    static int slashes;
    private float inputH;
    private float inputV;
    private bool jumbAttack;
	private string prevAttack ="prevAttack";
	private bool stand = true ;
    public int health = 10;
    public int damage = 5;

    public bool lockCursor = true;

    void OnTriggerEnter(Collider other){
		Debug.Log ("HIT");
		if (other.gameObject.tag == "HitBox") {
			health -= damage;
			anim.SetBool ("hit", true);

			if (health == 0) {
				anim.SetBool ("isDead", true);	
				Debug.Log ("Dead");
			}
		} else {
			anim.SetBool ("hit", false);
		}}


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
        anim.SetFloat("inputV", inputV);

	if (inputH > 0.1f) {
			transform.Rotate(new Vector3(0,100)* Time.deltaTime);
		} else if (inputH < -0.1f ) {
			transform.Rotate(new Vector3(0,-100)* Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.Space))
        {
           
            anim.SetTrigger("Jump");	
        }

		if (Input.GetKey("f")) {
			anim.ResetTrigger(prevAttack);
			prevAttack = "attack" + Random.Range (1, 6);
			anim.SetTrigger (prevAttack);
		}

        if (Input.GetKey("j"))
        {
            anim.SetTrigger("kick");

        }
       
		if (Input.GetKey("c")){
			stand = !stand;
			anim.SetBool("stand",stand);
		
		}

        // pressing esc toggles between hide/show
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            lockCursor = !lockCursor;
        }

        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;
    }


}
