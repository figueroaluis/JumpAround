using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour {

	// fields
	private int lives = 3;
	public SpriteRenderer sprite;
	public LayerMask groundLayer;
	public Transform groundCheck;
	public float groundCheckRadius;
	private bool grounded;
	private bool doubleJumped;
	public CanvasGroup canvasYouWin;

	// Use this for initialization
	void Start () {
		
	}

	// create method to avoid calling Physics2D so much in Update()
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundLayer);
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
			// first we check the direction in which the object is facing
			sprite.flipX = false;
			// this will move our object with a speed of 5, while staying vertically
			if(Input.GetKey("left shift")){
				rigidBody.velocity = new Vector2 (10, rigidBody.velocity.y);
			} else{
				rigidBody.velocity = new Vector2 (5, rigidBody.velocity.y);
			}

		}
		if (Input.GetKey("left")){
			sprite.flipX = true;
			if (Input.GetKey ("left shift")) {
				rigidBody.velocity = new Vector2 (-10, rigidBody.velocity.y);
			} else {
				rigidBody.velocity = new Vector2 (-5, rigidBody.velocity.y);
			}
		}
		if (grounded) {
			doubleJumped = false;
		}
		if (Input.GetKeyDown ("space") && grounded) {
			rigidBody.velocity = new Vector2 (rigidBody.velocity.x, 10);
		}
		// this is the if-statement to do a double jump
		if (Input.GetKeyDown("space") && !grounded && !doubleJumped) {
			// let the object jump one more time
			rigidBody.velocity = new Vector2 (rigidBody.velocity.x, 10);
			doubleJumped = true;
		}

		// check to see if the object fell down the platform
		// so if the position is very low, then it fell down, and needs to respawn
		if (transform.position.y < -10) {
			lives -= 1;
			if (transform.position.x < -2) {
				// in order to respawn, just give it a new position
				tranform.position = new Vector2 (-5, 2);
			} else {
				tranform.position = new Vector2 (2, 2);
			}
		}
		if (lives == 0) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}

	// if we hit the enemy, then we "die" and return to initial position
	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.name == "EnemyDamage") {
			lives -= 1;
			transform.position = new Vector2 (2, 1);
		} else if (collider.name == "exit") {
			canvasYouWin.alpha = 1;
		}
	}

}
