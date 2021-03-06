using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlayServicesUnityMain : MonoBehaviour {
	void Start(){
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			.Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate();
	}
}
