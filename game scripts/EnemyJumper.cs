using UnityEngine;
using System.Collections;

public class EnemyJumper : MonoBehaviour {

	public float forceY = 300f;
	private Rigidbody2D myRigidbody;
	private Animator myAnimator;


	void Awake(){
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
	}
	// Use this for initialization
	void Start () {
		StartCoroutine (Attack ());
	}

	//this sets up a random attack every 2-4 seconds
	IEnumerator Attack(){
		yield return new WaitForSeconds (Random.Range (2, 4));

		//this sets up a random range for the forceY so the jumps will vary in height
		forceY = Random.Range (250, 550);

		//this moves the character by adding force on the y axis and it sets the animation to the attack clip
		myRigidbody.AddForce(new Vector2(0, forceY));
		myAnimator.SetBool ("attack", true);

		//this creates a sight delay before the routine runs.
		yield return new WaitForSeconds (1.5f);
		myAnimator.SetBool ("attack", false);		 //return to idle clip
		StartCoroutine (Attack ());			//loop the action
	}
	//Now we want to check for collisions
	void OnTriggerEnter2D(Collider2D target){

		if (target.tag == "bullet") {
			Destroy (gameObject);
			Destroy (target.gameObject);
		}
	}

}


