using UnityEngine;
using System.Collections;

public class CenteredCamera : RootObject {

	// Update is called once per frame
	protected override void onUpdate () {
		Vector3 pos = Vector3.zero;		
		foreach(Tank tank in game.tanks)
		{
			pos += tank.transform.position;
		}
		
		pos += new Vector3(0f, 50f, 0f);
		
		pos.z = transform.position.z;
		
		transform.position = pos;
	}
}
