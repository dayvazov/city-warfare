using UnityEngine;
using System.Collections;

public class MissileController : RootController {

	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter( Collision info )
	{
		Object.Destroy( gameObject );
	}
	
	
}
