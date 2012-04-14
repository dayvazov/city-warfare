using UnityEngine;
using System.Collections;

public class RootController : MonoBehaviour {

	protected GameController m_Game = null;
	
	// Use this for initialization
	void Start () {
		m_Game = Object.FindObjectOfType( typeof(GameController) ) as GameController;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	Vector3 m_oldVel;
	public void Freeze () {
		m_oldVel = rigidbody.velocity;
		
		rigidbody.Sleep ();
	}
	
	public void Unfreeze () {
		rigidbody.WakeUp ();
		
		rigidbody.velocity = m_oldVel;
	}
}
