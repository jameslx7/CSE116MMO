using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basics : MonoBehaviour {

	// learn variables
	int patriots = 34;
	int falcons = 28;
	float speed = 2.5f;
	string result = "The refs were paid!";
	bool isDead = true;

	
	// Use this for initialization
	void Start () {
		//operators =, +, -, *, / these can be used to perform calculations
		// Debug.Log("Patriots=" + patriots + " " + "Falcons=" + falcons + " " + result);
		// int random = Random.Range (10, 70);
		// Debug.Log (random);
		// PrintSomething();

		if (isDead) {
			Debug.Log ("The logical test is true");
		} else {
			Debug.Log ("The logical test is false");
		}

		int num = 0;
		// looping
		while(num <5){ // As long as num is less than 5 loop this action
		
			Debug.Log ("Repeat num =" + num);
			num = num + 1; 
			// csharp WIZARDY NOT FOR NOOBS num++; 
		}

	}

	int Multiply (int a, int b){
		return a * b;
	}

	int AddUp(int a, int b){
		return a + b;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	// function definitions
	void PrintSomething(){
		Debug.Log ("I am running the PrintSomething Function!" + " " + AddUp (2, 5) + " " + Multiply (2, 5));
	}
}
