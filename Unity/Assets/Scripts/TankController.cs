using UnityEngine;
using System.Collections;

public class TankController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 v = rigidbody.velocity;
		
		if(Input.GetKeyDown(KeyCode.LeftArrow)) {
			v.x = -100;			
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			v.x = 100;
		} 
		else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
		{
			v.x = 0;
		}
		
		rigidbody.velocity = v;
	}
}
