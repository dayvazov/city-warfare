using UnityEngine;
using System.Collections;

public class CenteredCamera : RootObject {

	// Update is called once per frame
	protected override void onUpdate () {
		Vector3 pos = Vector3.zero;	
		int count = 0;
		foreach(Tank tank in game.tanks)
		{
			if ( tank != null )
			{
				pos += tank.transform.position;
				count++;
			}
		}
		
		if ( count > 1 )
		{
			pos /= (float)count;
		}
		
		pos += new Vector3(0f, camera.orthographicSize * 0.75f, 0f);
		
		pos.z = transform.position.z;
		
		transform.position = pos;
	}
}
