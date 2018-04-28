using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour {


	void Start ()
	{	
		print ("Connecting to Server...");
		PhotonNetwork.ConnectUsingSettings ("0.1");
	}


	private void OnConnectedToMaster()
	{
		print ("Connected to master");
		PhotonNetwork.automaticallySyncScene = false;
		//PhotonNetwork.playerName = Players.instance.Name;
		PhotonNetwork.JoinLobby (TypedLobby.Default);
	}

	private void OnJoinedLobby()
	{
		print ("Joined Lobby");
	       /* if (!PhotonNetwork.inRoom)
            MainCanvasManager.Instance.LobbyCanvas.transform.SetAsLastSibling();*/
	}
}

