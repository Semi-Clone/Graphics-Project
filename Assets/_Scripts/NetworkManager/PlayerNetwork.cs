using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : 	MonoBehaviour {


	[SerializeField] private GameObject playerCamera;
	[SerializeField] MonoBehaviour[] playerControlScripts;

	private PhotonView photonView;

	public int playerHealth = 100;

	void Start(){
		//if(photonView.isMine){
		//playerCamera.SetActive (true);
		//foreach ( MonoBehaviour m in playerControlScripts){
		//	m.enabled = true;
		//	}}
	   photonView = GetComponent<PhotonView> ();
		Initialize ();
	}

	private void Initialize(){

		if (photonView.isMine) {
		} else {
		
			playerCamera.SetActive (false);

			foreach ( MonoBehaviour m in playerControlScripts){
				m.enabled = false;
			}
		}

	}


	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){

		if (stream.isWriting) {
			stream.SendNext (playerHealth);
		} else if (stream.isReading) {
			playerHealth = (int) stream.ReceiveNext ();
		}
	}





}
