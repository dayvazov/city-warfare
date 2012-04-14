using UnityEngine;
using System.Collections;

public class TankController : RootController {
	
	public Object m_BasicMissile = null;
	
	public float m_MaxFocusTime = 5f;
	
	private bool m_HasFocus = false;
	private float m_GotFocusTime = 0f;
	
	
	public void StartTurn() 
	{
		m_HasFocus = true;
		m_GotFocusTime = Time.time;
		
		m_Firing = false;
	}

	bool m_Firing = false;
	Vector3 m_InitialFirePosition;
	
	private void UpdateInput()
	{
		if ( !m_HasFocus )
			return;
		
		if ( m_GotFocusTime + m_MaxFocusTime < Time.time )
		{
			m_HasFocus = false;
			
			m_Game.NextTurn();
			
			return;
		}

		if ( m_Firing )
		{
			if ( Input.GetMouseButtonUp(0) )
			{
				m_Firing = false;
				
				Vector3 velocity = m_InitialFirePosition - Input.mousePosition;
				float velMag = velocity.magnitude;
				
				float MAX_DIST = Screen.height / 5f;
				float speed = Mathf.Min(velMag / MAX_DIST, 1f) * 50f;
				
				Vector3 fireDir = velocity / velMag;
				
				Vector3 firePosition = transform.position;
				
				firePosition += fireDir * 4f;
				
				GameObject missile = Object.Instantiate( 
					m_BasicMissile, 
					firePosition, 
					Quaternion.identity ) as GameObject;
				
				Physics.IgnoreCollision(collider, missile.collider);
				
				missile.rigidbody.velocity = speed * fireDir;
			}
		}
		else
		{
			if ( Input.GetMouseButtonDown(0) )
			{
				m_Firing = true;
				m_InitialFirePosition = Input.mousePosition;
			}
			
			Vector3 v = rigidbody.velocity;
			
			if(Input.GetKey(KeyCode.LeftArrow)) {
				if ( v.x > -100 )
				{
					rigidbody.AddForce( new Vector3( -100, 0, 0 ), ForceMode.Acceleration );
				}
			} else if (Input.GetKey(KeyCode.RightArrow)) {
				if ( v.x < 100 )
				{
					rigidbody.AddForce( new Vector3( 100, 0, 0 ), ForceMode.Acceleration );
				}
			} 
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		UpdateInput();
	}
}
