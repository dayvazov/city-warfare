using UnityEngine;
using System.Collections;

public class Turret : RootObject {

	private float m_Angle = 0f;
	
	public FireType m_FireType = new FireType();
	
	public float m_MinAngle = 20f;
	public float m_MaxAngle = 70f;
	
	private Transform _firepoint = null;
	public Transform firepoint
	{
		get
		{
			if ( _firepoint == null )
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					Transform t = transform.GetChild(i);
					
					if ( t.gameObject.name == "FirePoint" )
					{
						_firepoint = t;
						break;
					}
				}
			}
			
			return _firepoint;
		}
	}
	
	// Use this for initialization
	protected override void onStart () {
		m_Angle = 90f - sprite.transform.localRotation.z * Mathf.Rad2Deg * 2f;
	}
	
	// Update is called once per frame
	protected override void onUpdate () {
		
	}
	
	public void fire(float speed)
	{
		speed = Mathf.Clamp(speed, 4f, 40f);
			
		GameObject missile = Object.Instantiate( 
			m_FireType.m_Projectile, 
			firepoint.transform.position, 
			firepoint.transform.rotation ) as GameObject;		

		missile.rigidbody.velocity = speed * missile.transform.TransformDirection( Vector3.up );	
	}

	public void setAngle(float angle)
	{
		Debug.Log ( angle);
		if ( float.IsNaN(angle) )
			return;
		
		m_Angle = Mathf.Clamp(angle, m_MinAngle, m_MaxAngle);
		
		resetAngle();
	}
	
	private void resetAngle()
	{
		float angleFrom90 = 90f - m_Angle;
		float angle = (m_Flipped) ? angleFrom90 : -angleFrom90;
		
		sprite.transform.localRotation = Quaternion.Euler( new Vector3(0f, 0f, angle) );
	}
	
	public override void SetHorizontalFlip(bool flip)
	{
		base.SetHorizontalFlip(flip);

		resetAngle();
	}

}
