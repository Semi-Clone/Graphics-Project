
using UnityEngine;

public class Players : MonoBehaviour {

	public static Players instance;
	public string Name { get ; private set;}
	private PhotonView PhotonView;
	

	private void Awake()
	{
		instance = this;

		Name = "Player#" + Random.Range (1000, 9999);
		//  SceneManager.sceneLoaded += OnSceneFinishedLoading;
	}

	// private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    // {
    //     if (scene.name == "Game")
    //     {
    //         if (PhotonNetwork.isMasterClient)
    //             MasterLoadedGame();
    //         else
    //             NonMasterLoadedGame();
    //     }
    // }

	//   private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    // {
    //     if (scene.name == "Game")
    //     {
    //         if (PhotonNetwork.isMasterClient)
    //             MasterLoadedGame();
    //         else
    //             NonMasterLoadedGame();
    //     }
    // }
	
}
