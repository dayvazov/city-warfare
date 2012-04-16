using UnityEngine;
using System.Collections;

public class Missile : RootObject {
	
	private const float MAX_DEG = 180f;
	
	// Update is called once per frame
	protected override void onUpdate () {
		Quaternion desired = Quaternion.FromToRotation( Vector3.up, rigidbody.velocity );
		
		Quaternion newLocalRotation = Quaternion.RotateTowards( transform.localRotation, 
			desired, MAX_DEG * Time.deltaTime );
			
		if ( float.IsNaN(newLocalRotation.w) )
			return;

		transform.localRotation = newLocalRotation;
	}
	
	void OnCollisionEnter( Collision info )
	{
		Object.Destroy( gameObject );
	}
	
	
}
