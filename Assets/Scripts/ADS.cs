using UnityEngine;
using System;
using System.Collections;
using GoogleMobileAds.Common;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class ADS : MonoBehaviour {
	public PlayerMovement m_Player;
	public Text m_ReviveCountText;
	public float m_CountDown = 3f;
	public string[] m_ZoneIDs;
	private bool m_VideoFinished = false;
    [HideInInspector] public InterstitialAd intersitial;


	public void OnInitialize(){
		AdColonySetup ();
	}

    public void Start() {
		m_ReviveCountText.gameObject.SetActive (false);
		AdColony.OnVideoFinished = this.OnVideoFinished;
        RequestBanner();
		AdColonySetup ();
    }

	public void AdColonySetup(){
		AdColony.Configure ("0.00005", "123", m_ZoneIDs[m_ZoneIDs.Length-1]);
	}

	public void Update(){
		if (m_VideoFinished){
			float _FinalCountDown = m_CountDown -= Time.deltaTime;
			Mathf.Floor(_FinalCountDown);
			m_ReviveCountText.gameObject.SetActive (true);
			m_ReviveCountText.text = m_CountDown.ToString ();
			if (m_CountDown <= 0) {
				m_Player.Revive ();
				m_VideoFinished = false;
				m_ReviveCountText.gameObject.SetActive (false);
				m_CountDown = 3f;
			}
			m_Player.m_AudioMain.Play ();
		}
	}

	// When a video is available, you may choose to play it in any fashion you like.
	// Generally you will play them automatically during breaks in your game,
	// or in response to a user action like clicking a button.
	// Below is a method that could be called, or attached to a GUI action.
	public void PlayAVideo( string zoneID )
	{
		// Check to see if a video is available in the zone.
		if(AdColony.IsVideoAvailable(zoneID))
		{
			Debug.Log("Play AdColony Video");
			// Call AdColony.ShowVideoAd with that zone to play an interstitial video.
			// Note that you should also pause your game here (audio, etc.) AdColony will not
			// pause your app for you.
			m_Player.m_AudioMain.Stop ();
			m_Player.m_Speed = 0f;
			m_Player.m_MyPlayer.SetActive (true);
			m_Player.m_MyPlayer.transform.position = m_Player.m_BackSpawn.position;
			//Time.timeScale = 0.001f;
			AdColony.ShowVideoAd(zoneID); 
		}
		else
		{
			AdColonySetup ();
		}
	}

	private void OnVideoFinished(bool ad_was_shown)
	{
		m_VideoFinished = true;
		// Resume your app here.
	}

    public void RequestBanner()
    {
        #if UNITY_ANDROID
        string adUnitId = "123";
        #elif UNITY_IPHONE
        string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
        #else
        string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        intersitial = new InterstitialAd(adUnitId);
        // Load the interstitial with the request.
        intersitial.LoadAd(createAdRequest());

    }

    private AdRequest createAdRequest()
    {
        return new AdRequest.Builder()
            //Borrar estas lineas cuando entren a produccion (Publiquen)
                .Build();

    }
}
