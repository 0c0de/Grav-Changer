using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
using GoogleMobileAds.Api;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class Menu : MonoBehaviour {
    //Buttons
    public Button m_StartButton;
    public Button m_RestartButton;
    public Button m_Continue;
	public Button m_Achivements;

    //Images
    public Image m_DiePanel;
    public Image m_AlivePanel;
    public Image m_GravChangerLogo;
    public Image m_Valoraciones;
    public Image m_NoAds;
    public Image m_Puase;

    //Text
    public Text m_ScoreText;
    public Text m_HighScoreText;

    //ScriptReferences
    public ADS m_ADS;
	public CompleteProject.InAPPPurchase m_Purchases;

    //GameObjects
    public GameObject m_Player;
    public GameObject m_MainEffects;

    //Bools
    public bool m_Paused;

	//Strings
	private string m_ZoneID;

    // Use this for initialization
    void Start () {
		m_ZoneID = "vzf07519b6bf554834b6";
	}
	
	// Update is called once per frame
	void Update () {


		if (!m_Player.activeInHierarchy) {
			m_ScoreText.gameObject.SetActive (true);
			m_DiePanel.gameObject.SetActive (true);
			m_RestartButton.gameObject.SetActive (true);
			m_HighScoreText.gameObject.SetActive (true);
			m_Continue.gameObject.SetActive (true);
		} else {
			m_ScoreText.gameObject.SetActive (false);
			m_DiePanel.gameObject.SetActive (false);
			m_RestartButton.gameObject.SetActive (false);
			m_HighScoreText.gameObject.SetActive (false);
			m_Continue.gameObject.SetActive (false);
		}

        if (Time.timeScale == 0f)
        {
			m_Achivements.gameObject.SetActive (true);
            m_ScoreText.gameObject.SetActive(false);
            m_Puase.gameObject.SetActive(false);
            m_AlivePanel.gameObject.SetActive(true);
            m_StartButton.gameObject.SetActive(true);
            m_GravChangerLogo.gameObject.SetActive(true);
            m_NoAds.gameObject.SetActive(true);
            m_Valoraciones.gameObject.SetActive(true);
            m_MainEffects.GetComponent<BlurOptimized>().enabled = true;
        }
        else {
			m_Achivements.gameObject.SetActive (false);
            m_ScoreText.gameObject.SetActive(true);
            m_Puase.gameObject.SetActive(true);
            m_AlivePanel.gameObject.SetActive(false);
            m_StartButton.gameObject.SetActive(false);
            m_StartButton.gameObject.SetActive(false);
            m_GravChangerLogo.gameObject.SetActive(false);
            m_NoAds.gameObject.SetActive(false);
            m_Valoraciones.gameObject.SetActive(false);
            m_MainEffects.GetComponent<BlurOptimized>().enabled = false;
        }
	}

    public void _RestartButton() {
        SceneManager.LoadScene(0);
    }

    public void _StartButton() {
        Time.timeScale = 1f;
    }

    public void _PauseButton() {
        m_Paused =! m_Paused;
        if (m_Paused)
        {
            Time.timeScale = 0.0001f;
            Physics.gravity = -Physics.gravity;
        }
        else {
            Time.timeScale = 1f;
        }
    }

    public void _PlayButton() {
        Time.timeScale = 1f;
    }

    public void _RateUs() {
		Application.OpenURL ("https://play.google.com/store/apps/details?id=com.FreakinGames.GravChanger");
    }

    public void _NoADS() {
		m_Purchases.BuyConsumable ();
    }

    public void _RewardedContinueButton() {
		m_ADS.PlayAVideo (m_ZoneID);
    }

	public void _Achivement(){
		Social.ShowAchievementsUI ();
	}

}
