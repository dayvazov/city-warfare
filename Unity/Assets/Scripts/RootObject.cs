using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[UnityEngine.ExecuteInEditMode]
public class RootObject : MonoBehaviour {
	
	public List<RootObject> m_Children = new List<RootObject>();
	
	public bool m_Flipped = false;
	
	public GameObject m_Creator;
	
	public static GameObject FindRoot(GameObject g)
	{
		while ( g.transform.parent != null )
		{
			g = g.transform.parent.gameObject;
		}	
		
		return g;
	}
	
	private MainGame _game = null;
	public MainGame game
	{
		get
		{
			if ( _game == null )
			{
				_game = Object.FindObjectOfType( typeof(MainGame) ) as MainGame;
			}
			
			return _game;
		}
		
		set
		{
			_game = value;
		}
	}
	
	private RagePixelSprite _sprite;
	public RagePixelSprite sprite
	{
		get
		{
			if ( _sprite == null )
			{
				_sprite = GetComponent<RagePixelSprite>();
				
				if ( _sprite == null )
				{
					_sprite = GetComponentInChildren<RagePixelSprite>();
				}
			}
			
			return _sprite;
		}
	}
	
	public virtual void SetHorizontalFlip(bool flip)
	{
		if ( flip != m_Flipped )
		{
			if ( sprite != null )
			{
				sprite.SetHorizontalFlip( flip );
			}
			
			foreach( RootObject child in m_Children )
			{
				Vector3 pos = child.transform.localPosition;
				pos.x *= -1f;
				child.transform.localPosition = pos;
				
				child.SetHorizontalFlip( flip );
			}
			
			m_Flipped = flip;
		}
	}
			
	
	// Use this for initialization
	void Start () {
#if UNITY_EDITOR
		if ( !Application.isPlaying )
		{
			onEditorStart();
		}
		else
#endif
		{
			onStart();
		}
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
		if ( !Application.isPlaying )
		{
			onEditorUpdateRoot();
			
			onEditorUpdate();
		}
		else
#endif
		{
			onUpdate();
		}
	}
	
	private void onEditorUpdateRoot()
	{
		m_Children = new List<RootObject>();
		
		for ( int i = 0; i < transform.childCount; i++ )
		{
			RootObject r = transform.GetChild(i).GetComponent<RootObject>();
			
			if ( r != null )
			{
				m_Children.Add(r);
			}
		}
	}

	protected virtual void onStart() {}
	protected virtual void onUpdate() {}
	
	protected virtual void onEditorStart() {}
	protected virtual void onEditorUpdate() {}
	
}
