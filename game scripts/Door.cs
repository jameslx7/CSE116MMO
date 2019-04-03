//this script depends on the fact that there are 8 collectibles at the start of the level.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Door : MonoBehaviour {

	public static Door instance;

	public GameObject NextLevel;

	//Assigning names to the door animation states and the collider
	private Animator anim;
	private BoxCollider2D box;

	//this variable will be used in both the door object and the collectables we will use it to trigger the opening of the door
	[HideInInspector]
	public int collectiblesCount;
	public Text countText;
	public Text winText;

	void Start () {
		NextLevel.gameObject.SetActive (false);
	}

	// Use this for initialization
	void Awake(){
		//This creates the door instance
		MakeInstance ();
		//This loads its default animation state
		anim = GetComponent<Animator> ();
		//this gets its BoxCollider state and packages it in a variable called box
		box = GetComponent<BoxCollider2D> ();
		winText.text = "";
		countText.text = "COINS: 0/8";
		collectiblesCount = collectiblesCount - 8;
	}

	void MakeInstance(){
		//this creates the door in the frame
		if (instance == null) 
			instance = this;

	}
	//This is a function that lowers the value of collectiblesCount which will be used in the collectable objects
	public void DecrementCollectibles(){
		//lower the value of collectiblesCount by one
		collectiblesCount++;

		countText.text = "COINS: " + collectiblesCount.ToString () + "/8";
		//check to see if the value of collectiblesCount has reached 0
		if(collectiblesCount == 8){
			//if there are no more collectibles on the scene, begin the OpenDoor coroutine
			StartCoroutine (OpenDoor ());
			winText.text = " Get to the portal!";
		}
	}

	//This function switches the animation state of the door from Idle to Open and turns on the box trigger to see if the player touches the door
	IEnumerator OpenDoor(){
		anim.Play ("Open");
		yield return new WaitForSeconds (.7f);
		//turns the trigger of the Door boxCollider on
		box.isTrigger = true;
	}
	//this constantly checks for a collision with the Player object
	void OnTriggerEnter2D(Collider2D target){
		if (target.tag == "Player"){
			//iff a collision with the player occurs, do this.  This can be used to load the next scene.
			Debug.Log("Level Finished");
		//	SceneManager.LoadScene("Level2");
			NextLevel.gameObject.SetActive (true);
				

		}
	}
}


