using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColumnsGenerator : MonoBehaviour {

    //Transforms
    public Transform[] m_SpawnGems;

    //GameObjects
    public GameObject[] m_Gems;
    public GameObject m_Column;
    public GameObject m_NewColum;

    //Colors
    public Color m_NewColor;
    private Color m_ObtainedColor;

    //Script References
    public PlayerMovement m_Player;

    //Numeric Variables
    private float m_RandomRange1 = -5f;
    private float m_RandomRange2 = 5f;
    //private int m_ScoreChecker;
    
    // Use this for initialization
    public void Start () {
        GenerateRandomlyColors();
        InvokeRepeating("InstantiateColumns", 0.1f, 0.05f);
    }

    void InstantiateColumns() {
        m_NewColum = (GameObject) Instantiate(m_Column, m_NewColum.transform.position, Quaternion.identity);
        float x_Pos = m_NewColum.transform.position.x + 0.80f;
        float y_Pos = m_NewColum.transform.position.y + Mathf.Round((Random.Range(m_RandomRange1, m_RandomRange2) / 5));
        float z_Pos = m_NewColum.transform.position.z;
        m_NewColum.transform.position = new Vector3(x_Pos, y_Pos, z_Pos);
        
    }

    void GenerateRandomlyColors() {
        m_Column.transform.GetChild(3).GetComponent<Renderer>().sharedMaterial.SetColor("_Color", new Color(Random.value - 0.2f, Random.value + 0.2f, Random.value - 0.1f));
        m_ObtainedColor = m_Column.transform.GetChild(3).GetComponent<Renderer>().sharedMaterial.GetColor("_Color");
        m_NewColor = m_ObtainedColor;


        m_NewColum.transform.GetChild(3).GetComponent<Renderer>().material.color = m_NewColor;
        m_NewColum.transform.GetChild(4).GetComponent<Renderer>().material.color = m_NewColor;
    }

}
