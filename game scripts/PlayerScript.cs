using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

	//gives my player the ability to interact with other game elements using physics
	private Rigidbody2D myRigidbody;

	//
	private Animator myAnimator;

	// This allows us to set the player speed in Unity inspector
	public float movementSpeed;

	// Can be set to true or false to change the character's facing direction
	private bool facingRight;

	public GameObject reset; 

	[SerializeField]
	private Transform[] groundPoints; // Creates something to collide with the ground

	[SerializeField]
	private float groundRadius; // Creates the size of the colliders

	[SerializeField]
	private LayerMask whatIsGround; // Defines what is ground

	private bool isGrounded; // Can be set to true or false based on our position

	private bool jump; // Can be set to true or false when we press the space key

	[SerializeField]
	private float jumpForce; // Allows us to determine the magnitude of the jump

	public bool isAlive; 

	//health slider variables
	private Slider healthBar;
	public float health = 10f;
	private float healthBurn = 5f;

	// Use this for initialization
	void Start () {
		// Associates the rigid body component of the player with a variable we could use later on (variable being myRigidbody)
		myRigidbody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator> ();
		// Default start position is Player facing right
		facingRight = true;
		isAlive = true;
		//

		reset.gameObject.SetActive (false);
		//health slider variables
		healthBar = GameObject.Find ("health slider").GetComponent<Slider>();
		healthBar.minValue = 0f;
		healthBar.maxValue = health;
		healthBar.value = healthBar.maxValue; 

	}
	// this function will be called when the player collides with an enemy
	public void UpdateHealth(){
		if (health > 0) {
			//if the health bar has 0 life left...
			health -= healthBurn; //current value of health minus 2f
			healthBar.value = health; //update the interface slider
		} if (health <= 0) {
			ImDead ();
		}
	}

	// Update is called once per frame
	void Update (){
		HandleInput ();
	}


	void FixedUpdate () {

		// Acts as the keyboards controls and move left/right
		float horizontal = Input.GetAxis("Horizontal");
		// Debug.Log (horizontal);

		// Enables the private void Flip function to work - flips player when moves left n right
		Flip(horizontal);

		isGrounded = IsGrounded ();

		if (isAlive) {
			HandleMovement (horizontal);
			Flip (horizontal); 
		} else {
			return;
		}


	}

	// Function Definitions Go Here

	private void HandleMovement(float horizontal) {

		if (isGrounded && jump) {
			isGrounded = false;
			jump = false;
			myRigidbody.AddForce (new Vector2 (0, jumpForce));
		}

		myRigidbody.velocity = new Vector2(horizontal * movementSpeed,myRigidbody.velocity.y);

		myAnimator.SetFloat ("speed" , Mathf.Abs (horizontal));
	} 

	// This function will flip the player facing direction - Also ! = not, || = or, && = and
	private void Flip(float horizontal) {
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {

			// This sets the value of facingRight to its opposite
			facingRight = !facingRight;

			// This accesses the local sale component of the Player
			Vector3 theScale = transform.localScale;

			// This takes whatever the x scale value is and changes it to 1 or -1
			theScale.x *= -1;

			// This takes the new value of the Player's x scale and reports it to the game
			transform.localScale = theScale;
		}
	}

	private void HandleInput() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			jump = true; 
			Debug.Log ("Jumping Activated");
			myAnimator.SetBool ("jumping", true);
		} 
	}

	private bool IsGrounded(){
		if (myRigidbody.velocity.y <= 0) {
			// If player is not moving vertically, test each of Player's groundPoints for collision with Ground
			foreach (Transform point in groundPoints) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);
				for(int i=0; 1 < colliders.Length; i++){
					if(colliders[i].gameObject != gameObject){
						return true;
					} 
				}
			}
		} 
		return false;
	} 

	private void ImDead (){
	myAnimator.SetBool ("dead", true);
	isAlive = false; 
		//Debug.Log ("GAME OVER");
		reset.gameObject.SetActive (true);
	}

	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "Ground") {

			myAnimator.SetBool ("jumping", false);
		}

		if (target.gameObject.tag == "deadly") {
			//gameObject.GetComponent<Collider2D>().enabled = false;
			ImDead();
		}

		if (target.gameObject.tag == "hurt") {
			UpdateHealth ();
		}
	}
}