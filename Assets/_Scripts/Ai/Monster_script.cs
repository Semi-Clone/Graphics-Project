using System.Collections;
using UnityEngine;
using UnityEngine.AI;




public class Monster_script : MonoBehaviour {

	//public Transform  player ;
	public Transform head;
	 Animator anim ;
	public GameObject[] players; 
	string state ="patrol";
	public GameObject[] waypoints;
	int currentWP = 0;
	public float speed=1.5f;
	public float accuracyWP = 5.0f;
	public float rotSpeed = 0.2f;
    private float dmg = 5;
    private float hp = 20;
    private bool isDead;
    GameObject objectMonster;
	 //public bool pursuing = false; 

	public float lookRadius = 10f;

	// Use this for initialization
	void Start () {
        isDead = false;
        objectMonster = this.gameObject;
        anim = GetComponent<Animator> ();
		players= GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
		foreach (GameObject p in players) {
			Vector3 direction = p.transform.position - this.transform.position; 
		
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
				direction = waypoints [currentWP].transform.position - transform.position;
				this.transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), rotSpeed * Time.deltaTime);
				this.transform.Translate (0, 0, Time.deltaTime * speed);
			}


			float distance = Vector3.Distance (p.transform.position, this.transform.position);

			if (Vector3.Distance (p.transform.position, this.transform.position) < 10 && angle < 50 || state == "pursuing") {
			
				//direction.y = 0;

				state = "pursuing";
				this.transform.rotation = Quaternion.Slerp (this.transform.rotation,
					Quaternion.LookRotation (direction), 0.1f);
			
				//anim.SetBool ("isIdle", false);
				if (direction.magnitude > 10)
					state = "patrol";
				if (direction.magnitude > 1) {
					this.transform.Translate (0, 0, 0.05f);
					anim.SetBool ("isWalking", true);
					anim.SetBool ("isAttacking", false);
				} else {
					anim.SetBool ("isWalking", false);
					anim.SetBool ("isAttacking", true);
				}
			} else {
			
				anim.SetBool ("isWalking", false);
				anim.SetBool ("isAttacking", false);
				state = "patrol";
			}
		}
}
	void OnDrawGizmosSelected (){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, lookRadius);
	}

    void OnTriggerEnter(Collider col)
    {
        Animator Another = col.GetComponentInParent<Animator>();
        if (Another != null && Another.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            if (!col.gameObject.tag.Contains("Monster") && col.gameObject.tag.Contains("Hitbox"))
            {
                Debug.Log("hit");
                hp -= dmg;
                if (hp == 0)
                {
                    Destroy(objectMonster);
                }
            }
        }
    }
    
}