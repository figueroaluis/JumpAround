using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour {

	// create fields
	public bool moveLeft;
	public LayerMask groundLayer;
	public Transform wallCheck;
	public float wallCheckRadius;
	private bool wallCollision;

	// Use this for initialization
	void Start () {
		moveLeft = true;
	}

	// create method to avoid calling Physics2D so much in Update()
	void FixedUpdate () {
		wallCollision = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, groundLayer);
	}

	// Update is called once per frame
	void Update () {
		var rigidBody = GetComponent<Rigidbody2D> ();
		// after checking if it collides with a wall, add this if statement
		if (wallCollision) {
			moveLeft = !moveLeft;
		}
		if (moveLeft) {
			transform.localScale = new Vector2 (1, 1);
			rigidBody.velocity = new Vector2 (-5, rigidBody.velocity.y);
		} else {
			transform.localScale = new Vector2 (-1, 1);
			rigidBody.velocity = new Vector2 (5, rigidBody.velocity.y);
		}
	}
}
