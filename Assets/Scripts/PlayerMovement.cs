using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using GooglePlayGames;

public class PlayerMovement : MonoBehaviour {

    //Coded by Jose Luis
    //Transforms
	public Transform m_BackSpawn;

	//Audio
	public AudioSource m_AudioMain;

    //UI
    public Text m_ScoreText;
    public Text m_HighScoreText;

    //Numeric Values
    public float m_Speed;
    public int m_Score;
    private int m_HighScore;

    //GameObject
    public GameObject m_NewText;
	[HideInInspector] public GameObject m_MyPlayer;

    //Layers
    public LayerMask m_WallCollision;

    //Vector3
    private Vector3 m_TempPos;

    //References
    private Rigidbody m_Rigid;

    //Script References
    public ADS m_ADS;
    public ColumnsGenerator m_Colum;

    //Booleans
    private bool m_CollisionRight = false;
	
    // Use this for initialization
	void Start () {
		m_MyPlayer = this.gameObject;
		m_AudioMain.Play ();
        Time.timeScale = 0f;
        this.gameObject.SetActive(true);
        m_Rigid = GetComponent<Rigidbody>();
		PlayerPrefs.DeleteKey ("speed");
        InvokeRepeating("IncreaseSpeed", 10f, 10f);

		PlayGamesPlatform.Instance.Authenticate ((bool success) =>{
			if(success){
				Debug.Log ("Everything was fine");
			}else{
				Debug.Log("Crash");
			}
		});
    }

	void Awake(){
		
	}
	
	// Update is called once per frame
	void Update () {
        Displacement();

		if (Input.GetMouseButtonDown(0)) {

			if (Social.localUser.authenticated) {
				Social.ReportProgress ("CgkIhsmRsaMDEAIQAQ", 100, (bool success) => {
				});
			}
            GravityJump();
        }


        m_CollisionRight = Physics.Raycast(transform.position, Vector3.right, 0.5f, m_WallCollision.value);
        if (m_CollisionRight) {
            m_TempPos = transform.position;
            if (m_TempPos == transform.position) {
                Die();
            }
            
        }


	}

    void Displacement() {
        m_Rigid.MovePosition(new Vector3(transform.position.x + m_Speed, transform.position.y, transform.position.z));
    }

    void GravityJump() {
        Physics.gravity = -Physics.gravity;
        m_Rigid.AddForce(Vector3.up * m_Speed * Time.deltaTime, ForceMode.Impulse);
    }

    void Die() {
        //Establecemos la maxima puntuacion

        if (PlayerPrefs.HasKey("HighScore")) {
                if (PlayerPrefs.GetInt("Highscore") < m_Score){
                PlayerPrefs.SetInt("Highscore", m_Score);
                m_NewText.SetActive(true);
            }
        }
        else {
            PlayerPrefs.SetInt("HighScore", m_Score);
        }
        m_HighScore = PlayerPrefs.GetInt("Highscore");
		PlayerPrefs.SetFloat ("speed", m_Speed);
        PlayerPrefs.Save();
        
		if (m_HighScore >= 50 && Social.localUser.authenticated) {
			Social.ReportProgress ("CgkIhsmRsaMDEAIQAg", 100, (bool success) =>{
			});
		}

		if (m_HighScore >= 1000 && Social.localUser.authenticated) {
			Social.ReportProgress ("CgkIhsmRsaMDEAIQBQ", 100, (bool success) =>{
			});
		}

		if (m_HighScore >= 300 && Social.localUser.authenticated) {
			Social.ReportProgress ("CgkIhsmRsaMDEAIQAw", 100, (bool success) =>{
			});
		}

		if (m_HighScore >= 600 && Social.localUser.authenticated) {
			Social.ReportProgress ("CgkIhsmRsaMDEAIQBA", 100, (bool success) =>{
			});
		}

		m_HighScoreText.text = "HighScore" + m_HighScore.ToString();
        if (Random.value >= 0.6f)
        {
            if (m_ADS.intersitial.IsLoaded())
            {
                m_ADS.intersitial.Show();
            }
        }
        //El gameObject muere
		m_MyPlayer.SetActive(false);
    }

    void OnTriggerExit(Collider trigger) {
        if (trigger.gameObject.tag == "ScoreCheck") {
            m_Score++;
            PlayerPrefs.SetInt("Score", m_Score);
            m_ScoreText.text = "Score: " + m_Score.ToString();
        }
    }

    void IncreaseSpeed()
    {
        m_Speed = m_Speed+0.05f;
    }

    public void Revive()
    {
		m_MyPlayer.SetActive(true);
		m_Speed = PlayerPrefs.GetFloat ("speed");
    }
}
