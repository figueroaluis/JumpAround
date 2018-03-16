using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// interact with the object by using GetComponent and component Rigidbody2D
		var rigidBody = GetComponent<Rigidbody2D>();
		// to make the object respawn use the Transform component
		var tranform = GetComponent<Transform>();

		// this will execute if the "right" arrow key is pressed
		if (Input.GetKey("right")) {
			// to make the object move, we give it some speed and direction
			// aka Velocity, this is a vector so we need to use vectors to move our object
			// this will move our object with a speed of 5, while staying vertically
			rigidBody.velocity = new Vector2 (5, rigidBody.velocity.y);
		}
		if (Input.GetKey("left")){
			rigidBody.velocity = new Vector2 (-5, rigidBody.velocity.y);
		}
		if (Input.GetKeyDown ("space")) {
			rigidBody.velocity = new Vector2 (rigidBody.velocity.x, 10);
		}

		// check to see if the object fell down the platform
		// so if the position is very low, then it fell down, and needs to respawn
		if (transform.position.y < -6) {
			if (transform.position.x < -2) {
				// in order to respawn, just give it a new position
				tranform.position = new Vector2 (-5, 2);
			} else {
				tranform.position = new Vector2 (2, 2);
			}
	
		}

	}


}
