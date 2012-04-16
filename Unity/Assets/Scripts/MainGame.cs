using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainGame : RootObject {
	
	public Object m_LevelPrefab = null;
	public Object m_TankPrefab = null;
	public Object m_CameraPrefab = null;
	
	private GameObject m_Camera = null;
	private GameObject m_Level = null;
	private List<Tank> m_Tanks = new List<Tank>();

	private int m_Turn = 0;
	
	private bool m_WaitingForTurn = false;
	
	public List<Tank> tanks
	{
		get
		{
			return m_Tanks;
		}
	}
	
	// Use this for initialization
	protected override void onStart () {
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

		m_Tanks = new List<Tank>( 
						Object.FindObjectsOfType( typeof(Tank) ) 
								as Tank[] );
		
		NextTurn();
	}
	
	public void NextTurn()
	{
		m_WaitingForTurn = true;
		
		EnableRigidbodies(false);
	}
	
	public void DoTurn()
	{
		m_Tanks[m_Turn].StartTurn();
		
		m_Turn = (m_Turn + 1) % m_Tanks.Count;

		EnableRigidbodies(true);
	}
	
	void OnGUI()
	{
		if ( m_WaitingForTurn )
		{
			if ( GUI.Button( new Rect (200, 200, 200, 50), "Player " + m_Turn ) )
			{
				DoTurn ();
				m_WaitingForTurn = false;	
			}
		}
	}
	
	void EnableRigidbodies(bool enable)
	{
		if ( enable )
		{
			Time.timeScale = 1f;
		}
		else
		{
			Time.timeScale = 0f;
		}
	}
	
	// Update is called once per frame
	protected override void onUpdate () {
	
	}
}
