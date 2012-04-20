using UnityEngine;
using System.Collections;

public class Missile : RootObject {
	
	public Object m_Explosion;
	public float m_Radius = 30f;
	public float m_Damage = 100f;
	
	private const float MAX_DEG = 180f;
	
	bool m_SuicidePlease = false;
	
	// Update is called once per frame
	protected override void onUpdate () {
		if ( m_SuicidePlease )
		{
			Object.Instantiate(m_Explosion, transform.position, Quaternion.identity);
			
			Collider[] objectsHit = Physics.OverlapSphere(transform.position, m_Radius);
			
			foreach (Collider c in objectsHit)
			{
				if ( FindRoot(c.gameObject) == m_Creator )
					continue;
				
				c.gameObject.SendMessageUpwards("Hit", m_Damage, SendMessageOptions.DontRequireReceiver);
			}
			
			Object.Destroy(gameObject);
			return;
		}
		
		Quaternion desired = Quaternion.FromToRotation( Vector3.up, rigidbody.velocity );
		
		Quaternion newLocalRotation = Quaternion.RotateTowards( transform.localRotation, 
			desired, MAX_DEG * Time.deltaTime );
			
		if ( float.IsNaN(newLocalRotation.w) )
			return;

		transform.localRotation = newLocalRotation;
	}
	
	void OnCollisionEnter( Collision info )
	{
		m_SuicidePlease = true;
	}
	
	void OnTriggerEnter( Collider other )
	{
		GameObject root = FindRoot(other.gameObject);
		
		Missile missile = root.GetComponent<Missile>();
		
		if ( missile != null && missile.m_Creator != m_Creator )
		{
			m_SuicidePlease = true;
		}
	}
	
	
}
