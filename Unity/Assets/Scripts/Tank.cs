using UnityEngine;
using System.Collections;

public class Tank : RootObject {
	
	public float m_MaxFocusTime = 5f;

	public float m_Health = 200f;
	
	private bool m_HasFocus = false;
	private float m_GotFocusTime = 0f;
	
	private int m_NumCollisions = 0;
	
	private Turret[] m_Turrets = new Turret[0];
	
	private Turret mainTurret
	{
		get
		{
			
			
			if ( m_Turrets.Length == 0 )
			{
				m_Turrets = GetComponentsInChildren<Turret>();
			}
			
			return m_Turrets[0];
		}
	}
	
	public void StartTurn() 
	{
		m_HasFocus = true;
		m_GotFocusTime = Time.time;
		
		m_Firing = false;
	}

	bool m_Firing = false;
	Vector3 m_InitialFirePosition;
	
	private const float speed = 15f;
	private const float acceleration = speed * 8f;

	private void UpdateInput()
	{
		if ( m_NumCollisions > 0 && Time.timeScale > 0f )
		{
			
			float velocity = rigidbody.velocity.x;
			float targetVelocity = 0f;
			
			if ( m_HasFocus )
			{
				if(Input.GetKey(KeyCode.LeftArrow)) {
					targetVelocity = -speed;
					SetHorizontalFlip(true);
					
				} else if (Input.GetKey(KeyCode.RightArrow)) {
					targetVelocity = speed;
					SetHorizontalFlip(false);
				} 
			}
			
			if ( Mathf.Abs( targetVelocity - velocity ) < speed / 20f )
			{
			}
			else if ( targetVelocity < velocity )
			{
				rigidbody.AddForce( new Vector3( -acceleration, 0, 0 ), ForceMode.Acceleration );
			}
			else if ( targetVelocity > velocity )
			{
				rigidbody.AddForce( new Vector3( acceleration, 0, 0 ), ForceMode.Acceleration );
			}
		}
		
		if ( !m_HasFocus )
			return;
		
		if ( m_GotFocusTime + m_MaxFocusTime < Time.time )
		{
			m_HasFocus = false;
			
			game.NextTurn();
			
			return;
		}

		if ( m_Firing )
		{
			Vector3 aim = m_InitialFirePosition - Input.mousePosition;
			
			bool flip = aim.x < 0f;
			SetHorizontalFlip( flip );
			
			float angle = Mathf.Rad2Deg * Mathf.Atan(aim.y / Mathf.Abs (aim.x));
			
			mainTurret.setAngle( angle );
			
			
			
			if ( Input.GetMouseButtonUp(0) )
			{
				m_Firing = false;
				
				Vector3 velocity = m_InitialFirePosition - Input.mousePosition;
				float velMag = velocity.magnitude;
				
				if ( velMag == 0f )
					return;
				
				mainTurret.fire( velMag / 5f );
			}
		}
		else
		{
			if ( Input.GetMouseButtonDown(0) )
			{
				m_Firing = true;
				m_InitialFirePosition = Input.mousePosition;
			}
			
		}
		
	}
	
	void OnCollisionEnter( Collision info )
	{
		m_NumCollisions++;
	}

	void OnCollisionExit( Collision info )
	{
		m_NumCollisions--;
	}

	
	// Update is called once per frame
	protected override void onUpdate () {
		UpdateInput();
	}
	
	protected override void onEditorUpdate()
	{
		m_Turrets = GetComponentsInChildren<Turret>();
	}
	
	void Hit(float damage)
	{
		m_Health -= damage;
		
		if ( m_Health <= 0f )
		{
			if ( m_HasFocus )
			{
				m_HasFocus = false;
				
				game.NextTurn();
			}
			
			Object.Destroy( gameObject );
		}
	}
}
