using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : Photon.MonoBehaviour {

	[SerializeField] private GameObject player;
	[SerializeField] private GameObject LobbyCamera;
	[SerializeField] private Transform spawnPoint;



	void Start () {
		
		PhotonNetwork.ConnectUsingSettings ("0.1");
	}

	public virtual void OnJoinedLobby(){
	
		Debug.Log("Joined Lobby");
		// use RoomOptions if Want to Configure Room.
		//RoomOptions roomOptions = new RoomOptions ();
		PhotonNetwork.JoinOrCreateRoom ("New", null,null);
	
	}

	public virtual void OnJoinedRoom(){
	
		PhotonNetwork.Instantiate (player.name, spawnPoint.position, spawnPoint.rotation, 0);
		LobbyCamera.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		//testing
		//Debug.Log(PhotonNetwork.connectionStateDetailed);

	}
}
