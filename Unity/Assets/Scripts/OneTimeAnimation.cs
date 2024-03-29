using UnityEngine;                                                                                                                                                                                                                                                                                                                                                    using UnityEngine;
using System.Collections;

public class OneTimeAnimation : RootObject {
	
	public string m_AnimationName = "Explode";
	
	// Use this for initialization
	protected override void onStart () {
		sprite.PlayNamedAnimation(m_AnimationName);
	}
	
	// Update is called once per frame
	protected override void onUpdate () {
		if ( ! sprite.isPlaying() )
		{
			Object.Destroy( gameObject );
		}
	}
}
