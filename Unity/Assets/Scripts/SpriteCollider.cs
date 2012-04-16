using UnityEngine;
using System.Collections;

public class SpriteCollider : RootObject {

	public override void SetHorizontalFlip (bool flip)
	{
		base.SetHorizontalFlip (flip);
		
		transform.localRotation = Quaternion.Euler( new Vector3( flip ? 180f : 0f, 0f, 0f ) );
	}
}
