using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	public Object m_LevelPrefab = null;
	public Object m_TankPrefab = null;
	public Object m_CameraPrefab = null;
	
	private GameObject m_Camera = null;
	private GameObject m_Level = null;
	private List<TankController> m_Tanks = new List<TankController>();

	public int m_Turn = 0;
	
	// Use this for initialization
	void Start () {
		m_Camera = Object.Instantiate( m_CameraPrefab ) as GameObject;
		m_Level = Object.Instantiate( m_LevelPrefab ) as GameObject;
		
		GameObject[] allObjs = GameObject.FindObjectsOfType( typeof(GameObject) ) as GameObject[];
		foreach( GameObject obj in allObjs )
		{
			if ( obj.name == "SpawnPoint" )
			{
				Object.Instantiate( 
					m_TankPrefab, 
					obj.transform.position, 
					obj.transform.rotation );
			}
		}

		m_Tanks = new List<TankController>( 
						Object.FindObjectsOfType( typeof(TankController) ) 
								as TankController[] );
		
		NextTurn();
	}
	
	public void NextTurn()
	{
		m_Tanks[m_Turn].StartTurn();
		
		m_Turn = (m_Turn + 1) % m_Tanks.Count;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
